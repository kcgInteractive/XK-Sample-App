import React, { useState, Dispatch, SetStateAction } from "react";
import {
  ActionMenuDivider,
  Button,
  ButtonColor,
  ButtonSize,
  ButtonType,
  Cols,
  Column,
  MenuItem,
  Paper,
  Row,
  Select,
  TreeNode,
  TreeView,
} from "@kentico/xperience-admin-components";
import { FormComponentProps } from "@kentico/xperience-admin-base";

const ROOT_NAME = "root";

export interface TaxonomyCategory {
  displayName: string;
  value: string;
  parentValue: string;
}

export interface TaxonomyNode {
  taxonomyData: TaxonomyCategory;
  level: number;
  children: any;
  activeNode: string;
  setActiveNode: Dispatch<SetStateAction<string>>;
  setIsEditing: Dispatch<SetStateAction<boolean>>;
  setFormData: Dispatch<SetStateAction<TaxonomyCategory>>;
}

const createTaxonomyValue = (
  taxonomies: TaxonomyCategory[],
  parentValue: string,
  displayName: string
) => {
  const regex = /^(\-*)/;
  const taxonomyValue = `--${
    regex.exec(parentValue)?.[1]
  }${displayName.toLowerCase()}`;

  const regexMatch = new RegExp(taxonomyValue);
  const duplicateTaxonomiesLength = taxonomies.filter((taxonomy) =>
    regexMatch.test(taxonomy.value)
  ).length;

  const appendNumber =
    duplicateTaxonomiesLength > 0 ? `_${duplicateTaxonomiesLength}` : "";

  return taxonomyValue + appendNumber;
};

const Title = ({
  title,
  identifier,
  activeNode,
}: {
  title?: string;
  identifier?: string;
  activeNode: string;
}) => {
  const isActive = identifier === activeNode;
  return (
    <div
      className={`main___eWnyJ ${isActive ? "selected___q9To5" : ""}`}
      onClick={() => {}}
    >
      <div
        className={`title___kyvsR ${isActive ? "selected___c22Xv" : ""}`}
        onClick={() => {}}
      >
        {title}
      </div>
    </div>
  );
};

const TaxonomyNode = ({
  taxonomyData,
  level,
  children,
  activeNode,
  setActiveNode,
  setIsEditing,
  setFormData,
}: TaxonomyNode) => {
  const [isExpanded, setIsExpanded] = useState(false);

  return (
    <TreeNode
      isToggleable={children.length > 0}
      isDraggable={false}
      isExpanded={isExpanded}
      onNodeClick={() => {
        setActiveNode(taxonomyData?.value || ROOT_NAME);
        setIsEditing(true);
        setFormData(taxonomyData);
      }}
      onNodeToggle={() => {
        setIsExpanded(!isExpanded);
      }}
      level={level}
      renderNode={() => {
        return (
          <Title
            title={taxonomyData.displayName}
            activeNode={activeNode}
            identifier={taxonomyData.value}
          />
        );
      }}
      nodeIdentifier={taxonomyData.value || "default"}
    >
      {children}
    </TreeNode>
  );
};

