import React, { useState } from "react";

import { MenuItem, Select } from "@kentico/xperience-admin-components";
import { FormComponentProps } from "@kentico/xperience-admin-base";

export interface DropDownOption {
  value: string;
  text: string;
}

export interface MyFormComponentProps extends FormComponentProps {
  options: DropDownOption[];
}

export const WebsiteChannelFormComponent = (props: MyFormComponentProps) => {
  return (
    <Select
      onChange={(val) => props.onChange?.(val)}
      label="Select Channel"
      invalid={props.invalid}
      validationMessage={props.validationMessage}
      markAsRequired={props.required}
      placeholder="Choose a channel"
    >
      {props.options?.map((item, index) => {
        return (
          <MenuItem primaryLabel={item.text} value={item.value} key={index} />
        );
      })}
    </Select>
  );
};
