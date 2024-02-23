import { TreeNode } from "@kentico/xperience-admin-components";
import React, { Dispatch, SetStateAction, useState } from "react";

export interface TaxonomyCategory {
  guid: string;
  parentGUID: string;
  displayName: string;
  value: string;
  parentValue: string;
  description: string;
}

export interface TaxonomyNode {
  taxonomyData: TaxonomyCategory;
  level: number;
  children: any;
  activeNode: TaxonomyCategory;
  setActiveNode: Dispatch<SetStateAction<TaxonomyCategory>>;
  setIsEditing: Dispatch<SetStateAction<boolean>>;
  setIsCreating: Dispatch<SetStateAction<boolean>>;
  setFormData: Dispatch<SetStateAction<TaxonomyCategory>>;
}

export const Title = ({
  title,
  identifier,
  activeNode,
}: {
  title?: string;
  identifier?: string;
  activeNode: TaxonomyCategory;
}) => {
  const isActive = identifier === activeNode.guid;
  return (
    <div className={`main___eWnyJ ${isActive ? "selected___q9To5" : ""}`}>
      <div className={`title___kyvsR ${isActive ? "selected___c22Xv" : ""}`}>
        {title}
      </div>
    </div>
  );
};

export const TaxonomyNode = ({
  taxonomyData,
  level,
  children,
  activeNode,
  setActiveNode,
  setIsEditing,
  setIsCreating,
  setFormData,
}: TaxonomyNode) => {
  const [isExpanded, setIsExpanded] = useState(false);

  console.log(taxonomyData);

  return (
    <TreeNode
      isToggleable={children.length > 0}
      isDraggable={false}
      isExpanded={isExpanded}
      onNodeClick={() => {
        setActiveNode(taxonomyData);
        setIsEditing(true);
        setIsCreating(false);
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
            identifier={taxonomyData.guid}
          />
        );
      }}
      nodeIdentifier={taxonomyData.guid || "default"}
    >
      {children}
    </TreeNode>
  );
};
