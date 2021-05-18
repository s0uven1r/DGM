import { Component, OnInit, ChangeDetectionStrategy, HostBinding, ChangeDetectorRef } from '@angular/core';
import { ChildActivationEnd, Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { sideNavItems, sideNavSections } from 'src/app/infrastructure/datum/data';
import { NavigationService } from './services';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {
  user: any;
  @HostBinding('class.sb-sidenav-toggled') sideNavHidden = false;
    subscription: Subscription = new Subscription();
    sideNavItems = sideNavItems;
    sideNavSections = sideNavSections;
    sidenavStyle = 'sb-sidenav-dark';
    expandNavStyle = '';
    routeDataSubscription!: Subscription;
    sideNavItem!: any;
    isActive!: boolean;
    expanded = false;
    routeData!: any;

  constructor(public router: Router,
     private authService: OAuthService,
     public navigationService: NavigationService,
    private changeDetectorRef: ChangeDetectorRef) { 
    this.user = this.authService.getIdentityClaims();
    console.log(sideNavSections)
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
    this.subscription.add(
      this.navigationService.sideNavVisible$().subscribe(isVisible => {
          this.sideNavHidden = !isVisible;
          this.changeDetectorRef.markForCheck();
      })
  );
}
ngOnDestroy() {
  this.subscription.unsubscribe();
}
toggleSideNav() {
  if(this.expanded == false){
    this.expanded = true;
    this. expandNavStyle = 'collapse';
  }else{
    this.expanded = false;
    this. expandNavStyle = '';
  }
}
getLogout(){
  this.authService.revokeTokenAndLogout();

}
}
