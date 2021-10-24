import { Component, OnInit, ChangeDetectionStrategy, HostBinding, ChangeDetectorRef } from '@angular/core';
import {  ChildActivationEnd,  Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { MenuResultModel } from 'src/app/infrastructure/model/UserManagement/Menu/menu-result-model';
import {  NavigationService } from './services';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {
    user: any;
    subscription: Subscription = new Subscription();
    sidenavStyle = 'sb-sidenav-dark';
    expandNavStyle = '';
    expandContentStyle = '';
    expanded = false;
    hideKyc = false;
    menuList: MenuResultModel[];
  constructor(public router: Router,
     private authService: OAuthService,
     public navigationService: NavigationService,
    private changeDetectorRef: ChangeDetectorRef) { 
    this.user = this.authService.getIdentityClaims();
    this.router.events
    .pipe(filter(event => event instanceof ChildActivationEnd))
    .subscribe(event => {
        let snapshot = (event as ChildActivationEnd).snapshot;
        while (snapshot.firstChild !== null) {
            snapshot = snapshot.firstChild;
        }
    });
  }

  ngOnInit(): void {
    if(this.authService.getIdentityClaims()['IsKYCUpdated']==='True'){
      this.hideKyc = true;
      this.subscription.add(
        this.navigationService.getMenu().subscribe(x => {
           this.menuList = x;
            this.changeDetectorRef.markForCheck();
        }));
   
    }else{
      this.expandContentStyle = 'layoutMarginLeft';
      this.hideKyc = false;
      this.router.navigateByUrl('dashboard/user/kyc')
    }
}
ngOnDestroy() {
  this.subscription.unsubscribe();
}
toggleSideNav() {
  if(this.expanded == false){
    this.expanded = true;
    this. expandNavStyle = 'collapse';
    this.expandContentStyle = 'layoutMarginLeft';
  }else{
    this.expanded = false;
    this. expandNavStyle = '';
    this.expandContentStyle = '';
  }
}
getLogout(){
  this.authService.revokeTokenAndLogout();

}
  
}
