import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import {FormInstance} from 'antd';
import {useNavigate} from 'react-router-dom';
import {toast} from 'react-toastify';
import {isDev} from '../config';
import handleLoginErrors from '../helpers/handleLoginErrors';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import {useGetUserMutation, useLoginMutation} from '../store/services/auth';
import {setUser} from '../store/user/slice';
import {GetUserResponse, LoginCredentials, LoginResponse, User} from '../types/User';
import useAppDispatch from './useAppDispatch';
import jwt_decode from 'jwt-decode';

const useLogin = (form: FormInstance) => {
  const [login, {isLoading}] = useLoginMutation();
  const [getUser] = useGetUserMutation();

  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const onSubmit = async (formData: LoginCredentials) => {
    try {
      // Send request (or return mocked response in development mode)
      let loginResponse: LoginResponse;
      let getUserResponse: GetUserResponse;
      if (isDev) {
        loginResponse = await returnDataWithDelay(
          {accessToken: 'mockToken'},
          'fast 3G',
        );
      } else {
        loginResponse = await login(formData).unwrap();
      }

      // Prepare user object from successful response
      let user: User = {
        login: formData.login,
        accessToken: loginResponse.accessToken,
        userId: getUserResponse.userId,
        gender: getUserResponse.gender,
        userStatus: getUserResponse.userStatus,
        banned: getUserResponse.banned
      };

      // Set user in Redux store
      dispatch(setUser(user));

      // Set user in localStorage (to keep user logged in after page refresh)
      localStorage.setItem('LOGIN', formData.login);
      localStorage.setItem('TOKEN', loginResponse.accessToken);
      
      // GET user/{userId}
      if (isDev) {
        getUserResponse = await returnDataWithDelay(
          {accessToken: 'mockToken',
            banned: false,
            userId: 12345,
            userStatus: 0,
            login: 'mockLogin',
            gender: 'female',
            age: 19},
          'fast 3G',
        );
      } else {
        getUserResponse = await getUser(jwt_decode<{userId: string}>(loginResponse.accessToken).userId).unwrap();
      }
      
      localStorage.setItem('AGE', getUserResponse.age!!.toString());
      localStorage.setItem('GENDER', getUserResponse.gender);
      localStorage.setItem('STATUS', getUserResponse.userStatus.toString());
      localStorage.setItem('ID', getUserResponse.userId.toString());
      localStorage.setItem('BANNED', getUserResponse.banned.toString());

      // Display notification about successful login
      toast.success('Logged in!');

      // Redirect to the main page
      navigate('/');
    } catch (err) {
      if ('status' in err) {
        handleLoginErrors(err as FetchBaseQueryError, form);
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

export default useLogin;
