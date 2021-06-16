export const ApiGateway = {
    identity:{
        role: {
            base : 'Roles',
            getRole: '/GetRoles',
            postRole: '/AddRole',
            putRole: '/UpdateRole',
            deleteRole: '/DeleteRole'
        },
        menu: {
            base: 'Menu',
            getMenu:'/GetMenu'
        },
        permission: {
            base: 'Permissions',
            getPermission: '/GetRolePermission',
            managePermission: '/ManageRolePermission'
        },
        user: {
            base: 'Users',
            getUser: '/GetAllUsers',
            enableUser: '/EnableLogin',
            disableUser: '/DisableLogin'
        }
    }
}