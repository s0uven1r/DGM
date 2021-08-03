import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";
import {
  RoutingComponent,
  AccountTypeRoutingModule,
} from "./account-type-routing.module";
import { AccountService } from "../service/account.service";
import { DataTablesModule } from "angular-datatables";

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    AccountTypeRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [
    {
      provide: AccountService,
      useClass: AccountService,
    },
  ],
})
export class AccountTypeModule {}
