export interface AuthRequest
{
    login : string
    password : string
}

export interface UserWithToken
{
    id : number,
    login : string,
    role : string,
    token : string
}

export interface User
{
    id : number,
    login : string,
    role : string,
}

export interface UpdateRole
{
    id : number,
    role : string,
}