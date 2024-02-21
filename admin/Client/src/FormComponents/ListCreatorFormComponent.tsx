import React from "react";
import { FormComponentProps } from "@kentico/xperience-admin-base";
import { Checkbox, CheckboxSize } from "@kentico/xperience-admin-components";

export interface Taxonomy {
  categoryID: number;
  name: string;
}

export interface TaxonomyCategory {
  id: number;
  name: string;
}

export interface ListCreatorFormComponentProps extends FormComponentProps {
  categories: TaxonomyCategory[];
  taxonomies: Taxonomy[];
}

export const ListCreatorFormComponent = (
  props: ListCreatorFormComponentProps
) => {
  return (
    <div>
      <span className="label___WET63">{props.name}</span>
      {props.categories.map((category, i) => {
        return (
          <div key={category.name + i}>
            <h3 style={{ color: "black" }}>{category.name}</h3>
            {props.taxonomies.map((taxonomy) => {
              if (taxonomy.categoryID === category.id) {
                return (
                  <div>
                    <Checkbox
                      label={taxonomy.name}
                      onChange={() => props.onChange?.("")}
                    />
                  </div>
                );
              }
            })}
          </div>
        );
      })}
    </div>
  );
};
