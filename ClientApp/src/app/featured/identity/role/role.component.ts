import { Component, OnInit } from '@angular/core';
import { RoleService } from './service/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  constructor(private roleService: RoleService) { }

  ngOnInit(): void {
    this.roleService.getRole().subscribe(x => console.log(x), err => {console.error(err)});
  }

}
