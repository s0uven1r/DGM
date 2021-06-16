export const UserRoutePath = {path:'user',data: { breadcrumb: 'User'}, children: [{path: '',
loadChildren: () =>import('src/app/featured/identity/user/user.module').then(
 (m) => m.UserModule
)},
{path: 'create',
data: { breadcrumb: 'Create'},
loadChildren: () =>import('src/app/featured/identity/user/create/create.module').then(
 (m) => m.CreateModule
)},
]}