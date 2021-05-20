import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { RoleService } from './service/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoleComponent implements OnInit {
  roleData!: RoleModel [];
  subscription: Subscription = new Subscription();
  constructor(private roleService: RoleService, private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.subscription.add(this.roleService.getRole().subscribe(x => {this.roleData = x;
      this.changeDetectorRef.markForCheck();}));
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
export interface RoleModel {
  id: string;
  name: string;
}
