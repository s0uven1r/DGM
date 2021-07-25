import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subscription } from 'rxjs';
import { IdentityControllersClaim } from 'src/app/infrastructure/datum/claim/user-management';
import Swal from 'sweetalert2';
import { PermissionService } from './service/permission.service';

@Component({
  selector: 'app-permission',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PermissionComponent implements OnInit {
  id: string;
  user: any;
  private sub: any;
  permissionData!: any [];
  permissionForm: FormGroup;
  subscription: Subscription = new Subscription();
  permissionCreateClaim = [IdentityControllersClaim.Permission.WritePermission];
  constructor(private route: ActivatedRoute,
     private permissionService: PermissionService, private authService: OAuthService,
      private form: FormBuilder, private changeDetectorRef: ChangeDetectorRef) { 
    this.FormDesign();
    this.user = this.authService.getIdentityClaims();
  }

  ngOnInit() {
    this.getInitData();
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
  FormDesign() {
    return (this.permissionForm = this.form.group({
      roleId: [null,  [Validators.required]],
      modules: this.form.array([])
    }));
  }
  createItem( title: string, permissionList: any,): FormGroup {
    var moduleForm =  this.form.group({
      title: [title],
      claims: this.form.array([])
    });
    return moduleForm;
  }

  addClaim(permission: any, moduleIndex: number): void {
    let fg = this.form.group({
      id: [permission['claimId'], [Validators.required]],
      hasChecked: [permission['hasClaim']],
      title: [permission['claimTitle']]
    });

    (<FormArray>(<FormGroup>(<FormArray>this.permissionForm.controls['modules'])
        .controls[moduleIndex]).controls['claims']).push(fg);

  }

  addClaimModule(permissionList: any): void {
    permissionList.forEach((item: { module: string; permissionList: any }) => {
          var moduleForm =  this.form.group({
            title: [item.module],
            claims: this.form.array([])
          });
          (<FormArray>this.permissionForm.get('modules')).push(moduleForm);
          let moduleIndex = (<FormArray>this.permissionForm.get('modules')).length - 1;
            item.permissionList.forEach((claim: any) => {
                this.addClaim(claim, moduleIndex);
            });
      });
  }

  onSubmit(){
    Swal.fire({
      title: 'Permission',
      text: 'Are you sure want to assign this permission?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ok',
      cancelButtonText: 'No'
  }).then((result) => {
      if (result.value) {
        if (this.permissionForm.valid) {
        this.permissionService.assignPermission(this.permissionForm.value)
        .subscribe(
            () => {
              Swal.fire(
                'Added!',
                'Permission has assigned successfully.',
                'success'
            );
            if(this.user.RoleId ===  this.permissionForm.get('roleId').value){
              this.authService.revokeTokenAndLogout();
            }
          })
        }}})
  }
  getInitData(){
    this.permissionData = [];
    let frmArray = this.permissionForm.get('modules') as FormArray;
    frmArray.clear();
    this.sub = this.route.params.subscribe(params => {
      this.id = params['roleId']; 
      this.permissionForm.patchValue({'roleId': this.id});
   });
   this.subscription.add(this.permissionService.getPermission(this.id).subscribe(x => {
     this.permissionData = x;
     this.addClaimModule(x['rolePermissionGroup']);
     this.changeDetectorRef.markForCheck();
   }));
  }
}
