import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import React, {useState} from 'react';
import {
  Form,
  Typography,
  Input,
  Radio,
  InputNumber,
  Button,
  RadioChangeEvent,
} from 'antd';
import {Link, useNavigate} from 'react-router-dom';

import './Registration.css';
import {toast} from 'react-toastify';
import {isDev} from '../../config';
import handleRegistrationErrors from '../../helpers/handleRegistrationError';
import returnDataWithDelay from '../../helpers/returnDataWithDelay';
import validatePassword from '../../helpers/validatePassword';
import {useRegistrationMutation} from '../../store/services/auth';
import {RegistrationRequest, Gender, RegistrationForm} from '../../types/User';

const {Title} = Typography;

const Registration = () => {
  const [form] = Form.useForm();
  const [radioValue, setRadioValue] = useState<Gender>('would rather not say');

  const [register, {isLoading}] = useRegistrationMutation();

  const navigate = useNavigate();

  const registerUser = async (formData: RegistrationForm) => {
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
      } else {
        await register(registrationRequest);
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
  const displayErrors = (errorInfo) => {
    console.error(errorInfo);
  };

  const setGender = (e: RadioChangeEvent) => {
    setRadioValue(e.target.value);
  };

  return (
    <div className="registration">
      <div className="authSupform">
        <Title level={2}>Sign Up</Title>
      </div>
      <Form
        name="registrationForm"
        onFinish={registerUser}
        onFinishFailed={displayErrors}
        initialValues={{
          gender: 'would rather not say',
        }}
        autoComplete="off"
        layout="horizontal"
      >
        <Form.Item
          name="username"
          rules={[{required: true, message: 'Username is required'}]}
        >
          <Input placeholder="Username" disabled={isLoading} />
        </Form.Item>
        <Form.Item
          name="password"
          rules={[
            {required: true, message: 'Password is required'},
            () => ({
              validator(_, password: string) {
                return validatePassword(password);
              },
            }),
          ]}
        >
          <Input.Password placeholder="Password" disabled={isLoading} />
        </Form.Item>
        <Form.Item name="gender">
          <Radio.Group
            onChange={setGender}
            value={radioValue}
            buttonStyle="solid"
            disabled={isLoading}
          >
            <Radio.Button value="male">Male</Radio.Button>
            <Radio.Button value="female">Female</Radio.Button>
            <Radio.Button value="other">Other</Radio.Button>
            <Radio.Button value="would rather not say">
              Would rather not say
            </Radio.Button>
          </Radio.Group>
        </Form.Item>
        <Form.Item name="age">
          <label>
            Age:&nbsp;&nbsp;&nbsp;
            <InputNumber
              min={12}
              max={125}
              name="registrationAge"
              disabled={isLoading}
            />
          </label>
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={isLoading} block>
            Sign Up
          </Button>
        </Form.Item>
      </Form>
      <div className="authSubform">
        <p>
          Have an account?&nbsp;
          <Link to="/login">Log In</Link>
        </p>
      </div>
    </div>
  );
};

export default Registration;
