import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AccountService } from "../../service/account.service";
import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";
import { ReactiveFormsModule } from "@angular/forms";
import {
  RoutingComponent,
  AccountHeadCreateRoutingModule,
} from "./account-head-create-routing.module";

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AccountHeadCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: AccountService,
      useClass: AccountService,
    },
  ],
})
export class AccountHeadCreateModule {}
