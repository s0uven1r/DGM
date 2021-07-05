import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { PermissionService } from 'src/app/featured/identity/permission/service/permission.service';


@Directive({
  selector: '[appCheckclaim]',
})
export class CheckclaimDirective {
  private hasClaim = false;
  constructor(private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private permissionService: PermissionService) { }

    @Input() set appCheckclaim(claims: string[]) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.permissionService.checkPermission(claims)
      .subscribe(
          (res) => {
           this.hasClaim = res;
           if(!this.hasClaim){
            this.viewContainer.clear();
          }
        })
      
    }
}
