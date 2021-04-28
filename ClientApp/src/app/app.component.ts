import { Component, AfterContentInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { oidcAuthConfig } from './infrastructure/datum/configure-oidc';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'DSC-APP';
  constructor(private oauthService: OAuthService) {
    this.configureOAuth();
  }
  
  private configureOAuth(){
    this.oauthService.configure(oidcAuthConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
    
  }
 
}