export const TaxonomiesTemplate = (props: FormComponentProps) => {
  const [isEditing, setIsEditing] = useState(false);
  const [isCreating, setIsCreating] = useState(false);
  const [rootExpanded, setRootExpanded] = useState(false);
  const [activeNode, setActiveNode] = useState(ROOT_NAME);
  const [taxonomies, setTaxonomies] = useState<TaxonomyCategory[]>([]);
  const [formData, setFormData] = useState<TaxonomyCategory>({
    displayName: "",
    value: "",
    parentValue: "",
  });

  const renderChildNodes = (
    taxonomies: (TaxonomyCategory | undefined)[],
    parent: string,
    level: number
  ) => {
    const nodesToRender: any[] = taxonomies.filter(
      (taxonomy) => taxonomy?.parentValue === parent
    );

    return nodesToRender.map((node, i) => {
      return (
        <TaxonomyNode
          taxonomyData={node}
          level={level}
          key={i}
          activeNode={activeNode}
          setActiveNode={setActiveNode}
          setIsEditing={setIsEditing}
          setFormData={setFormData}
        >
          {renderChildNodes(taxonomies, node.value, level + 1)}
        </TaxonomyNode>
      );
    });
  };

  return (
    <Row>
      <Column width={40}>
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
                label="New Taxonomy"
                size={ButtonSize.S}
                color={ButtonColor.Secondary}
                onClick={() => {
                  setIsCreating(true);
                  setIsEditing(false);
                  setFormData({ displayName: "", parentValue: "", value: "" });
                }}
              />
              <Button
                icon={"xp-bin"}
                size={ButtonSize.S}
                color={ButtonColor.Quinary}
                destructive
                disabled={activeNode === ROOT_NAME}
                onClick={() => {
                  setTaxonomies((taxonomies) => {
                    const nodesToDelete = [];

                    const selectedTaxonomy = taxonomies.find(
                      (taxonomy) => taxonomy.value === activeNode
                    );

                    nodesToDelete.push(selectedTaxonomy);

                    while (nodesToDelete.length > 0) {
                      const currentNode = nodesToDelete.shift();

                      const currentNodeChildren = taxonomies.filter(
                        (child) => child.parentValue === currentNode?.value
                      );

                      currentNodeChildren.forEach((child) => {
                        nodesToDelete.push(child);
                      });

                      taxonomies.splice(
                        taxonomies.findIndex(
                          (taxonomy) => taxonomy.value === currentNode!.value
                        ),
                        1
                      );
                    }

                    return [...taxonomies];
                  });
                  setIsCreating(false);
                  setIsEditing(false);
                }}
              />
            </div>
            <ActionMenuDivider />
            <TreeView>
              <TreeNode
                isToggleable={taxonomies.length > 0}
                onNodeClick={() => {
                  setActiveNode(ROOT_NAME);
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
                      identifier={ROOT_NAME}
                    />
                  );
                }}
                nodeIdentifier={ROOT_NAME}
              >
                {renderChildNodes(taxonomies, ROOT_NAME, 2)}
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
                onClick={() => {
                  if (isCreating) {
                    const data = {
                      displayName: formData.displayName,
                      value: createTaxonomyValue(
                        taxonomies,
                        activeNode,
                        formData.displayName
                      ),
                      parentValue: activeNode,
                    };

                    setTaxonomies((taxonomies) => [...taxonomies, data]);
                  }

                  if (isEditing) {
                    const editTaxonomy = taxonomies.find((taxonomy) => {
                      return taxonomy.value === activeNode;
                    });

                    const editChildren = taxonomies.filter((taxonomy) => {
                      return taxonomy.parentValue === editTaxonomy?.value;
                    });

                    editTaxonomy!.parentValue = formData.parentValue;
                    editTaxonomy!.displayName = formData.displayName;
                    editTaxonomy!.value = createTaxonomyValue(
                      taxonomies,
                      formData.parentValue,
                      formData.displayName
                    );

                    const nodesToUpdate = editChildren.map((child) => {
                      return { newParent: editTaxonomy!.value, data: child };
                    });

                    while (nodesToUpdate.length > 0) {
                      const currentNode = nodesToUpdate.shift();

                      const currentNodeChildren = taxonomies.filter(
                        (taxonomy) =>
                          taxonomy.parentValue === currentNode?.data.value
                      );

                      currentNode!.data.parentValue = currentNode!.newParent;
                      currentNode!.data.value = createTaxonomyValue(
                        taxonomies,
                        currentNode!.newParent,
                        currentNode!.data.displayName
                      );

                      currentNodeChildren.forEach((child) => {
                        nodesToUpdate.push({
                          newParent: currentNode!.data.value,
                          data: child,
                        });
                      });
                    }

                    setActiveNode(editTaxonomy!.value);
                    setTaxonomies([...taxonomies]);
                  }
                }}
                type={ButtonType.Button}
              />
            </div>

            <Paper>
              <div style={{ padding: "16px" }}>
                {(isEditing || isCreating) && (
                  <div>
                    <div className="label-wrapper___AcszK">
                      <label className="label___WET63">Name</label>
                    </div>
                    <div className="component-input___HBr7j">
                      <div className="input-wrapper___Ae7IT">
                        <input
                          type="text"
                          onChange={(evt) => {
                            setFormData((data) => ({
                              ...data,
                              displayName: evt.target.value,
                            }));
                          }}
                          value={formData.displayName}
                        />
                      </div>
                    </div>
                  </div>
                )}

                <br />
                {isEditing && (
                  <Select
                    onChange={(val) => {
                      setFormData((data) => ({
                        ...data,
                        parentValue: val || ROOT_NAME,
                      }));
                    }}
                    label="Parent Category"
                    value={formData.parentValue}
                  >
                    <MenuItem primaryLabel={ROOT_NAME} value={ROOT_NAME} />
                    {taxonomies.map((taxonomy, i) => {
                      if (
                        taxonomy.value === activeNode &&
                        taxonomy.value !== ROOT_NAME
                      )
                        return;

                      const regex = /^(\-*)/;

                      const activeNodeLevel = regex.exec(activeNode);
                      const currentLevel = regex.exec(taxonomy.value);

                      const linkedLevel =
                        activeNodeLevel![1].length >= currentLevel![1].length;

                      if (linkedLevel) {
                        return (
                          <MenuItem
                            key={i}
                            primaryLabel={taxonomy.value}
                            value={taxonomy.value}
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
