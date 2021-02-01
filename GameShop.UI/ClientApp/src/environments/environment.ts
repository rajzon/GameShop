// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  baseUrl: 'api/',
  //OrderInfo config
  nameMaxLength: 30,
  surNameMaxLength: 30,
  streetMaxLength: 80,
  postCodeMaxLength: 12,
  cityMaxLength: 40,
  countryMaxLength: 40,
  phonePattern: '^[0-9]{1,15}$',
  emailPattern: '^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$',
  ///////////////////////
  //Pagination config
  pageNumber: 1,
  pageSize: 5,
  ///////////////////
  //Image uploader config
  maxFileSize: 10 * 1024 * 1024, //10MB
  ///////////////////////
  //User management config
  availableRoles: [
    {name: 'Admin', value: 'Admin'},
    {name: 'Moderator', value: 'Moderator'},
    {name: 'Customer', value: 'Customer'},
  ],
  ///////////////////////
  //User login/register config
  userNameMaxLength: 30,
  userNameMinLength: 3,
  userSurNameMaxLength: 40,
  userSurNameMinLength: 3,
  userPasswordMaxLength: 8,
  userPasswordMinLength: 4,
  /////////////////
  //Requirements model config
  osMaxLength: 30,
  processorMaxLength: 100,
  graphicsCardMaxLength: 100,
  ////////////////
  //Product model config
  productNameMaxLength: 50,
  productDescriptionMaxLength: 2000,
  productPriceMaxValue: 9999999.99,
  productPriceMinValue: 0
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
