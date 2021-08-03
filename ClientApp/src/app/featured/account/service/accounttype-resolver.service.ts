import { Injectable } from "@angular/core";
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { AccountTypeDDL } from "src/app/infrastructure/model/UserManagement/resource/account/account-type-model";
import { AccountService } from "./account.service";

@Injectable({
  providedIn: "root",
})
export class AccountTypeResolverService
  implements Resolve<Observable<AccountTypeDDL[]>>
{
  constructor(private accountService: AccountService) {}
  resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) {
    var x = this.accountService.getAccountTypeDDL();
    console.log(x);
    debugger;
    return x;
  }
}
