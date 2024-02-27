import React, { useState, Dispatch, SetStateAction, useEffect } from "react";
import {
  ActionMenuDivider,
  Button,
  ButtonColor,
  ButtonSize,
  ButtonType,
  Cols,
  Column,
  Dialog,
  Input,
  MenuItem,
  Paper,
  Row,
  Select,
  TextArea,
  TreeNode,
  TreeView,
} from "@kentico/xperience-admin-components";
import { usePageCommand } from "@kentico/xperience-admin-base";
import { Title } from "../Shared/Title";
import { TaxonomyCategory, TaxonomyNode } from "./TaxonomyNode";

interface ClientPageTemplateProperties {
  readonly initialTaxonomies: TaxonomyCategory[];
}

interface ResponseResult {
  taxonomies: TaxonomyCategory[];
}

const createTaxonomyValue = (
  taxonomies: TaxonomyCategory[],
  node: TaxonomyCategory,
  isEditing: boolean
) => {
  const regex = /^(\-*)/;

  let taxonomyValue = `--${
    regex.exec(node.parentValue)?.[1]
  }${node.displayName.toLowerCase()}`;

  const siblingsWithSameValue = taxonomies.filter((taxonomy) => {
    return (
      taxonomy.parentGUID === node.parentGUID &&
      taxonomy.value.startsWith(taxonomyValue)
    );
  });

  if (
    isEditing &&
    siblingsWithSameValue.some((element) => element.guid === node.guid)
  ) {
    return node.value;
  }

  if (siblingsWithSameValue.length > 0) {
    return `${taxonomyValue}_${siblingsWithSameValue.length}`;
  } else {
    return taxonomyValue;
  }
};

const initialFormData = {
  guid: "",
  parentGUID: "",
  displayName: "",
  value: "",
  parentValue: "",
  description: "",
};

const ROOT_NAME = "root";
const EMPTY_GUID = "00000000-0000-0000-0000-000000000000";

const rootNode = {
  guid: EMPTY_GUID,
  parentGUID: EMPTY_GUID,
  displayName: "Category",
  value: ROOT_NAME,
  parentValue: ROOT_NAME,
  description: "",
};

const getAllChildren = (
  selectedNode: TaxonomyCategory,
  taxonomies: TaxonomyCategory[]
) => {
  const children: TaxonomyCategory[] = [];
  const nodeQueue = taxonomies.filter((taxonomy) => {
    return taxonomy.parentGUID === selectedNode.guid;
  });

  while (nodeQueue.length > 0) {
    const currentNode = nodeQueue.shift();

    if (!currentNode) return;
    taxonomies
      .filter((taxonomy) => {
        return currentNode.guid === taxonomy.parentGUID;
      })
      .forEach((child) => {
        nodeQueue.push(child);
      });

    children.push(currentNode);
  }

  return children;
};

