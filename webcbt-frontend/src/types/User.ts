export type Gender = 'male' | 'female' | 'other' | 'would rather not say';

export interface User {
  login: string;
  accessToken?: string;
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
  password: string;
  gender: string;
  age?: number | string;
}

export interface ManageAccountResponse {
  accessToken: string;
}
