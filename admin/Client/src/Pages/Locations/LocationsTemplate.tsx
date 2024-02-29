import {
  ActionMenuDivider,
  Button,
  ButtonColor,
  ButtonSize,
  Cols,
  Column,
  Dialog,
  FormItemWrapper,
  Input,
  MenuItem,
  Paper,
  Row,
  Select,
  TreeNode,
  TreeView,
} from "@kentico/xperience-admin-components";

import React, { useRef, useState } from "react";
import SearchSelect from "react-select";
import { Title } from "../Shared/Title";
import { continents, countries } from "countries-list";
import { PhoneInput, PhoneInputRefType } from "react-international-phone";
import "react-international-phone/style.css";
import { usePageCommand } from "@kentico/xperience-admin-base";
import { TagSelectorFormComponent } from "../../FormComponents/TagSelectorFormComponent";
import { TaxonomyCategory } from "../Taxonomies/TaxonomyNode";
import { State } from "country-state-city";

interface ClientPageTemplateProperties {
  initialLocations: Location[];
  channels: Channel[];
  tags: TaxonomyCategory[];
}

interface ResponseResult {
  locations: Location[];
}

interface Channel {
  channelDisplayName: string;
  channelGUID: string;
}

interface Location {
  locationGUID: string;
  channelGUID: string;
  companyLocationName: string | null;
  region: string;
  countryCode: string;
  country: string;
  stateProvince: string | null;
  stateProvinceCode: string | null;
  city: string;
  street: string | null;
  phone: string | null;
  tags: string | null;
}

const initialFormData: Location = {
  locationGUID: "",
  channelGUID: "",
  companyLocationName: null,
  region: "",
  countryCode: "",
  country: "",
  stateProvince: null,
  stateProvinceCode: null,
  city: "",
  street: null,
  phone: null,
  tags: null,
};

const initialValidation = {
  region: {
    hasError: false,
  },
  country: {
    hasError: false,
  },
  city: {
    hasError: false,
  },

  stateProvince: {
    hasError: false,
  },
};

const createTitleName = (data: Location) => {
  return `${data.region} | ${data.countryCode} | ${
    data.stateProvince ? data.stateProvince + " | " : ""
  } ${data.city}`;
};

