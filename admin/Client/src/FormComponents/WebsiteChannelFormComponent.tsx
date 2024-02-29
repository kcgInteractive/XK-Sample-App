import React, { useState } from "react";

import { MenuItem, Select } from "@kentico/xperience-admin-components";
import { FormComponentProps } from "@kentico/xperience-admin-base";

export interface DropDownOption {
  value: string;
  text: string;
}

export interface WebsiteChannelFormComponentProps extends FormComponentProps {
  options: DropDownOption[];
}

export const WebsiteChannelFormComponent = (
  props: WebsiteChannelFormComponentProps
) => {
  return (
    <Select
      onChange={(val) => props.onChange?.(val!)}
      label="Select Channel"
      invalid={props.invalid}
      validationMessage={props.validationMessage}
      markAsRequired={props.required}
      placeholder="Choose a channel"
      value={props.value}
    >
      {props.options?.map((item, index) => {
        return (
          <MenuItem primaryLabel={item.text} value={item.value} key={index} />
        );
      })}
    </Select>
  );
};
