import {useState} from 'react';
import {toast} from 'react-toastify';
import {isDev} from '../config';
import returnDataWithDelay from '../helpers/returnDataWithDelay';
import selectLogin from '../store/user/selectors/selectLogin';
import {setUser} from '../store/user/slice';
import {ManageAccountForm, ManageAccountResponse, User} from '../types/User';
import useAppDispatch from './useAppDispatch';
import useAppSelector from './useAppSelector';

const useSettings = () => {
  const [isLoading, setLoadingState] = useState(false);
  const dispatch = useAppDispatch();
  const userLogin = useAppSelector(selectLogin);
  const [isEditing, setEditingState] = useState(false);

  const onSubmit = async (formData: ManageAccountForm) => {
    try {
      setLoadingState(true);
      let manageAccountResponse: ManageAccountResponse;
      if (!formData.login) formData.login = userLogin;
      if (isDev) {
        manageAccountResponse = await returnDataWithDelay(
          {accessToken: 'mockToken'},
          'fast 3G',
        );
        let user: User = {
          login: formData.login,
          accessToken: 'mockToken',
        };
        // Set user in Redux store
        dispatch(setUser(user));

        localStorage.setItem('LOGIN', formData.login);
        localStorage.setItem('TOKEN', manageAccountResponse.accessToken);
      }

      toast.success('Account settings changed!');
      setLoadingState(false);
      setEditingState(false);
    } catch (err) {
      /*handling errors*/
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
  };
};

export default useSettings;
