import {
  GetUserResponse,
  LoginCredentials,
  LoginResponse,
  PutUserRequest,
  RegistrationRequest,
} from '../../types/User';
import { mainApi } from './main';

export const authApi = mainApi.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginCredentials>({
      query: (credentials: LoginCredentials) => ({
        url: 'user/login',
        method: 'POST',
        body: credentials,
      }),
    }),
    registration: builder.mutation<{}, RegistrationRequest>({
      query: (credentials: RegistrationRequest) => ({
        url: 'user',
        method: 'POST',
        body: credentials,
      }),
    }),
    getUser: builder.mutation<GetUserResponse, string>({
      query: (userId) => ({
        url: `user/${userId}`,
        method: 'GET',
      }),
    }),
    putUser: builder.mutation<{}, PutUserRequest>({
      query: (credentials: PutUserRequest) => ({
        url: `user/${credentials.userId}`,
        method: 'PUT',
        body: credentials.body,
      }),
    }),
    getUserAdmin: builder.mutation<GetUserResponse[], void>({
      query: () => ({
        url: 'user',
        method: 'GET',
      }),
    }),
    deleteUser: builder.mutation<{}, string>({
      query: (userId) => ({
        url: `user/${userId}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const {
  useLoginMutation,
  useRegistrationMutation,
  useGetUserMutation,
  usePutUserMutation,
  useGetUserAdminMutation,
  useDeleteUserMutation,
} = authApi;
