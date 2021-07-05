export const VehicleRoutePath = {path:'vehicle', data: { breadcrumb: 'Vehicle'},
children: [{path: 'maintain',
data: { breadcrumb: 'Maintain'},
children: [{path: '',
loadChildren: () =>import('src/app/featured/vehicle/maintenance/maintenance.module').then(
    (m) => m.MaintenanceModule
   )},
    {path: 'create',
    data: { breadcrumb: 'Create'},
    loadChildren: () =>import('src/app/featured/vehicle/maintenance/createmaintenance/createmaintenance.module').then(
    (m) => m.CreatemaintenanceModule
    )},
]},

{path: 'register',
data: { breadcrumb: 'Register'},
loadChildren: () =>import('src/app/featured/vehicle/register/register.module').then(
 (m) => m.RegisterModule
)}
]}