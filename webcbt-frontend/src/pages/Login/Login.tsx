import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import React from 'react';
import {Form, Typography, Input, Button} from 'antd';
import {Link, useNavigate} from 'react-router-dom';

import './Login.css';
import {toast} from 'react-toastify';
import {isDev} from '../../config';
import handleLoginErrors from '../../helpers/handleLoginErrors';
import returnDataWithDelay from '../../helpers/returnDataWithDelay';
import useAppDispatch from '../../hooks/useAppDispatch';
import {useLoginMutation} from '../../store/services/auth';
import {setUser} from '../../store/user/slice';
import {LoginCredentials, LoginResponse, User} from '../../types/User';

const {Title} = Typography;

const Login = () => {
  const [form] = Form.useForm();
  const [login, {isLoading}] = useLoginMutation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const onFinish = async (formData: LoginCredentials) => {
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
      localStorage.setItem('USERNAME', formData.login);
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
  const onFinishFailed = (errorInfo) => {
    console.error(errorInfo);
  };

  return (
    <div className="login">
      <div className="authSupform">
        <Title level={2}>Log In</Title>
      </div>
      <Form
        form={form}
        name="loginForm"
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
        autoComplete="off"
        layout="horizontal"
      >
        <Form.Item
          name="login"
          rules={[{required: true, message: 'Username is required'}]}
        >
          <Input placeholder="Username" disabled={isLoading} />
        </Form.Item>
        <Form.Item
          name="password"
          rules={[{required: true, message: 'Password is required'}]}
        >
          <Input.Password placeholder="Password" disabled={isLoading} />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={isLoading} block>
            Log In
          </Button>
        </Form.Item>
      </Form>
      <div className="authSubform">
        <p>
          Don't have an account?&nbsp;
          <Link to="/registration">Sign Up</Link>
        </p>
      </div>
    </div>
  );
};

export default Login;
