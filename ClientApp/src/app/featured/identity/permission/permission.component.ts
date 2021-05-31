import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subscription } from 'rxjs';
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
  private sub: any;
  permissionData!: any [];
  permissionForm: FormGroup;
  claims: FormArray;
  subscription: Subscription = new Subscription();
  constructor(private route: ActivatedRoute,
     private permissionService: PermissionService, private authService: OAuthService,
      private form: FormBuilder, private changeDetectorRef: ChangeDetectorRef) { 
    this.FormDesign();
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
      claims: this.form.array([])
    }));
  }
  createItem(id: string, hasClaim: boolean = false, title: string): FormGroup {
    return this.form.group({
      id: [id, [Validators.required]],
      hasChecked: [hasClaim],
      title: [title]
    });
  }
  addItem(permissionList: any): void {
    permissionList.forEach((element: { permissionList: any[]; }) => {
      element.permissionList.forEach((item: { claimId: string; hasClaim: boolean; claimTitle: string; }) => {
          this.claims = this.permissionForm.get('claims') as FormArray;
          this.claims.push(this.createItem(item.claimId, item.hasClaim, item.claimTitle));
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
            this.authService.revokeTokenAndLogout();
          },(err) => {
                Swal.fire(
                  'Error Added!',
                  err,
                  'error'
              )
            })
        }}})
  }
  getInitData(){
    this.permissionData = [];
    let frmArray = this.permissionForm.get('claims') as FormArray;
    frmArray.clear();
    this.sub = this.route.params.subscribe(params => {
      this.id = params['roleId']; 
      this.permissionForm.patchValue({'roleId': this.id});
   });
   this.subscription.add(this.permissionService.getPermission(this.id).subscribe(x => {
     this.permissionData = x;
     this.addItem(x['rolePermissionGroup']);
     this.changeDetectorRef.markForCheck();
   }));
  }
}
