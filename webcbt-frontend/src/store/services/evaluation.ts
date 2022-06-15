import {MoodTestResponse} from '../../types/MoodTest';
import { mainApi } from './main';

export const moodTestApi = mainApi.injectEndpoints({
  endpoints: (builder) => ({
    getAllMoodTests: builder.mutation<MoodTestResponse[], void>({
      query: () => ({
        url: 'evaluation',
        method: 'GET',
      }),
    }),
    deleteMoodTest: builder.mutation<{}, number>({
      query: (evaluationId: number) => ({
        url: `evaluation/${evaluationId}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const {useGetAllMoodTestsMutation, useDeleteMoodTestMutation} =
  moodTestApi;
