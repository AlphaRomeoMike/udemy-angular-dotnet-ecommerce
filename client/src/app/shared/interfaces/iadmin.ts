export interface IAdmin {
    name: string;
    email: string;
    token?: string;
    created_at: string
}

export interface IAdminDto {
    Name: string;
    Email: string;
    Password: string
}

export interface ILogin {
    name: string;
    email: string;
}