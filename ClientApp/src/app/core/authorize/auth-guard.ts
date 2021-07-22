import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { OAuthService } from "angular-oauth2-oidc";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private oauthService: OAuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot ) {
        if (this.oauthService.hasValidAccessToken()) {
            return true;
        }
        localStorage.setItem('returnAfterLoginUrl', "");
        localStorage.setItem('returnAfterLoginUrl', state.url);
        this.oauthService.initImplicitFlow();
    }
}