export const LocationsTemplate = ({
  initialLocations = [],
  channels = [],
  tags = [],
}: ClientPageTemplateProperties) => {
  const [locations, setLocations] = useState(initialLocations);
  const [isCreating, setIsCreating] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [validation, setValidation] = useState(initialValidation);
  const [expandedNodes, setExpandedNodes] = useState<string[]>([]);
  const [openModal, setOpenModal] = useState(false);

  const [activeNode, setActiveNode] = useState<Channel | Location>(
    initialFormData
  );

  const [formData, setFormData] = useState<Location>(initialFormData);

  const phoneRef = useRef<PhoneInputRefType>(null);

  const { execute: invokeGetAll } = usePageCommand<ResponseResult>(
    "GetAllLocations",
    {
      after: (response) => {
        console.log(response);
        setLocations(response!.locations);
      },
    }
  );
  const { execute: invokeUpdate } = usePageCommand<void, Location>(
    "EditLocation"
  );

  const { execute: invokeSave } = usePageCommand<void, Location>(
    "SaveLocation"
  );

  const { execute: invokeDelete } = usePageCommand<void, Location>(
    "DeleteLocation"
  );

  return (
    <Row>
      <Dialog
        isOpen={openModal}
        headline={"Delete"}
        onClose={() => {
          setOpenModal(false);
        }}
        headerCloseButton={{ tooltipText: "Close" }}
        isDismissable={true}
        confirmAction={{
          label: "confirm",
          onClick: async () => {
            await invokeDelete(formData);
            await invokeGetAll();
            setOpenModal(false);
            setFormData(initialFormData);
            setIsCreating(false);
            setIsEditing(false);
          },
        }}
        cancelAction={{
          label: "cancel",
          onClick: () => {
            setOpenModal(false);
          },
        }}
      >
        {`Are you sure you want to delete '${
          formData.companyLocationName
            ? formData.companyLocationName
            : createTitleName(formData)
        }' ?`}
      </Dialog>
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
                    setFormData({
                      ...initialFormData,
                      channelGUID: activeNode.channelGUID,
                    });
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
                disabled={
                  ("channelDisplayName" in activeNode &&
                    "channelGUID" in activeNode) ||
                  activeNode === initialFormData
                }
                onClick={() => {
                  setOpenModal(true);
                }}
              />
            </div>
            <ActionMenuDivider />
            <TreeView>
              {channels.map((channel) => {
                const nodeIsExpanded = expandedNodes.some(
                  (node) => node === channel.channelGUID
                );
                return (
                  <TreeNode
                    onNodeClick={() => {
                      setActiveNode(channel);
                      setIsEditing(false);
                    }}
                    isExpanded={nodeIsExpanded}
                    onNodeToggle={() => {
                      setExpandedNodes((nodes) => {
                        if (nodeIsExpanded) {
                          return nodes.filter(
                            (node) => node !== channel.channelGUID
                          );
                        } else {
                          return [...nodes, channel.channelGUID];
                        }
                      });
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
                          activeNodeID={
                            "channelDisplayName" in activeNode &&
                            "channelGUID" in activeNode
                              ? activeNode.channelGUID
                              : ""
                          }
                          identifier={channel.channelGUID}
                        />
                      );
                    }}
                    nodeIdentifier={channel.channelGUID}
                  >
                    {locations
                      .filter(
                        (location) =>
                          location.channelGUID === channel.channelGUID
                      )
                      .map((location) => {
                        return (
                          <TreeNode
                            onNodeClick={() => {
                              setActiveNode(location);
                              setFormData(location);
                              setIsCreating(false);
                              setIsEditing(true);
                            }}
                            isToggleable={false}
                            key={location.locationGUID}
                            isDraggable={false}
                            level={2}
                            renderNode={() => {
                              return (
                                <Title
                                  title={
                                    location?.companyLocationName ||
                                    createTitleName(location)
                                  }
                                  activeNodeID={
                                    (activeNode as Location).locationGUID
                                  }
                                  identifier={location.locationGUID}
                                />
                              );
                            }}
                            nodeIdentifier={location.locationGUID}
                          />
                        );
                      })}
                  </TreeNode>
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
              <Button
                label="Save"
                color={ButtonColor.Primary}
                onClick={async () => {
                  setValidation(initialValidation);
                  let formHasErrors = false;
                  if (formData.region === "") {
                    setValidation((data) => ({
                      ...data,
                      region: {
                        hasError: true,
                      },
                    }));

                    formHasErrors = true;
                  }

                  if (formData.region && formData.country === "") {
                    setValidation((data) => ({
                      ...data,
                      country: { hasError: true },
                    }));

                    formHasErrors = true;
                  }

                  if (formData.country && formData.city === "") {
                    setValidation((data) => ({
                      ...data,
                      city: { hasError: true },
                    }));

                    formHasErrors = true;
                  }

                  if (
                    State.getStatesOfCountry(formData.countryCode).length > 0 &&
                    !formData.stateProvince
                  ) {
                    setValidation((data) => ({
                      ...data,
                      stateProvince: { hasError: true },
                    }));

                    formHasErrors = true;
                  }

                  if (formHasErrors) return;

                  if (isCreating) {
                    await invokeSave(formData);
                  } else {
                    await invokeUpdate(formData);
                  }

                  setIsCreating(false);
                  await invokeGetAll();
                }}
              />
            </div>
            <Paper>
              <div style={{ padding: "16px" }}>
                <Select
                  invalid={validation.region.hasError}
                  markAsRequired
                  validationMessage="Please select a region"
                  placeholder="Select a region"
                  label="Region"
                  onChange={(val) => {
                    if (val) {
                      setFormData((data) => {
                        return {
                          ...data,
                          region: val!,
                          country: "",
                          countryCode: "",
                        };
                      });
                    }
                  }}
                  value={formData.region || ""}
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

                {formData.region && (
                  <>
                    <FormItemWrapper
                      markAsRequired
                      label="Country"
                      invalid={validation.country.hasError}
                      validationMessage="Please select a country"
                    >
                      <SearchSelect
                        placeholder="Select a country"
                        value={
                          formData.countryCode
                            ? {
                                value: formData.countryCode,
                                label: formData.country,
                              }
                            : null
                        }
                        onChange={(inputData) => {
                          phoneRef.current?.setCountry(
                            (inputData?.value as string).toLowerCase()
                          );

                          setFormData((data) => ({
                            ...data,
                            countryCode: inputData?.value as string,
                            country: Object.entries(countries).find(
                              (country) => country[0] === inputData?.value
                            )![1].name,
                            stateProvince: null,
                            stateProvinceCode: null,
                          }));
                        }}
                        styles={{
                          container: (baseStyles) => {
                            return { ...baseStyles, color: "#151515" };
                          },
                        }}
                        options={Object.entries(countries)
                          .filter((country) => {
                            return country[1].continent === formData.region;
                          })
                          .map((country) => {
                            return {
                              label: country[1].name,
                              value: country[0],
                            };
                          })}
                      />
                    </FormItemWrapper>
                    <br />
                  </>
                )}

                {State.getStatesOfCountry(formData.countryCode).length > 0 && (
                  <>
                    <FormItemWrapper
                      label="State/Province"
                      markAsRequired
                      invalid={validation.stateProvince.hasError}
                      validationMessage="Please select a State/Province"
                    >
                      <SearchSelect
                        placeholder="Select a state"
                        value={
                          formData.stateProvinceCode
                            ? {
                                value: formData.stateProvinceCode,
                                label: formData.stateProvince,
                              }
                            : null
                        }
                        onChange={(inputData) => {
                          setFormData((data) => ({
                            ...data,
                            stateProvince: State.getStateByCodeAndCountry(
                              inputData!.value as string,
                              formData.countryCode
                            )!.name,
                            stateProvinceCode: inputData!.value,
                          }));
                        }}
                        options={State.getStatesOfCountry(
                          formData.countryCode
                        ).map((state) => ({
                          label: state.name,
                          value: state.isoCode,
                        }))}
                        styles={{
                          container: (baseStyles) => {
                            return { ...baseStyles, color: "#151515" };
                          },
                        }}
                      />
                    </FormItemWrapper>
                    <br />
                  </>
                )}
                {formData.country && (
                  <>
                    <Input
                      markAsRequired
                      label="City"
                      type="text"
                      invalid={validation.city.hasError}
                      validationMessage="Cannot be empty"
                      onChange={(evt) => {
                        setFormData((data) => ({
                          ...data,
                          city: evt.target.value,
                        }));
                      }}
                      value={formData.city || ""}
                    />
                    <br />
                    <Input
                      label="Street"
                      type="text"
                      onChange={(evt) => {
                        setFormData((data) => ({
                          ...data,
                          street: evt.target.value,
                        }));
                      }}
                      value={formData.street || ""}
                    />
                    <br />
                    <FormItemWrapper label="Phone Number">
                      <div className="component-input___HBr7j">
                        <div className="input-wrapper___Ae7IT">
                          <PhoneInput
                            value={formData.phone || ""}
                            onChange={(val) => {
                              setFormData((data) => ({ ...data, phone: val }));
                            }}
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
                    <Input
                      label="Company Location Name"
                      type="text"
                      onChange={(evt) => {
                        setFormData((data) => ({
                          ...data,
                          companyLocationName: evt.target.value,
                        }));
                      }}
                      value={formData.companyLocationName || ""}
                    />
                    <br />

                    <TagSelectorFormComponent
                      tags={tags}
                      value={formData.tags}
                      onChange={(val) => {
                        setFormData((data) => ({
                          ...data,
                          tags: val as string,
                        }));
                      }}
                      hasDependencies={false}
                      guid={"28287532-56e1-467c-af38-d830c5142fb6"}
                      componentName={"@sample/web-admin/TagSelector"}
                      name={"Tag Selector"}
                      fieldValues={{}}
                    />
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
