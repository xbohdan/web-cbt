import {useDeleteUserMutation, useGetUserAdminMutation} from '../store/services/auth';
import {useEffect, useState} from 'react';
import {GetUserResponse} from '../types/User';
import {isDev} from '../config';
import {toast} from 'react-toastify';
import returnDataWithDelay from '../helpers/returnDataWithDelay';

const useAdminSettings = () => {
  const [getUserData] = useGetUserAdminMutation();
  const [userData, setUserData] = useState<GetUserResponse[]>([]);
  const [deleteUser] = useDeleteUserMutation();

  useEffect(() => {
    const GetUserData = async () =>
    {
      let userDataList: GetUserResponse[];
      try {
        if (isDev) {
          userDataList = [
            {
              userId: 100,
              login: "mockLogin0",
              age: 18,
              gender: "male",
              userStatus: 0,
              banned: false
            },
            {
              userId: 101,
              login: "mockLogin1",
              age: 20,
              gender: "female",
              userStatus: 0,
              banned: false
            },
            {
              userId: 102,
              login: "mockLogin2",
              age: 23,
              gender: "other",
              userStatus: 0,
              banned: false
            }
          ]
        } else {
          userDataList = await getUserData().unwrap();
        }
        setUserData(userDataList);
      } catch (err) {
        toast.error('Unknown error. Please try later');
      }
    };

    GetUserData().then(() => {});
  }, [getUserData, deleteUser])

  const OnDelete = async (userId: number) => {
    try {
      if (isDev) {
        await returnDataWithDelay(204, 'fast 3G');
      }else{
        await deleteUser(userId.toString()).unwrap();
      }
      toast.success('User was successfully deleted');
    } catch (err) {
      toast.error('Unknown error. Please try later');
    }
  };

  return {
    setUserData,
    userData,
    OnDelete
  };
}

export default useAdminSettings;
