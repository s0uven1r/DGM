import { Injectable } from "@angular/core";
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { AccountTypeModel } from "src/app/infrastructure/model/UserManagement/resource/account/account-type-model";
import { AccountService } from "./account.service";

@Injectable({
  providedIn: "root",
})
export class AccountHeadResolverService
  implements Resolve<Observable<AccountTypeModel[]>>
{
  constructor(private accountService: AccountService) {}
  resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) {
    return this.accountService.getAllAccountType();
  }
}
