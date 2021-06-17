import { Component, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { RoleModel } from 'src/app/infrastructure/model/UserManagement/role-model';
import Swal from 'sweetalert2';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  InternalUserForm: FormGroup;
  roleData: RoleModel[] =   [];
  private isEdit: boolean;
  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private form: FormBuilder ) {
                this.InternalUserFormDesign();
            
              }

  ngOnInit(): void {
    this.roleData = this.route.snapshot.data.roleData;
     this.route.params.subscribe(params => {
      if(params['id']){
        this.isEdit = true;
        this.InternalUserForm.controls['email'].disable();
        this.InternalUserForm.controls['confirmEmail'].disable();
        this.userService.getUserById(params['id']).subscribe(x => {
          this.InternalUserForm.patchValue({
            'id': params['id'],
            'appliedRole': x.roleId,
            'firstName': x.firstName,
            'middleName': x.middleName,
            'lastName': x.lastName,
            'email': x.email,
            'confirmEmail': x.email,
            'phone': x.phoneNumber
        }); 
        })
      }
     
   });
  }
  InternalUserFormDesign() {
    return this.InternalUserForm = this.form.group({
      appliedRole: [""],
      id: [null],
      phone: [null],
      email: [null],
      confirmEmail: [null],
      firstName: [null],
      middleName: [null],
      lastName: [null]
    });
  }
  registerUser(){
    Swal.fire({
      title: this.isEdit?'Update a User': 'Add a User',
      text: 'User Action',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ok',
      cancelButtonText: 'No'
      }).then((result) => {
          if (result.value) {
        this.userService.performInternalUserAction(this.InternalUserForm.value)
          .pipe(catchError(err => {
            return throwError(err);
        }))
          .subscribe(() => {
                    this.InternalUserForm.reset(); this.InternalUserForm.clearValidators();
                    this.InternalUserForm.patchValue({appliedRole: ''});
                    Swal.fire(
                      this.isEdit?'Updated':'Added!',
                      'User Action',
                      'success'
                  )},
                err => { Swal.fire(
                  'Error!',
                  err,
                  'error'
              )},
          () => console.log('HTTP request completed.') );
      }
    })};

}
