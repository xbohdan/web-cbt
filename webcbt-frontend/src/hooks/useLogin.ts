import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import {FormInstance} from 'antd';
import {useNavigate} from 'react-router-dom';
import {toast} from 'react-toastify';
import {isDev} from '../config';
import handleLoginErrors from '../helpers/handleLoginErrors';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import {useLoginMutation} from '../store/services/auth';
import {setUser} from '../store/user/slice';
import {LoginCredentials, LoginResponse, User} from '../types/User';
import useAppDispatch from './useAppDispatch';

const useLogin = (form: FormInstance) => {
  const [login, {isLoading}] = useLoginMutation();

  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const onSubmit = async (formData: LoginCredentials) => {
    try {
      // Send request (or return mocked response in development mode)
      let loginResponse: LoginResponse;
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
      };

      // Set user in Redux store
      dispatch(setUser(user));

      // Set user in localStorage (to keep user logged in after page refresh)
      localStorage.setItem('LOGIN', formData.login);
      localStorage.setItem('TOKEN', loginResponse.accessToken);

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
