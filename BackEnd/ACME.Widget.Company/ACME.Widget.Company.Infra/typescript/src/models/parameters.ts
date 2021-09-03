import {
  OperationParameter,
  OperationURLParameter,
  OperationQueryParameter
} from "@azure/core-client";
import { SignUpInformation as SignUpInformationMapper } from "../models/mappers";

export const accept: OperationParameter = {
  parameterPath: "accept",
  mapper: {
    defaultValue: "application/json, text/json",
    isConstant: true,
    serializedName: "Accept",
    type: {
      name: "String"
    }
  }
};

export const $host: OperationURLParameter = {
  parameterPath: "$host",
  mapper: {
    serializedName: "$host",
    required: true,
    type: {
      name: "String"
    }
  },
  skipEncoding: true
};

export const searchKeyword: OperationQueryParameter = {
  parameterPath: ["options", "searchKeyword"],
  mapper: {
    serializedName: "searchKeyword",
    type: {
      name: "String"
    }
  }
};

export const activityId: OperationURLParameter = {
  parameterPath: "activityId",
  mapper: {
    serializedName: "activityId",
    required: true,
    type: {
      name: "Number"
    }
  }
};

export const contentType: OperationParameter = {
  parameterPath: ["options", "contentType"],
  mapper: {
    defaultValue: "application/json",
    isConstant: true,
    serializedName: "Content-Type",
    type: {
      name: "String"
    }
  }
};

export const body: OperationParameter = {
  parameterPath: ["options", "body"],
  mapper: SignUpInformationMapper
};
