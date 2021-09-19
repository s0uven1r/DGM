import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import {
  RoutingComponent,
  PackageCreateRoutingModule,
} from "./package-create-routing.module";
import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";
import { PackageService } from "../../service/package.service";

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PackageCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: PackageService,
      useClass: PackageService,
    },
  ],
})
export class PackageCreateModule {}
