// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  // apiIdentityUrl: 'https://localhost:44316/Authorization/',
  // resourceUrl: 'https://localhost:44337/api/',
  // issuer: 'https://localhost:44316',
  // redirectUri: 'http://localhost:4200/auth-callback'
  apiIdentityUrl: 'https://auth.dgmtech.tk/Authorization/',
  resourceUrl: 'https://api.dgmtech.tk/api/',
  issuer: 'http://auth.dgmtech.tk',
  redirectUri: 'https://demo.dgmtech.tk/auth-callback'
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
