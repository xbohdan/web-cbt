import {MoodTestRequest} from '../../types/MoodTest';
import { mainApi } from './main';

export const moodTestApi = mainApi.injectEndpoints({
  endpoints: (builder) => ({
    submitMoodTest: builder.mutation<{}, MoodTestRequest>({
      query: (answer: MoodTestRequest) => ({
        url: 'moodtests',
        method: 'POST',
        body: answer,
      }),
    }),
  }),
});

export const {useSubmitMoodTestMutation} = moodTestApi;
