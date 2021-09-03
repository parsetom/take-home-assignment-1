import * as coreClient from "@azure/core-client";

export const Activity: coreClient.CompositeMapper = {
  type: {
    name: "Composite",
    className: "Activity",
    modelProperties: {
      id: {
        serializedName: "id",
        type: {
          name: "Number"
        }
      },
      name: {
        serializedName: "name",
        nullable: true,
        type: {
          name: "String"
        }
      },
      startDate: {
        serializedName: "startDate",
        type: {
          name: "DateTime"
        }
      },
      endDate: {
        serializedName: "endDate",
        type: {
          name: "DateTime"
        }
      },
      registrationDeadline: {
        serializedName: "registrationDeadline",
        type: {
          name: "DateTime"
        }
      },
      registrations: {
        serializedName: "registrations",
        nullable: true,
        type: {
          name: "Sequence",
          element: {
            type: {
              name: "Composite",
              className: "ActivityRegistration"
            }
          }
        }
      }
    }
  }
};

export const ActivityRegistration: coreClient.CompositeMapper = {
  type: {
    name: "Composite",
    className: "ActivityRegistration",
    modelProperties: {
      id: {
        serializedName: "id",
        type: {
          name: "Number"
        }
      },
      person: {
        serializedName: "person",
        type: {
          name: "Composite",
          className: "Person"
        }
      },
      activity: {
        serializedName: "activity",
        type: {
          name: "Composite",
          className: "Activity"
        }
      },
      personId: {
        serializedName: "personId",
        type: {
          name: "Number"
        }
      },
      activityId: {
        serializedName: "activityId",
        type: {
          name: "Number"
        }
      },
      comments: {
        serializedName: "comments",
        nullable: true,
        type: {
          name: "String"
        }
      }
    }
  }
};

export const Person: coreClient.CompositeMapper = {
  type: {
    name: "Composite",
    className: "Person",
    modelProperties: {
      id: {
        serializedName: "id",
        type: {
          name: "Number"
        }
      },
      firstName: {
        serializedName: "firstName",
        nullable: true,
        type: {
          name: "String"
        }
      },
      lastName: {
        serializedName: "lastName",
        nullable: true,
        type: {
          name: "String"
        }
      },
      email: {
        serializedName: "email",
        nullable: true,
        type: {
          name: "String"
        }
      }
    }
  }
};

export const Participant: coreClient.CompositeMapper = {
  type: {
    name: "Composite",
    className: "Participant",
    modelProperties: {
      registrationId: {
        serializedName: "registrationId",
        type: {
          name: "Number"
        }
      },
      personId: {
        serializedName: "personId",
        type: {
          name: "Number"
        }
      },
      name: {
        serializedName: "name",
        nullable: true,
        type: {
          name: "String"
        }
      },
      email: {
        serializedName: "email",
        nullable: true,
        type: {
          name: "String"
        }
      },
      comments: {
        serializedName: "comments",
        nullable: true,
        type: {
          name: "String"
        }
      },
      activityName: {
        serializedName: "activityName",
        nullable: true,
        type: {
          name: "String"
        }
      }
    }
  }
};

export const SignUpInformation: coreClient.CompositeMapper = {
  type: {
    name: "Composite",
    className: "SignUpInformation",
    modelProperties: {
      firstName: {
        constraints: {
          MaxLength: 50
        },
        serializedName: "firstName",
        required: true,
        type: {
          name: "String"
        }
      },
      lastName: {
        constraints: {
          MaxLength: 50
        },
        serializedName: "lastName",
        required: true,
        type: {
          name: "String"
        }
      },
      email: {
        constraints: {
          MaxLength: 50
        },
        serializedName: "email",
        required: true,
        type: {
          name: "String"
        }
      },
      activityId: {
        serializedName: "activityId",
        required: true,
        type: {
          name: "Number"
        }
      },
      comments: {
        constraints: {
          MaxLength: 255
        },
        serializedName: "comments",
        nullable: true,
        type: {
          name: "String"
        }
      }
    }
  }
};
