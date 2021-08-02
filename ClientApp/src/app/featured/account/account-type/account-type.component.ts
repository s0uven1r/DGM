import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { DataTableDirective } from "angular-datatables";
import { Subject } from "rxjs";
import { AccountControllersClaim } from "src/app/infrastructure/datum/claim/account-management";
import { AccountTypeModel } from "src/app/infrastructure/model/UserManagement/resource/account/account-type-model";
import { AccountService } from "../service/account.service";

@Component({
  selector: "app-account-type",
  templateUrl: "./account-type.component.html",
  styleUrls: ["./account-type.component.css"],
})
export class AccountTypeComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  accountTypeData: AccountTypeModel[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  accountTypeCreateClaim = [AccountControllersClaim.AccountType.Write];
  isDtInitialized: boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  constructor(
    private accountService: AccountService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: "full_numbers",
      pageLength: 5,
      lengthMenu: [
        [5, 10, 25, 50, 100, -1],
        [5, 10, 25, 50, 100, "All"],
      ],
    };
    this.getInitData();
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  updateAccountTypeDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/account/accounttype/edit/${id}`])
    );
    window.open(url, "_self");
  }

  getInitData() {
    this.accountService.getAllAccountType().subscribe((x) => {
      this.accountTypeData = x;
      if (this.isDtInitialized) {
        this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next();
        });
      } else {
        this.isDtInitialized = true;
        this.dtTrigger.next();
      }
      this.changeDetectorRef.markForCheck();
    });
  }
}
