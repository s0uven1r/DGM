import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule, RoutingComponent } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpErrorInterceptor } from './core/interceptor/http-error.interceptor';
import { OAuthModule } from 'angular-oauth2-oidc';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    OAuthModule.forRoot()
  ],
  providers: [ {

    provide: HTTP_INTERCEPTORS,

    useClass: HttpErrorInterceptor,

    multi: true

  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
