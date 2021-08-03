import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush

})
export class AuthCallbackComponent implements OnInit {

  constructor(private oauthService: OAuthService, private changeDetectorRef: ChangeDetectorRef, private router: Router) {
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
      if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
          this.oauthService.initImplicitFlow();
        }else{
          this.redirectControl();
        }
      }).catch(x => {this.redirectControl();});
  }

  ngOnInit() {

        }
  redirectControl(){

    setTimeout(x => {
          var url = localStorage.getItem('returnAfterLoginUrl');
          if(this.router.routerState.snapshot.url.includes(url)){
            this.router.navigateByUrl('/dashboard');
          }
          else{
            this.router.navigateByUrl(url);
          }
          this.changeDetectorRef.markForCheck();
        }, 200);
    }
}
