import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { PermissionService } from "src/app/featured/identity/permission/service/permission.service";
import { CheckclaimDirective } from "./checkclaim.directive";

@NgModule({
    declarations: [CheckclaimDirective],
    imports: [
      CommonModule
    ],
    providers: [{
      provide: PermissionService,
      useClass: PermissionService
    }],
    exports: [CheckclaimDirective]
  })
  export class CheckDirectiveModule { }
