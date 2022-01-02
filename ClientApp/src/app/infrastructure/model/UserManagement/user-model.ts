export interface UserModel{
    id: string;
    email: string;
    userName: string;
    firstName: string; 
    middleName : string; 
    lastName: string;  
    phoneNumber: string;  
    roleName: string;
    isDefault: boolean;
    IsKYCUpdated: boolean;
}