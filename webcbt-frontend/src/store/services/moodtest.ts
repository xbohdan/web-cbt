import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import getToken from '../../helpers/getToken';
import {RootState} from '../store';
import {MoodTestRequest} from '../../types/MoodTest';

export const moodTestApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://130.162.232.178/api/moodtests/',
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
    submitMoodTest: builder.mutation<{}, MoodTestRequest>({
      query: (answer: MoodTestRequest) => ({
        url: '/',
        method: 'POST',
        body: answer,
      }),
    }),
  }),
});

export const {useSubmitMoodTestMutation} = moodTestApi;
