import { Component, Input } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { oidcAuthConfig } from 'src/app/infrastructure/datum/configure-oidc';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isLogedIn = false;
  constructor(private oauthService: OAuthService) {
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
    this.isLogedIn = this.oauthService.hasValidAccessToken();
  });
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
   public Login(){
     this.oauthService.initImplicitFlow();
   }
   public logOut(){
    this.oauthService.revokeTokenAndLogout();   
   }
   public get GetClaim(){
     let claims = this.oauthService.getIdentityClaims();
     return claims?claims : null;
   }
}
