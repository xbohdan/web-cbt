export type Gender = 'male' | 'female' | 'other' | 'would rather not say';

export interface User {
  userId: number;
  login: string;
  age?: number | string;
  gender: string;
  userStatus: number;
  banned: boolean;
  accessToken?: string;
}

export interface GetUserResponse {
  userId: number;
  login: string;
  age?: number | string;
  gender: string;
  userStatus: number;
  banned: boolean;
}

export interface PutUserRequest {
  body: {
    login: string;
    password?: string;
    age?: number | string;
    gender?: string;
  };
  userId: number;
}

export interface RegistrationForm {
  login: string;
  password: string;
  gender: string;
  age?: number | string;
}

export interface RegistrationRequest {
  login: string;
  password: string;
  gender: string;
  age?: number | string;
  userStatus: number;
  banned: boolean;
}

export interface LoginCredentials {
  login: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
}

export interface ManageAccountForm {
  login: string;
  password?: string;
  gender?: string;
  age?: number | string;
}
