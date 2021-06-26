import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { IdentityControllersClaim } from 'src/app/infrastructure/datum/claim/user-management';
import { RoleModel } from 'src/app/infrastructure/model/UserManagement/role-model';
import Swal from 'sweetalert2';
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
  roleForm: FormGroup;
  isEdit: boolean;
  roleCreateClaim = [IdentityControllersClaim.Role.WriteRole];
  roleViewClaim = [IdentityControllersClaim.Permission.ViewPermission];
  constructor(private form: FormBuilder, private roleService: RoleService, private changeDetectorRef: ChangeDetectorRef) { 
    this.FormDesign();
  }
  ngOnInit(): void {
    this.subscription.add(this.roleService.getRole().subscribe(x => {this.roleData = x;
      this.changeDetectorRef.markForCheck();}));
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  FormDesign() {
    return (this.roleForm = this.form.group({
      title: [null,  [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      id: [null],
      rank: [null],
      hasPublic: [false]
    }));
  }
  onSubmit() {
    Swal.fire({
        title: 'Are you sure?',
        text: '',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ok',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            this.PerformCreateEditRole();
        }
    })
 
  }
 PerformCreateEditRole(){
    if (this.roleForm.valid) {
      var msg = this.roleForm.get('id').value ?'Updated!':'Added!';
      (this.roleService.performCreateEditRole(
        this.roleForm.get('title').value,
        this.roleForm.get('hasPublic').value,
        this.roleForm.get('id').value,
        this.roleForm.get('rank').value
      )).subscribe(
        () => {
          Swal.fire(
            msg,
            'Success.',
            'success'
        )
        this.subscription.add(this.roleService.getRole().subscribe(x => {this.roleData = x;
          this.changeDetectorRef.markForCheck();}));
        this.roleForm.reset();
        }
    );
    }
  }

  getData(id: string, title: string, isPublic: boolean){
    this.isEdit = true;
    this.roleForm.patchValue({
      'id':id,
      'title': title,
      'hasPublic': isPublic
    });
  }
  deleteData(id: string){
    Swal.fire({
      title: 'Delete a role',
      text: '',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ok',
      cancelButtonText: 'No'
  }).then((result) => {
      if (result.value) {
        this.roleService.deleteRole(id)
        .subscribe(
            () => {
              Swal.fire(
                'Deleted!',
                'Role has deleted successfully.',
                'success'
            );
            this.subscription.add(this.roleService.getRole().subscribe(x => {this.roleData = x;
              this.changeDetectorRef.markForCheck();}));
          },(err) => {
                Swal.fire(
                  'Error Deleted!',
                  err,
                  'error'
              )
            })
      }
  })
  }
}

