import * as coreClient from "@azure/core-client";
import * as coreAuth from "@azure/core-auth";
import * as Parameters from "./models/parameters";
import * as Mappers from "./models/mappers";
import { AcmeApiContext } from "./acmeApiContext";
import {
  AcmeApiOptionalParams,
  AcmeApiGetActivitiesOptionalParams,
  AcmeApiGetActivitiesResponse,
  AcmeApiGetParticipantsOptionalParams,
  AcmeApiGetParticipantsResponse,
  AcmeApiSignUpOptionalParams
} from "./models";

export class AcmeApi extends AcmeApiContext {
  /**
   * Initializes a new instance of the AcmeApi class.
   * @param credentials Subscription credentials which uniquely identify client subscription.
   * @param $host server parameter
   * @param options The parameter options
   */
  constructor(
    credentials: coreAuth.TokenCredential,
    $host: string,
    options?: AcmeApiOptionalParams
  ) {
    super(credentials, $host, options);
  }

  /** @param options The options parameters. */
  getActivities(
    options?: AcmeApiGetActivitiesOptionalParams
  ): Promise<AcmeApiGetActivitiesResponse> {
    return this.sendOperationRequest({ options }, getActivitiesOperationSpec);
  }

  /**
   * @param activityId
   * @param options The options parameters.
   */
  getParticipants(
    activityId: number,
    options?: AcmeApiGetParticipantsOptionalParams
  ): Promise<AcmeApiGetParticipantsResponse> {
    return this.sendOperationRequest(
      { activityId, options },
      getParticipantsOperationSpec
    );
  }

  /**
   * @param activityId
   * @param options The options parameters.
   */
  signUp(
    activityId: number,
    options?: AcmeApiSignUpOptionalParams
  ): Promise<void> {
    return this.sendOperationRequest(
      { activityId, options },
      signUpOperationSpec
    );
  }
}
// Operation Specifications
const serializer = coreClient.createSerializer(Mappers, /* isXml */ false);

const getActivitiesOperationSpec: coreClient.OperationSpec = {
  path: "/api/Activity",
  httpMethod: "GET",
  responses: {
    200: {
      bodyMapper: {
        type: {
          name: "Sequence",
          element: { type: { name: "Composite", className: "Activity" } }
        }
      }
    }
  },
  queryParameters: [Parameters.searchKeyword],
  urlParameters: [Parameters.$host],
  headerParameters: [Parameters.accept],
  serializer
};
const getParticipantsOperationSpec: coreClient.OperationSpec = {
  path: "/api/Activity/{activityId}/participants",
  httpMethod: "GET",
  responses: {
    200: {
      bodyMapper: {
        type: {
          name: "Sequence",
          element: { type: { name: "Composite", className: "Participant" } }
        }
      }
    }
  },
  urlParameters: [Parameters.$host, Parameters.activityId],
  headerParameters: [Parameters.accept],
  serializer
};
const signUpOperationSpec: coreClient.OperationSpec = {
  path: "/api/Activity/{activityId}/sign-up",
  httpMethod: "POST",
  responses: { 200: {} },
  requestBody: Parameters.body,
  urlParameters: [Parameters.$host, Parameters.activityId],
  headerParameters: [Parameters.contentType],
  mediaType: "json",
  serializer
};
