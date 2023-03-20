import {Injectable} from "@angular/core";
import {MainframeService} from "./mainframe.service";

@Injectable({
  providedIn: "root"
})
export class WebUntisService {
  constructor(private mainframe: MainframeService) {
  }
}
