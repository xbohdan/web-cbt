// Or from '@reduxjs/toolkit/query' if not using the auto-generated hooks
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import getToken from '../../helpers/getToken';
import { RootState } from '../store';

// initialize an empty api service that we'll inject endpoints into later as needed
export const mainApi = createApi({
    baseQuery: fetchBaseQuery({
        baseUrl: 'https://130.162.232.178/api/',
        prepareHeaders: (headers, { getState }) => {
            // If we have a token, let's use that for authenticated requests
            const token = getToken(getState() as RootState);
            if (token) {
                headers.set('Authorization', `Bearer ${token}`);
            }
            return headers;
        },
    }),
    endpoints: () => ({}),
})