export const TaxonomiesTemplate = ({
  initialTaxonomies = [],
}: ClientPageTemplateProperties) => {
  const [isEditing, setIsEditing] = useState(false);
  const [isCreating, setIsCreating] = useState(false);
  const [rootExpanded, setRootExpanded] = useState(false);
  const [activeNode, setActiveNode] = useState(rootNode);
  const [taxonomies, setTaxonomies] = useState(initialTaxonomies);
  const [formData, setFormData] = useState<TaxonomyCategory>(initialFormData);
  const [hasError, setHasError] = useState(false);
  const [openModal, setOpenModal] = useState(false);

  const { execute: invokeGetAll } = usePageCommand<ResponseResult>(
    "GetAllTaxonomies",
    {
      after: (response) => {
        setTaxonomies(response!.taxonomies);
      },
    }
  );
  const { execute: invokeUpdate } = usePageCommand<void, TaxonomyCategory[]>(
    "EditTaxonomies"
  );

  const { execute: invokeSave } = usePageCommand<void, TaxonomyCategory>(
    "SaveTaxonomy"
  );

  const { execute: invokeDelete } = usePageCommand<void, TaxonomyCategory[]>(
    "DeleteTaxonomies"
  );

  const renderChildNodes = (
    taxonomies: (TaxonomyCategory | undefined)[],
    parentGUID: string,
    level: number
  ) => {
    const nodesToRender: any[] = taxonomies.filter(
      (taxonomy) => taxonomy?.parentGUID === parentGUID
    );

    return nodesToRender.map((node: TaxonomyCategory, i) => {
      return (
        <TaxonomyNode
          taxonomyData={node}
          level={level}
          key={i}
          activeNode={activeNode}
          setActiveNode={setActiveNode}
          setIsCreating={setIsCreating}
          setIsEditing={setIsEditing}
          setFormData={setFormData}
        >
          {renderChildNodes(taxonomies, node.guid, level + 1)}
        </TaxonomyNode>
      );
    });
  };

  return (
    <Row>
      <Dialog
        isOpen={openModal}
        headline={"Delete"}
        onClose={() => {
          setOpenModal(false);
        }}
        headerCloseButton={{ tooltipText: "Close" }}
        isDismissable={true}
        confirmAction={{
          label: "confirm",
          onClick: async () => {
            setIsCreating(false);
            setIsEditing(false);

            const nodesToDelete = getAllChildren(activeNode, taxonomies);
            nodesToDelete?.unshift({ ...activeNode });

            await invokeDelete(nodesToDelete);
            await invokeGetAll();
            setActiveNode(rootNode);
            setOpenModal(false);
          },
        }}
        cancelAction={{
          label: "cancel",
          onClick: () => {
            setOpenModal(false);
          },
        }}
      >
        {`Are you sure you want to delete '${activeNode.displayName}' ?`}
      </Dialog>
      <Column width={50}>
        <Paper>
          <div style={{ padding: "16px" }}>
            <div
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "space-between",
              }}
            >
              <Button
                label="New Term"
                size={ButtonSize.S}
                color={ButtonColor.Secondary}
                onClick={() => {
                  setHasError(false);
                  setIsCreating(true);
                  setIsEditing(false);
                  setFormData(initialFormData);
                }}
              />
              <Button
                icon={"xp-bin"}
                size={ButtonSize.S}
                color={ButtonColor.Quinary}
                destructive
                disabled={
                  activeNode.value === ROOT_NAME || activeNode.value === ""
                }
                onClick={() => {
                  setOpenModal(true);
                }}
              />
            </div>

            <ActionMenuDivider />
            <TreeView>
              <TreeNode
                isToggleable={taxonomies.length > 0}
                onNodeClick={() => {
                  setActiveNode(rootNode);
                  setIsCreating(false);
                  setIsEditing(false);
                }}
                onNodeToggle={() => {
                  setRootExpanded(!rootExpanded);
                }}
                isExpanded={rootExpanded}
                isDraggable={false}
                level={1}
                renderNode={() => {
                  return (
                    <Title
                      title={"Categories"}
                      activeNode={activeNode}
                      identifier={rootNode.guid}
                    />
                  );
                }}
                nodeIdentifier={ROOT_NAME}
              >
                {renderChildNodes(taxonomies, rootNode.parentGUID, 2)}
              </TreeNode>
            </TreeView>
          </div>
        </Paper>
      </Column>
      {(isEditing || isCreating) && (
        <Column cols={Cols.Col4}>
          <div style={{ margin: "0 16px" }}>
            <div style={{ marginBottom: "16px" }}>
              <Button
                label="Save"
                color={ButtonColor.Primary}
                onClick={async () => {
                  setHasError(false);
                  if (isCreating) {
                    const data = {
                      guid: "",
                      parentGUID: activeNode.guid,
                      displayName: formData.displayName,
                      value: "",
                      parentValue: activeNode.value,
                      description: formData.description,
                    };

                    data.value = createTaxonomyValue(
                      taxonomies,
                      data,
                      isEditing
                    );
                    if (!formData.displayName) {
                      setHasError(true);
                      return;
                    }

                    await invokeSave(data);
                    await invokeGetAll();

                    setFormData(initialFormData);
                  }

                  if (isEditing) {
                    const nodesToEdit = [];
                    const editingNode = { ...activeNode };

                    const editingNodeDirectChildren = taxonomies.filter(
                      (taxonomy) => {
                        return taxonomy.parentGUID === editingNode.guid;
                      }
                    );

                    editingNode.displayName = formData.displayName;
                    editingNode.parentValue = formData.parentValue;
                    editingNode.parentGUID = formData.parentGUID;
                    editingNode.description = formData.description;
                    editingNode.value = createTaxonomyValue(
                      taxonomies,
                      editingNode,
                      isEditing
                    );

                    nodesToEdit.push(editingNode);

                    const nodeQueue = [...editingNodeDirectChildren];

                    while (nodeQueue.length > 0) {
                      const currentNode = nodeQueue.shift();

                      if (!currentNode) return;
                      const parentNode = taxonomies.find((taxonomy) => {
                        return taxonomy.guid === currentNode.parentGUID;
                      });

                      currentNode.parentValue = parentNode!.value;
                      currentNode.value = createTaxonomyValue(
                        taxonomies,
                        currentNode,
                        isEditing
                      );

                      taxonomies
                        .filter((taxonomy) => {
                          return taxonomy.parentGUID === currentNode.guid;
                        })
                        .forEach((child) => {
                          nodesToEdit.push(child);
                        });

                      nodesToEdit.push(currentNode);
                    }

                    if (!formData.displayName) {
                      setHasError(true);
                      return;
                    }

                    await invokeUpdate(nodesToEdit);
                    await invokeGetAll();
                  }
                }}
                type={ButtonType.Button}
              />
            </div>

            <Paper>
              <div style={{ padding: "16px" }}>
                {(isEditing || isCreating) && (
                  <Input
                    label="Name"
                    type="text"
                    invalid={hasError}
                    validationMessage="Cannot be empty"
                    markAsRequired={true}
                    onChange={(evt) => {
                      setFormData((data) => ({
                        ...data,
                        displayName: evt.target.value,
                      }));
                    }}
                    value={formData.displayName}
                  />
                )}

                <br />
                <TextArea
                  label="Description"
                  onChange={(evt) => {
                    setFormData((data) => ({
                      ...data,
                      description: evt.target.value,
                    }));
                  }}
                  value={formData.description}
                />

                <br />
                {isEditing && (
                  <Select
                    onChange={(val) => {
                      setFormData((data) => {
                        const parentValue =
                          taxonomies.find((taxonomy) => taxonomy.guid === val)
                            ?.value || ROOT_NAME;

                        return {
                          ...data,
                          parentValue,
                          parentGUID: val!,
                        };
                      });
                    }}
                    label="Parent Category"
                    value={formData.parentGUID}
                  >
                    <MenuItem primaryLabel={ROOT_NAME} value={rootNode.guid} />
                    {taxonomies.map((taxonomy, i) => {
                      if (
                        taxonomy.guid === activeNode.guid &&
                        taxonomy.value !== ROOT_NAME
                      )
                        return;

                      const regex = /^(\-*)/;

                      const activeNodeLevel = regex.exec(activeNode.value);
                      const currentLevel = regex.exec(taxonomy.value);

                      const linkedLevel =
                        activeNodeLevel![1].length >= currentLevel![1].length;

                      if (linkedLevel) {
                        return (
                          <MenuItem
                            key={i}
                            primaryLabel={taxonomy.value}
                            value={taxonomy.guid}
                          />
                        );
                      }
                    })}
                  </Select>
                )}
              </div>
            </Paper>
          </div>
        </Column>
      )}
    </Row>
  );
};
