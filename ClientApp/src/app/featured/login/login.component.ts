import { Component, OnInit } from '@angular/core';
import {  OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks'
import { oidcAuthConfig } from 'src/app/infrastructure/datum/configure-oidc';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private oauthService: OAuthService) {
	
    this.ConfigureImplicitFlowAuthentication();
  }

  private ConfigureImplicitFlowAuthentication() {

    this.oauthService.configure(oidcAuthConfig);
  
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocument().then(doc =>{
      this.oauthService.tryLogin()
			.catch(err => {
				console.log(err);
			}).then(() => {
				if(!this.oauthService.hasValidAccessToken()) {
					this.oauthService.initImplicitFlow()
				}
			});
    });
  
  }
  
}

