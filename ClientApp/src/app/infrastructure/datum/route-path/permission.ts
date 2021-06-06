export const PermissionRoutePath = {path:'permission/:roleId',data: { breadcrumb: 'Permission'}, children: [{path: '',
loadChildren: () =>import('src/app/featured/identity/permission/permission.module').then(
 (m) => m.PermissionModule
)}]};