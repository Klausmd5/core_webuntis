import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class MainframeService {

  constructor() {
  }

  public sendRequest(method: "GET" | "POST" | "PUT" | "DELETE", path: string, payload: string | null): Promise<MainframeRequestResponse> {
    return new Promise((resolve, reject) => {
      const message: MainframeRequest = {
        method: "sendRequest",
        data: {
          requestId: Math.floor(Math.random() * 1_000_000),
          method,
          path,
          payload
        }
      };
      window.addEventListener("message", (event) => {
        if(event.data.method !== "sendRequest") return;
        const response = event.data as MainframeRequestResponse;
        if (response.data.requestId === message.data.requestId) {
          resolve(response);
        }
      });
      window.parent.postMessage(message, "*");
    });
  }
}

export interface MainframeRequest {
  method: "sendRequest";
  data: {
    requestId: number;
    method: "GET" | "POST" | "PUT" | "DELETE";
    path: string;
    payload: string | null;
  };
}

export interface MainframeRequestResponse {
  method: "sendRequest";
  data: {
    requestId: number;
  } & ({ error: false; message: string; } | { error: true; message: unknown; });
}
