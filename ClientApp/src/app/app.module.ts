import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule, RoutingComponent } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpErrorInterceptor } from './core/interceptor/http-error.interceptor';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AuthGuard } from './core/authorize/auth-guard';
import { TokenInterceptor } from './core/interceptor/http-token-interceptor';
import { BreadcrumbModule } from 'angular-crumbs';
import { LogoModule } from './core/logo/logo.module';
@NgModule({
  declarations: RoutingComponent,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BreadcrumbModule,
    LogoModule,
    OAuthModule.forRoot(),

  ],
  providers: [ {

    provide: HTTP_INTERCEPTORS,

    useClass: HttpErrorInterceptor,

    multi: true

  },{

    provide: HTTP_INTERCEPTORS,

    useClass: TokenInterceptor,

    multi: true

  },{

    provide: AuthGuard,

    useClass: AuthGuard

  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
