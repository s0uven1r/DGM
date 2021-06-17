export const RoleRoutePath = {path:'role',data: { breadcrumb: 'Role'}, children: [{path: '',
loadChildren: () =>import('src/app/featured/identity/role/role.module').then(
 (m) => m.RoleModule
)}]}