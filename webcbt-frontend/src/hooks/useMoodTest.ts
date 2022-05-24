import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import {FormInstance} from 'antd';
import {useNavigate} from 'react-router-dom';
import {toast} from 'react-toastify';
import {isDev} from '../config';
import handleMoodTestErrors from '../helpers/handleMoodTestErrors';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import {useSubmitMoodTestMutation} from '../store/services/moodtest';
import MoodTest, {MoodTestCategory, MoodTestRequest} from '../types/MoodTest';

const useMoodTest = (form: FormInstance, testCategory: MoodTestCategory) => {
  const [submitMoodTest, {isLoading}] = useSubmitMoodTestMutation();

  const navigate = useNavigate();

  const onSubmit = async (formData: MoodTest) => {
    try {
      const moodTestRequest: MoodTestRequest = {
        userId: 0,
        category: testCategory,
        ...formData,
      };
      console.log(moodTestRequest);

      if (isDev) {
        await returnDataWithDelay(200, 'fast 3G');
      } else {
        await submitMoodTest(moodTestRequest).unwrap();
      }

      // Display notification about successful registration
      toast.success('Submitted!');

      // Redirect user to the mood tests page, if the response was ok
      navigate('/tests');
    } catch (err) {
      if ('status' in err) {
        handleMoodTestErrors(err as FetchBaseQueryError);
      } else {
        toast.error('Unknown error. Please try later');
      }
    }
  };

  // @ts-ignore
  const onSubmitFailed = (errorInfo) => {
    console.error(errorInfo);
  };

  return {isLoading, onSubmit, onSubmitFailed};
};

export default useMoodTest;
