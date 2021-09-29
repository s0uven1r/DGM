import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { PackageControllersClaim } from 'src/app/infrastructure/datum/claim/package-management';
import { PackageModel } from 'src/app/infrastructure/model/UserManagement/resource/package/packagemodel';
import Swal from 'sweetalert2';
import { PackageService } from '../service/package.service';

@Component({
  selector: 'app-package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PackageComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  packages: PackageModel[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  packageCreateClaim = [PackageControllersClaim.Package.Write];
  isDtInitialized:boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  constructor(
    private packageService: PackageService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
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
  updatePackageDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/config/package/edit/${id}`])
    );
    window.open(url, "_self");
    //window.open(url, "_blank");
  }
  deletePackageDetail(id: string) {
    Swal.fire({
      title: "Delete pacakge details?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.packageService.deletePackageById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Package detail deleted successfully.",
              "success"
            );
           this.getInitData();
          },
          (err) => {
            Swal.fire("Error Deleted!", err, "error");
          }
        );
      }
    });
  }

  getInitData(){
    this.packageService.getPackage().subscribe(x => {this.packages = x;
      if (this.isDtInitialized) {
        this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next();
        });
      } else {
        this.isDtInitialized = true
        this.dtTrigger.next();
      }
      this.changeDetectorRef.markForCheck();
    });
  }
}
