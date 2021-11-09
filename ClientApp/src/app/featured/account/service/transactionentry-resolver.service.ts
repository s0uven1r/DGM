import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { TransactionEntryModel } from "src/app/infrastructure/model/UserManagement/resource/account/transaction-entry-model";
import { AccountService } from "./account.service";

@Injectable({
    providedIn: 'root'
  })
  export class TransactionEntryResolverService implements Resolve <Observable<TransactionEntryModel[]>>{
  
    constructor( private accountService: AccountService ) {
      }
      resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot){
        return this.accountService.getAllTransactionEntries();
      }
  
  }