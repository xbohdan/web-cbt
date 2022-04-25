export type Gender = 'male' | 'female' | 'other' | 'would rather not say';

export interface User {
  username: string;
  accessToken?: string;
}

export interface RegistrationForm {
  username: string;
  password: string;
  gender: string;
  age?: number | string;
}

export interface RegistrationRequest {
  username: string;
  password: string;
  gender: string;
  age?: number | string;
  userStatus: number;
  banned: boolean;
}

export interface LoginCredentials {
  username: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
}
