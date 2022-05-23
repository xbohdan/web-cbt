import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import {FormInstance} from 'antd';
import {useNavigate} from 'react-router-dom';
import {toast} from 'react-toastify';
import {isDev} from '../config';
import handleRegistrationErrors from '../helpers/handleRegistrationError';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import {useRegistrationMutation} from '../store/services/auth';
import {RegistrationForm, RegistrationRequest} from '../types/User';

const useRegistration = (form: FormInstance) => {
  const [register, {isLoading}] = useRegistrationMutation();

  const navigate = useNavigate();

  const onSubmit = async (formData: RegistrationForm) => {
    try {
      // Prepare request object
      if (!formData.age) delete formData.age;
      else formData.age = parseInt(formData.age as string);
      let registrationRequest: RegistrationRequest = {
        ...formData,
        userStatus: 0,
        banned: false,
      };

      // Send request (or return mocked response in development mode)
      if (isDev) {
        await returnDataWithDelay(200, 'fast 3G');
        // Throw an error to see error handling
        // throw {status: 400};
      } else {
        await register(registrationRequest).unwrap();
      }

      // Display notification about successful registration
      toast.success('Created a new account!');

      // Redirect user to the login page, if the response was ok
      navigate('/login');
    } catch (err) {
      if ('status' in err) {
        handleRegistrationErrors(err as FetchBaseQueryError, form);
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

export default useRegistration;
