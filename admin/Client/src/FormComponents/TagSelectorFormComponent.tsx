import { FormComponentProps } from "@kentico/xperience-admin-base";
import React, { useEffect, useState } from "react";
import { TaxonomyCategory } from "src/Pages/Taxonomies/TaxonomyNode";
import SearchSelect from "react-select";
import { FormItemWrapper } from "@kentico/xperience-admin-components";
import { TreeSelect } from "primereact/treeselect";
import "primereact/resources/themes/lara-light-blue/theme.css";
import { EMPTY_GUID } from "../contants";

export interface TagSelectorFormComponentProps extends FormComponentProps {
  tags: TaxonomyCategory[];
}

export interface TagNode {
  key: string;
  label: string;
  data: string;

  children?: TagNode[];
}

const tagMapper = (taxonomies: TaxonomyCategory[]): TagNode[] => {
  return taxonomies.map((taxonomy) => ({
    key: taxonomy.guid,
    label: taxonomy.displayName,
    data: taxonomy.value,
    children: [],
  }));
};

const createTagTree = (allTags: TaxonomyCategory[]) => {
  const rootElements: TaxonomyCategory[] = allTags.filter(
    (tag) => tag.parentGUID === EMPTY_GUID
  );
  const mappedRootElements = tagMapper(rootElements);
  mappedRootElements.forEach((rootElement) => {
    const children: TaxonomyCategory[] = allTags.filter(
      (tag) => tag.parentGUID === rootElement.key
    );

    const mappedChildren = tagMapper(children);
    rootElement.children = mappedChildren;

    const nodeQueue = [...mappedChildren];

    while (nodeQueue.length > 0) {
      const node = nodeQueue.shift();

      if (!node) return;

      const nodeChildren = allTags.filter((tag) => tag.parentGUID === node.key);

      const mappedNodeChildren = tagMapper(nodeChildren);

      node.children = mappedNodeChildren;

      mappedNodeChildren.forEach((child) => {
        nodeQueue.push(child);
      });
    }
  });

  return mappedRootElements;
};

export const TagSelectorFormComponent = ({
  tags,
  onChange,
  value,
}: TagSelectorFormComponentProps) => {
  const [selectedTags, setSelectedTags] = useState(
    value ? JSON.parse(value) : {}
  );
  const [nodes, setNodes] = useState<TagNode[]>();

  useEffect(() => {
    const mappedNodes = createTagTree(tags);
    console.log(mappedNodes);
    setNodes(mappedNodes);
  }, []);

  return (
    <FormItemWrapper label="Tags">
      <div className="card flex justify-content-center">
        <TreeSelect
          showClear
          filter
          value={selectedTags}
          onChange={(e) => {
            if (e.value) {
              setSelectedTags(e.value);
              onChange?.(JSON.stringify(e.value));
            } else {
              setSelectedTags({});
              onChange?.(JSON.stringify({}));
            }
          }}
          options={nodes}
          metaKeySelection={false}
          selectionMode="checkbox"
          pt={{
            clearIcon: {
              style: { position: "absolute", top: "24px", right: "25px" },
            },
            trigger: {
              style: { display: "none" },
            },
            label: {
              style: { whiteSpace: "unset" },
            },
            token: {
              style: { margin: "5px 5px 2px 0" },
            },
          }}
          style={{ width: "100%" }}
          display="chip"
          placeholder="Select Tags"
        ></TreeSelect>
      </div>
    </FormItemWrapper>
  );
};
