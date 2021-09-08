export const ApiGateway = {
    identity:{
        role: {
            base : 'Roles',
            getRole: '/GetRoles',
            postRole: '/AddRole',
            putRole: '/UpdateRole',
            setPublic: '/SetPublic',
            removePublic: '/RemovePublic',
            deleteRole: '/DeleteRole',
            getRoleTypeDDL: '/Get/GetRoleTypeEnumDDL'
        },
        menu: {
            base: 'Menu',
            getMenu:'/GetMenu'
        },
        permission: {
            base: 'Permissions',
            getPermission: '/GetRolePermission',
            managePermission: '/ManageRolePermission',
            checkPermission: '/CheckPermission'
        },
        user: {
            base: 'Users',
            getUser: '/GetUser',
            enableUser: '/EnableLogin',
            disableUser: '/DisableLogin',
            createEmployee: '/CreateEmployee',
            updateEmployee: '/UpdateEmployee',
            getAccountNumberDetails: '/GetAccountDetails'
        }
    },
    resource: {
        vehicle: {
            inventory: {
                base: 'VehicleInventory',
                getAll: '/Get/GetAllVehicle',
                getSingleById: '/Get/GetVehicleDetailById',
                create: '/Create',
                update: '/Update',
                delete: '/Delete',
                getAccountNumberDetails:'/GetAccountDetails',
            },
            maintenance:{
                base: 'VehicleMaintenance',
                create: '/Create',
            }
        },
        account: {
            accounttype: {
                base: 'AccountType',
                getAll: '/Get/GetAll',
                getSingleById: '/Get/GetById',
                getAccountTypeDDL: '/Get/GetAccountTypeEnumDDL',
                create: '/Create',
                update: '/Update',
            },
            accounthead:{
                base: 'AccountHead',
                getAll: '/Get/GetAll',
                getAccountNumberDetails:'/GetAccountDetails',
                getSingleById: '/Get/GetById',
                create: '/Create',
                update: '/Update',
            }
        }
    }
}
