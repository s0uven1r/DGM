import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";
import { ReactiveFormsModule } from "@angular/forms";
import { AccountService } from "../service/account.service";
import {
  RoutingComponent,
  AccountHeadRoutingModule,
} from "./account-head-routing.module";

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AccountHeadRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: AccountService,
      useClass: AccountService,
    },
  ],
})
export class AccountHeadModule {}
