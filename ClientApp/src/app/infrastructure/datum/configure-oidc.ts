import { AuthConfig } from 'angular-oauth2-oidc';

export const oidcAuthConfig: AuthConfig = {
  // Url of the Identity Provider
  issuer: 'https://localhost:44316',

  // URL of the SPA to redirect the user to after login
  redirectUri: 'http://localhost:4200/auth-callback',

  // The SPA's id. The SPA is registered with this id at the auth-server
  clientId: 'angular_spa',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'api.read',

  // // Login Url of the Identity Provider
  loginUrl: 'https://localhost:44316/connect/authorize',

  // // Login Url of the Identity Provider
  logoutUrl: 'https://localhost:44316/connect/endsession',

  // postLogoutRedirectUri: 'http://localhost:4200',
  useSilentRefresh: true,

  oidc: true
}
