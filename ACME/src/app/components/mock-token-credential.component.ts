import * as coreAuth from "@azure/core-auth";

export class MockTokenCredential implements coreAuth.TokenCredential {
    getToken(scopes: string | string[], options?: coreAuth.GetTokenOptions): Promise<coreAuth.AccessToken | null> {
        return new Promise<coreAuth.AccessToken>((resolve, reject) => { resolve(new MockAccessToken())});
    }
}

export class MockAccessToken implements coreAuth.AccessToken {
    token: string;
    expiresOnTimestamp: number;
    
    constructor(){
        this.token = '';
        this.expiresOnTimestamp = 3600;
    }
}