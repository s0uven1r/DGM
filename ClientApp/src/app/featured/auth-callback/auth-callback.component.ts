import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AuthCallbackComponent implements OnInit {

  constructor(private oauthService: OAuthService, private router: Router) {
    
  }

  ngOnInit() {
      this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
          if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
              this.oauthService.initImplicitFlow();
          }else{
            this.router.navigateByUrl('/dashboard');
          }
      });
  }
}
