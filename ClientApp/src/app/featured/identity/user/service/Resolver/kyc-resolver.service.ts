import { Injectable } from "@angular/core";
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { UserService } from "../user.service";

@Injectable({
  providedIn: "root",
})
export class KycResolverService implements Resolve<Observable<any[]>> {
  constructor(private userService: UserService) {}
  resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) {
    var x = this.userService.getKYCDDL();
    return x;
  }
}
