import React, {useState} from 'react';
import {Form, Typography, Input, Button} from 'antd';

import './Settings.css';
import {toast} from 'react-toastify';
import {isDev} from '../../config';
import {setUser} from '../../store/user/slice';
import returnDataWithDelay from '../../helpers/returnDataWithDelay';
import useAppDispatch from '../../hooks/useAppDispatch';
import {ManageAccountForm, ManageAccountResponse, User} from '../../types/User';
import useAppSelector from '../../hooks/useAppSelector';
import selectLogin from '../../store/user/selectors/selectLogin';

const {Title} = Typography;

const Settings = () => {
  const [form] = Form.useForm();
  const [isLoading, setLoadingState] = useState(false);
  const dispatch = useAppDispatch();
  const userLogin = useAppSelector(selectLogin);
  const [isEditing, setEditingState] = useState(false);

  const onFinish = async (formData: ManageAccountForm) => {
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
    }catch (err){
      /*handling errors*/
    }
  };

  // @ts-ignore
  const onFinishFailed = (errorInfo) => {
    console.error(errorInfo);
  };

  return(
    <div className="settings">
      <div className="authSupform">
        <Title level={2}>Manage Account</Title>
      </div>
      <Form
        form={form}
        name="settingsForm"
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
        autoComplete="off"
        layout="horizontal"
      >
        <Form.Item
          name="login"
          rules={[
            {type: 'email', message: 'Email is not valid'},
          ]}
        >
          <Input placeholder={userLogin} disabled={isLoading || !isEditing} />
        </Form.Item>
        <Form.Item
          name="password"
        >
          <Input.Password placeholder="Password" disabled={isLoading || !isEditing} />
        </Form.Item>
        <Form.Item
          name="gender"
        >
          <Input list="browsers" disabled={isLoading || !isEditing} placeholder="would rather not say" readOnly/>
          <datalist id="browsers">
            <option value="male"/>
            <option value="female"/>
            <option value="other"/>
            <option value="would rather not say"/>
          </datalist>
        </Form.Item>
        <Form.Item
          name="age"
        >
          <Input placeholder="25" disabled={isLoading || !isEditing} />
        </Form.Item>
        <Form.Item hidden={!isEditing}>
          <Button type="primary" htmlType="submit" loading={isLoading} block>
            Save
          </Button>
        </Form.Item>
        <Button hidden={isEditing} onClick={() => setEditingState(true)} block>
          Edit
        </Button>
      </Form>
    </div>
  );
};

export default Settings;
