import { ChangeDetectorRef, Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  constructor(private oauthService: OAuthService) {
  }
  redirectToLogin(){
    this.oauthService.initImplicitFlow();
  }
}
