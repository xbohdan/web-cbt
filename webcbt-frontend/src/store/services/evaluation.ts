import {MoodTestResponse} from '../../types/MoodTest';
import { mainApi } from './main';

<<<<<<< HEAD
export const moodTestApi = mainApi.injectEndpoints({
=======
export const moodTestApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://school-se-back.monicz.pl/evaluation/',
    prepareHeaders: (headers, {getState}) => {
      // If we have a token, let's use that for authenticated requests
      const token = getToken(getState() as RootState);
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }
      return headers;
    },
  }),
>>>>>>> 48d5e30d9fbde2d6e3739f3a81fbdc1a14582ba2
  endpoints: (builder) => ({
    getAllMoodTests: builder.mutation<MoodTestResponse[], void>({
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
