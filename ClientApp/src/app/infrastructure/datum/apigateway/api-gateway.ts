export const ApiGateway = {
    identity:{
        role: {
            base : 'Roles',
            getRole: '/GetRoles',
            postRole: '/AddRole',
            putRole: '/UpdateRole',
            setPublic: '/SetPublic',
            removePublic: '/RemovePublic',
            deleteRole: '/DeleteRole'
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
            updateEmployee: '/UpdateEmployee'
        }
    }
}