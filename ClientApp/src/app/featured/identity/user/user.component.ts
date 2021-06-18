import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { UserModel } from 'src/app/infrastructure/model/UserManagement/user-model';
import { UserService } from './service/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  persons: UserModel[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private userService: UserService, private router: Router, private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]]
    };
    this.userService.getUser().subscribe(x => {this.persons = x;
      this.changeDetectorRef.markForCheck();
      this.dtTrigger.next();
    });
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  hasEnable(id: string, val: any){
      this.userService.enableDisableLogin(id, val.checked ).subscribe(() => {
        this.changeDetectorRef.markForCheck();
      }, () => {
        val.checked = !val.checked
      });;
  }
  getEdit(id: string){
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/user/edit/${id}`])
    );
    window.open(url, "_blank");
  }
}

