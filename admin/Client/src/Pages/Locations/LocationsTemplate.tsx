import {
  ActionMenuDivider,
  Button,
  ButtonColor,
  ButtonSize,
  Cols,
  Column,
  FormItemWrapper,
  Input,
  KXIcons,
  MenuItem,
  Paper,
  Row,
  Select,
  TextWithLabel,
  TreeNode,
  TreeView,
} from "@kentico/xperience-admin-components";

import { Dropdown } from "semantic-ui-react";
import React, { useRef, useState } from "react";
import states from "states-us";
import { Title } from "../Shared/Title";
import { TContinents, TCountries, continents, countries } from "countries-list";
import {
  CountryIso2,
  PhoneInput,
  PhoneInputRefType,
} from "react-international-phone";
import "react-international-phone/style.css";
import "semantic-ui-css/semantic.min.css";
import "./location-styles.css";

interface ClientPageTemplateProperties {
  initialLocations: Location[];
  channels: Channel[];
}

interface Channel {
  channelDisplayName: string;
  channelGUID: string;
}

interface Location {
  locationGUID: string;
  channelGUID: string;
  companyLocationName: string;
  region: string;
  countryCode: string;
  country: string;
  state?: string | null;
  city: string;
  street: string;
  telephone: string | null;
}

const initialFormData: Location = {
  locationGUID: "",
  channelGUID: "",
  companyLocationName: "",
  region: "",
  countryCode: "",
  country: "",
  state: null,
  city: "",
  street: "",
  telephone: null,
};

