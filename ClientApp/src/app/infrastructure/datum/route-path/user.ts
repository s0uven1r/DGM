export const UserRoutePath = {path:'user',data: { breadcrumb: 'User'}, children: [{path: '',
loadChildren: () =>import('src/app/featured/identity/user/user.module').then(
 (m) => m.UserModule
)},
{path: 'create',
data: { breadcrumb: 'Create'},
loadChildren: () =>import('src/app/featured/identity/user/create/create.module').then(
 (m) => m.CreateModule
)},
{path: 'edit/:id',
data: { breadcrumb: 'Edit'},
loadChildren: () =>import('src/app/featured/identity/user/create/create.module').then(
 (m) => m.CreateModule
)},
{path: 'kyc',
data: { breadcrumb: 'KYC'},
loadChildren: () =>import('src/app/featured/identity/user/kyc/kyc.module').then(
 (m) => m.KycModule
)},
]}