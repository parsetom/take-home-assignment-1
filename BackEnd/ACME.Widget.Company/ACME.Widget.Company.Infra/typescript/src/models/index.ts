import * as coreClient from "@azure/core-client";

export interface Activity {
  id?: number;
  name?: string;
  startDate?: Date;
  endDate?: Date;
  registrationDeadline?: Date;
  registrations?: ActivityRegistration[];
}

export interface ActivityRegistration {
  id?: number;
  person?: Person;
  activity?: Activity;
  personId?: number;
  activityId?: number;
  comments?: string;
}

export interface Person {
  id?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
}

export interface Participant {
  registrationId?: number;
  personId?: number;
  name?: string;
  email?: string;
  comments?: string;
  activityName?: string;
}

export interface SignUpInformation {
  firstName: string;
  lastName: string;
  email: string;
  activityId: number;
  comments?: string;
}

/** Optional parameters. */
export interface AcmeApiGetActivitiesOptionalParams
  extends coreClient.OperationOptions {
  searchKeyword?: string;
}

/** Contains response data for the getActivities operation. */
export type AcmeApiGetActivitiesResponse = Activity[];

/** Optional parameters. */
export interface AcmeApiGetParticipantsOptionalParams
  extends coreClient.OperationOptions {}

/** Contains response data for the getParticipants operation. */
export type AcmeApiGetParticipantsResponse = Participant[];

/** Optional parameters. */
export interface AcmeApiSignUpOptionalParams
  extends coreClient.OperationOptions {
  body?: SignUpInformation;
}

/** Optional parameters. */
export interface AcmeApiOptionalParams extends coreClient.ServiceClientOptions {
  /** Overrides client endpoint. */
  endpoint?: string;
}
