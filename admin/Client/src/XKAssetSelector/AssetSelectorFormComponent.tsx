import React, { Suspense, useState } from "react";
import { MyFormComponentProps } from "src/WebsiteChannelFormComponent";

const Assets = () => {
  const data = fetch("http://localhost:5000/api/select");

  data.then((res) => res.json()).then((data) => console.log(data));
  return <div></div>;
};

export const AssetSelectorFormComponent = (props: MyFormComponentProps) => {
  return (
    <div>
      <Suspense fallback={<div>Loading...</div>}>
        <Assets />
      </Suspense>
    </div>
  );
};
