import React from "react";

export const Title = ({
  title,
  identifier,
  activeNodeID,
}: {
  title?: string;
  identifier: string;
  activeNodeID: string;
}) => {
  const isActive = identifier === activeNodeID;
  return (
    <div className={`main___eWnyJ ${isActive ? "selected___q9To5" : ""}`}>
      <div className={`title___kyvsR ${isActive ? "selected___c22Xv" : ""}`}>
        {title}
      </div>
    </div>
  );
};
