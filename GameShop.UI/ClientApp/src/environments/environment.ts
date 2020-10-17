// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  baseUrl: 'api/',
  nameMaxLength: 30,
  surNameMaxLength: 30,
  addressMaxLength: 80,
  streetMaxLength: 80,
  postCodeMaxLength: 6,
  cityMaxLength: 40,
  pageNumber: 1,
  pageSize: 5,
  maxFileSize: 10 * 1024 * 1024, //10MB
  availableRoles: [
    {name: 'Admin', value: 'Admin'},
    {name: 'Moderator', value: 'Moderator'},
    {name: 'Customer', value: 'Customer'},
  ]
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
