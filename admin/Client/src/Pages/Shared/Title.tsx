import React from "react";

export const Title = ({
  title,
  identifier,
  activeNode,
}: {
  title?: string;
  identifier?: string;
  activeNode: { guid: string };
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
