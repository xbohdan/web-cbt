import {useEffect, useState} from 'react';
import {toast} from 'react-toastify';
import {isDev} from '../config';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import selectLogin from '../store/user/selectors/selectLogin';
import {setUser} from '../store/user/slice';
import {ManageAccountForm, PutUserRequest, User} from '../types/User';
import useAppDispatch from './useAppDispatch';
import useAppSelector from './useAppSelector';
import selectAge from '../store/user/selectors/selectAge';
import selectGender from '../store/user/selectors/selectGender';
import {usePutUserMutation} from '../store/services/auth';
import selectId from '../store/user/selectors/selectId';
import selectToken from '../store/user/selectors/selectToken';
import selectStatus from '../store/user/selectors/selectStatus';
import selectBanned from '../store/user/selectors/selectBanned';

const useSettings = () => {
  const [putUser, {isLoading}] = usePutUserMutation();
  const dispatch = useAppDispatch();
  const userLogin = useAppSelector(selectLogin);
  const userAge = useAppSelector(selectAge);
  const userGender = useAppSelector(selectGender);
  const userId = useAppSelector(selectId);
  const userToken = useAppSelector(selectToken);
  const userStatus = useAppSelector(selectStatus);
  const userIsBanned = useAppSelector(selectBanned);
  const [isEditing, setEditingState] = useState(false);

  const onSubmit = async (formData: ManageAccountForm) => {
    try {
      if (!formData.login) formData.login = userLogin;
      if (!formData.gender) formData.gender = userGender;
      if (!formData.age) formData.age = userAge;
      if (!formData.password) delete formData.password;
      if (isDev) {
        await returnDataWithDelay(200, 'fast 3G');
      }else{
        const putUserRequest: PutUserRequest = {
          userId: userId,
          body: {
            login: formData.login,
            password: formData.password,
            age: formData.age,
            gender: formData.gender
          }
        }
        await putUser(putUserRequest).unwrap()
      }

      let user: User = {
        login: formData.login,
        accessToken: userToken!!,
        userId: userId,
        gender: formData.gender!!,
        userStatus: userStatus,
        banned: userIsBanned,
        age: formData.age
      };

      dispatch(setUser(user));

      localStorage.setItem('LOGIN', formData.login);
      localStorage.setItem('AGE', formData.age!!.toString());
      localStorage.setItem('GENDER', formData.gender);

      toast.success('Account settings changed!');
      setEditingState(false);
    } catch (err) {
      toast.error('Unknown error. Please try later');
    }
  };

  // @ts-ignore
  const onSubmitFailed = (errorInfo) => {
    console.error(errorInfo);
  };

  return {
    isLoading,
    isEditing,
    setEditingState,
    userLogin,
    onSubmit,
    onSubmitFailed,
    userAge,
    userGender
  };
};

export default useSettings;
