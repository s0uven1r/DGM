import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";
import {
  RoutingComponent,
  AccountTypeCreateRoutingModule,
} from "./account-type-create-routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { AccountService } from "../../service/account.service";
import { DataTablesModule } from "angular-datatables";

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AccountTypeCreateRoutingModule,
    CheckDirectiveModule
  ],
  providers: [
    {
      provide: AccountService,
      useClass: AccountService,
    },
  ],
})
export class AccountTypeCreateModule {}
