import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import getToken from '../../helpers/getToken';
import {
  LoginCredentials,
  LoginResponse,
  RegistrationRequest,
} from '../../types/User';
import {RootState} from '../store';

export const authApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://130.162.232.178/api/user/',
    prepareHeaders: (headers, {getState}) => {
      // If we have a token, let's use that for authenticated requests
      const token = getToken(getState() as RootState);
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginCredentials>({
      query: (credentials: LoginCredentials) => ({
        url: 'login',
        method: 'POST',
        body: credentials,
      }),
    }),
    registration: builder.mutation<{}, RegistrationRequest>({
      query: (credentials: RegistrationRequest) => ({
        url: '/',
        method: 'POST',
        body: credentials,
      }),
    }),
  }),
});

export const {useLoginMutation, useRegistrationMutation} = authApi;
