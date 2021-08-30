import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { RoleModel } from "src/app/infrastructure/model/UserManagement/role-model";
import { RoleService } from "../role.service";

@Injectable({
    providedIn: 'root'
  })
  export class RoleResolverService implements Resolve <Observable<RoleModel[]>>{

    constructor( private roleService: RoleService ) {
      }
      resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot){
        return this.roleService.getRole();
      }

  }
  @Injectable({
    providedIn: 'root'
  })
  export class RoleTypeResolverService
  implements Resolve<Observable<any[]>>
{
  constructor( private roleService: RoleService ) {}
  resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) {
    var x = this.roleService.getRoleTypeDDL();
    return x;
  }
}
