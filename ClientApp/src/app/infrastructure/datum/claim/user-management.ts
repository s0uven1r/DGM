export const IdentityControllersClaim = {
    identity: {
        ViewIdentity: 'Claim.Identity.Read'
     },
    User:{
       ViewUser: "Claim.Identity.User.Read",
       WriteUser: "Claim.Identity.User.Write"
    },
    Role:{
        ViewRole: "Claim.Role.Read",
        WriteRole: "Claim.Role.Write"        
     },
     Permission:{
        ViewPermission: "Claim.Permission.Read",
        WritePermission: "Claim.Permission.Write"        
     },
     Menu:{
        ViewMenu: "Claim.Menu.Read",
        WriteMenu: "Claim.Menu.Write"        
     }
} 