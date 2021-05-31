import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
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
      id: [null]
    }));
  }
  onSubmit() {
    Swal.fire({
        title: 'Add a role',
        text: '',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ok',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            this.RegisterRole();
        }
    })
 
  }
  RegisterRole(){
    if (this.roleForm.valid) {
      (this.roleService.registerRole(
        this.roleForm.get('title').value
      )).subscribe(
        () => {
          Swal.fire(
            'Added!',
            'Role has added successfully.',
            'success'
        )
        this.subscription.add(this.roleService.getRole().subscribe(x => {this.roleData = x;
          this.changeDetectorRef.markForCheck();}));
        this.roleForm.reset();
        },
        (err) => {
          Swal.fire(
            'Error Added!',
            err,
            'error'
        )
        }
    );
    }
  }

  getData(id: string, title: string){
    this.roleForm.patchValue({
      'id':id,
      'title': title
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
      }
  })
  }
}
export interface RoleModel {
  id: string;
  name: string;
}
