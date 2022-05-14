import React from 'react';
import {Form, Typography, Input, Button} from 'antd';
import {Link} from 'react-router-dom';

import './Login.css';
import useLogin from '../../hooks/useLogin';

const {Title} = Typography;

const Login = () => {
  const [form] = Form.useForm();

  const {isLoading, onSubmit, onSubmitFailed} = useLogin(form);

  return (
    <div className="login">
      <div className="authSupform">
        <Title level={2}>Log In</Title>
      </div>
      <Form
        form={form}
        name="loginForm"
        onFinish={onSubmit}
        onFinishFailed={onSubmitFailed}
        autoComplete="off"
        layout="horizontal"
      >
        <Form.Item
          name="login"
          rules={[
            {required: true, message: 'Email is required'},
            {type: 'email', message: 'Email is not valid'},
          ]}
        >
          <Input placeholder="Email" disabled={isLoading} />
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
