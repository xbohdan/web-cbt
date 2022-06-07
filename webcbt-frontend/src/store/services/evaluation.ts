import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import getToken from '../../helpers/getToken';
import {RootState} from '../store';
import {MoodTestRequest} from '../../types/MoodTest';

export const moodTestApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://130.162.232.178/api/evaluation/',
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
    getAllMoodTests: builder.mutation<MoodTestRequest[], void>({
      query: () => ({
        url: '/',
        method: 'GET',
      }),
    }),
    deleteMoodTest: builder.mutation<{}, number>({
      query: (evaluationId: number) => ({
        url: `${evaluationId}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const {useGetAllMoodTestsMutation, useDeleteMoodTestMutation} =
  moodTestApi;