export const LocationsTemplate = ({
  initialLocations = [],
  channels = [],
}: ClientPageTemplateProperties) => {
  const [locations, setLocations] = useState(initialLocations);
  const [isCreating, setIsCreating] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  const [activeNode, setActiveNode] = useState<Channel | Location>(
    initialFormData
  );
  const [formData, setFormData] = useState<Location>(initialFormData);
  const phoneRef = useRef<PhoneInputRefType>(null);

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
                onClick={() => {
                  if (
                    "channelDisplayName" in activeNode &&
                    "channelGUID" in activeNode
                  ) {
                    setIsCreating(true);
                  }
                }}
                disabled={
                  !(
                    "channelDisplayName" in activeNode &&
                    "channelGUID" in activeNode
                  )
                }
                label="New Location"
                color={ButtonColor.Secondary}
                size={ButtonSize.S}
              />
              <Button
                icon={"xp-bin"}
                size={ButtonSize.S}
                color={ButtonColor.Quinary}
                destructive
                disabled={true}
                onClick={() => {}}
              />
            </div>
            <ActionMenuDivider />
            <TreeView>
              {channels.map((channel) => {
                return (
                  <TreeNode
                    onNodeClick={() => {
                      setActiveNode(channel);
                    }}
                    isToggleable={locations.some(
                      (location) => location.channelGUID === channel.channelGUID
                    )}
                    key={channel.channelGUID}
                    isDraggable={false}
                    level={1}
                    renderNode={() => {
                      return (
                        <Title
                          title={channel.channelDisplayName}
                          activeNode={{ guid: activeNode.channelGUID }}
                          identifier={channel.channelGUID}
                        />
                      );
                    }}
                    nodeIdentifier={channel.channelGUID}
                  ></TreeNode>
                );
              })}
            </TreeView>
          </div>
        </Paper>
      </Column>
      {(isCreating || isEditing) && (
        <Column cols={Cols.Col3}>
          <div style={{ margin: "0 16px" }}>
            <div style={{ marginBottom: "16px" }}>
              <Button label="Save" color={ButtonColor.Primary} />
            </div>
            <Paper>
              <div style={{ padding: "16px" }}>
                <Input
                  label="Company Location Name"
                  type="text"
                  validationMessage="Cannot be empty"
                  markAsRequired={true}
                  onChange={(evt) => {
                    setFormData((data) => ({
                      ...data,
                      companyLocationName: evt.target.value,
                    }));
                  }}
                  value={formData.companyLocationName}
                />
                <br />
                <Select
                  placeholder="Select a region"
                  label="Region"
                  onChange={(val) => {
                    if (val) {
                      setFormData((data) => {
                        return { ...data, region: val! };
                      });
                    }
                  }}
                  value={""}
                >
                  {Object.entries(continents).map((continentItem) => {
                    return (
                      <MenuItem
                        key={continentItem[0]}
                        primaryLabel={continentItem[1]}
                        value={continentItem[0]}
                      />
                    );
                  })}
                </Select>
                <br />

                {formData.region !== "" && (
                  <FormItemWrapper label="Country">
                    <Dropdown
                      placeholder="Select a country"
                      fluid
                      search
                      onChange={(evt, inputData) => {
                        phoneRef.current?.setCountry(
                          (inputData?.value as string).toLowerCase()
                        );
                        setFormData((data) => ({
                          ...data,
                          countryCode: inputData.value as string,
                          country: Object.entries(countries).find(
                            (country) => country[0] === inputData.value
                          )![1].name,
                        }));
                      }}
                      icon={
                        <svg
                          className="dropdown icon"
                          style={{ marginTop: "4px" }}
                          width="1em"
                          height="1em"
                          viewBox="0 0 16 16"
                          fill="none"
                          xmlns="http://www.w3.org/2000/svg"
                          role="img"
                        >
                          <path
                            fill-rule="evenodd"
                            clip-rule="evenodd"
                            d="M14.92 5.724a.504.504 0 0 1-.14.696l-6.5 4.996a.498.498 0 0 1-.554 0l-6.5-4.996a.504.504 0 0 1-.138-.696.499.499 0 0 1 .693-.14l6.222 4.81 6.223-4.81a.499.499 0 0 1 .693.14Z"
                            fill="currentColor"
                          ></path>
                        </svg>
                      }
                      className="input-wrapper___Ae7IT dropdown-custom"
                      options={Object.entries(countries)
                        .filter((country) => {
                          return country[1].continent === formData.region;
                        })
                        .map((country) => {
                          return { text: country[1].name, value: country[0] };
                        })}
                    />
                    <br />
                  </FormItemWrapper>
                )}

                {formData.countryCode === "US" && (
                  <>
                    <FormItemWrapper label="State">
                      <Dropdown
                        placeholder="Select a state"
                        fluid
                        search
                        onChange={(evt, inputData) => {
                          setFormData((data) => ({
                            ...data,
                            state: inputData.value as string,
                          }));
                        }}
                        icon={
                          <svg
                            className="dropdown icon"
                            style={{ marginTop: "4px" }}
                            width="1em"
                            height="1em"
                            viewBox="0 0 16 16"
                            fill="none"
                            xmlns="http://www.w3.org/2000/svg"
                            role="img"
                          >
                            <path
                              fill-rule="evenodd"
                              clip-rule="evenodd"
                              d="M14.92 5.724a.504.504 0 0 1-.14.696l-6.5 4.996a.498.498 0 0 1-.554 0l-6.5-4.996a.504.504 0 0 1-.138-.696.499.499 0 0 1 .693-.14l6.222 4.81 6.223-4.81a.499.499 0 0 1 .693.14Z"
                              fill="currentColor"
                            ></path>
                          </svg>
                        }
                        className="input-wrapper___Ae7IT dropdown-custom"
                        options={states.map((state) => ({
                          text: state.name,
                          value: state.name,
                        }))}
                      />
                    </FormItemWrapper>
                    <br />
                  </>
                )}
                {formData.country && (
                  <>
                    <Input
                      label="City"
                      type="text"
                      validationMessage="Cannot be empty"
                      markAsRequired={true}
                      onChange={(evt) => {
                        setFormData((data) => ({
                          ...data,
                          city: evt.target.value,
                        }));
                      }}
                      value={formData.companyLocationName}
                    />
                    <br />
                    <Input
                      label="Street"
                      type="text"
                      validationMessage="Cannot be empty"
                      markAsRequired={true}
                      onChange={(evt) => {
                        setFormData((data) => ({
                          ...data,
                          street: evt.target.value,
                        }));
                      }}
                      value={formData.companyLocationName}
                    />
                    <br />
                    <FormItemWrapper label="Phone Number">
                      <div className="component-input___HBr7j">
                        <div className="input-wrapper___Ae7IT">
                          <PhoneInput
                            ref={phoneRef}
                            defaultCountry={"us"}
                            style={{ marginLeft: "20px", width: "100%" }}
                            inputStyle={{ marginLeft: 0 }}
                            countrySelectorStyleProps={{
                              buttonStyle: {
                                backgroundColor: "transparent",
                                border: "none",
                              },
                            }}
                          />
                        </div>
                      </div>
                    </FormItemWrapper>
                    <br />
                  </>
                )}
              </div>
            </Paper>
          </div>
        </Column>
      )}
    </Row>
  );
};
