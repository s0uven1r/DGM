import { CourseComponent } from 'src/app/featured/package-course/course/course.component';
import { TransactionEntryComponent } from 'src/app/featured/account/transaction-entry/transaction-entry.component';
import { AppComponent } from "src/app/app.component";
import { HomeComponent } from "src/app/core/home/home.component";
import { AuthCallbackComponent } from "src/app/core/auth-callback/auth-callback.component";
import { DashboardComponent } from "src/app/core/dashboard/dashboard.component";
import { RoleComponent } from "src/app/featured/identity/role/role.component";
import { PermissionComponent } from "src/app/featured/identity/permission/permission.component";
import { UserComponent } from "src/app/featured/identity/user/user.component";
import { CreateComponent } from "src/app/featured/identity/user/create/create.component";
import { ForbiddenComponent } from "src/app/core/forbidden/forbidden.component";
import { InternalServerErrorComponent } from "src/app/core/internal-server-error/internal-server-error.component";
import { MaintenanceComponent } from "src/app/featured/vehicle/maintenance/maintenance.component";
import { VehicleRegisterComponent } from "src/app/featured/vehicle/register/register.component";
import { VehicleCreateComponent } from "src/app/featured/vehicle/register/vehicle-create/vehicle-create.component";
import { VehicleUpdateComponent } from "src/app/featured/vehicle/register/vehicle-update/vehicle-update.component";
import { CreatemaintenanceComponent } from "src/app/featured/vehicle/maintenance/createmaintenance/createmaintenance.component";
import { AccountTypeComponent } from "src/app/featured/account/account-type/account-type.component";
import { AccountHeadComponent } from "src/app/featured/account/account-head/account-head.component";
import { AccountTypeCreateComponent } from "src/app/featured/account/account-type/account-type-create/account-type-create.component";
import { AccountTypeEditComponent } from "src/app/featured/account/account-type/account-type-edit/account-type-edit.component";
import { AccountHeadCreateComponent } from "src/app/featured/account/account-head/account-head-create/account-head-create.component";
import { AccountHeadEditComponent } from "src/app/featured/account/account-head/account-head-edit/account-head-edit.component";
import { KycComponent } from 'src/app/featured/identity/user/kyc/kyc.component';
import { PackageComponent } from 'src/app/featured/package-course/package/package.component';
import { PackageUpdateComponent } from 'src/app/featured/package-course/package/package-update/package-update.component';
import { PackageCreateComponent } from 'src/app/featured/package-course/package/package-create/package-create.component';
import { PromoComponent } from 'src/app/featured/package-course/promo/promo.component';
import { PromoCreateComponent } from 'src/app/featured/package-course/promo/promo-create/promo-create.component';
import { PromoUpdateComponent } from 'src/app/featured/package-course/promo/promo-update/promo-update.component';
import { CourseCreateComponent } from 'src/app/featured/package-course/course/course-create/course-create.component';
import { CourseUpdateComponent } from 'src/app/featured/package-course/course/course-update/course-update.component';
import { CourseTypeComponent } from 'src/app/featured/package-course/course-type/course-type.component';
import { CourseTypeCreateComponent } from 'src/app/featured/package-course/course-type/course-type-create/course-type-create.component';
import { CourseTypeUpdateComponent } from 'src/app/featured/package-course/course-type/course-type-update/course-type-update.component';
import { LogoComponent } from 'src/app/featured/settings/logo/logo.component';
import { DescriptiveImageComponent } from 'src/app/featured/settings/descriptive-image/descriptive-image.component';
<<<<<<< HEAD
import { TransactionEntryCreateComponent } from 'src/app/featured/account/transaction-entry/transaction-entry-create/transaction-entry-create.component';
=======
import { IndividualShiftComponent } from 'src/app/featured/shift/individual-shift/individual-shift.component';
import { CustomerPackageComponent } from 'src/app/featured/package-course/customer-package/customer-package.component';
>>>>>>> dgm

export const RouteComponent = {
    AppRouteComponent: [
        AppComponent,
        HomeComponent,
        DashboardComponent,
        ForbiddenComponent,
        InternalServerErrorComponent
      ],
      AuthCallbackRouteComponent: [
        AuthCallbackComponent
      ],
      DashBoardRouteComponent: [],
      RoleRouteComponent: [RoleComponent],
      PermissionRouteComponent: [PermissionComponent],
      UserRouteComponent: [UserComponent],
      UserCreateRouteComponent: [CreateComponent],
      KYCRouteComponent: [KycComponent],
      VehicleInventoryRouteComponent: [VehicleRegisterComponent],
      VehicleInventoryCreateRouteComponent: [VehicleCreateComponent],
      VehicleInventoryUpdateRouteComponent: [VehicleUpdateComponent],
      VehicleMaintenanceRouteComponent: [MaintenanceComponent],
      VehicleMaintenanceCreateRouteComponent: [CreatemaintenanceComponent],
      AccountTypeRouteComponent: [AccountTypeComponent],
      AccountTypeCreateRouteComponent: [AccountTypeCreateComponent],
      AccountTypeEditRouteComponent: [AccountTypeEditComponent],
      AccountHeadRouteComponent: [AccountHeadComponent],
      AccountHeadCreateRouteComponent: [AccountHeadCreateComponent],
      AccountHeadEditRouteComponent: [AccountHeadEditComponent],
      AccountTransactionEntryRouteComponent: [TransactionEntryComponent],
      AccountTransactionEntryCreateRouteComponent: [TransactionEntryCreateComponent],
      PackageRouteComponent: [PackageComponent],
      PackageCreateRouteComponent: [PackageCreateComponent],
      PackageUpdateRouteComponent: [PackageUpdateComponent],
      PromoRouteComponent: [PromoComponent],
      PromoCreateRouteComponent: [PromoCreateComponent],
      PromoUpdateRouteComponent: [PromoUpdateComponent],
      CourseRouteComponent: [CourseComponent],
      CourseCreateRouteComponent: [CourseCreateComponent],
      CourseUpdateRouteComponent: [CourseUpdateComponent],
      CourseTypeRouteComponent:[CourseTypeComponent],
      CourseTypeCreateRouteComponent: [CourseTypeCreateComponent],
      CourseTypeUpdateRouteComponent: [CourseTypeUpdateComponent],
      LogoRouteComponent: [LogoComponent],
      DescriptiveImageRouteComponent: [DescriptiveImageComponent],
      IndividualShiftRouteComponent: [IndividualShiftComponent],
      CustomerPackageRouteComponent: [CustomerPackageComponent],
}
