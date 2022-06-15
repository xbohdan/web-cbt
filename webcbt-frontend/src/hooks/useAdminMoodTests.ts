import {useEffect, useState} from 'react';
import {isDev} from '../config';
import {toast} from 'react-toastify';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import {
  useDeleteMoodTestMutation,
  useGetAllMoodTestsMutation,
} from '../store/services/evaluation';
import {MoodTestResponse} from '../types/MoodTest';
import mockMoodTests from '../helpers/mockMoodTests';

const useAdminMoodTests = () => {
  const [getAllMoodTests] = useGetAllMoodTestsMutation();
  let [allMoodTests, setAllMoodTests] = useState<MoodTestResponse[]>([]);
  const [deleteMoodTest] = useDeleteMoodTestMutation();

  useEffect(() => {
    const fetchAllMoodTests = async () => {
      let allTests: MoodTestResponse[];
      try {
        if (isDev) {
          allTests = mockMoodTests;
        } else {
          allTests = await getAllMoodTests().unwrap();
        }
        setAllMoodTests(allTests);
      } catch (err) {
        toast.error('Unknown error. Please try later');
      }
    };

    fetchAllMoodTests().then(() => {});
  }, [getAllMoodTests]);

  const OnDelete = async (evaluationId: number) => {
    try {
      if (isDev) {
        await returnDataWithDelay(204, 'fast 3G');
      } else {
        await deleteMoodTest(evaluationId).unwrap();
      }
      toast.success('Mood test was successfully deleted');
    } catch (err) {
      toast.error('Unknown error. Please try later');
    }
  };

  return {
    allMoodTests,
    OnDelete,
  };
};

export default useAdminMoodTests;