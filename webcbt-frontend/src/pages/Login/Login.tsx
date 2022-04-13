import React, {useState} from 'react';
import {
  Form,
  Typography,
  Input,
  Button,
} from 'antd';
import {Link} from 'react-router-dom';

import './Login.css';

const {Title} = Typography;

interface Credentials {
  username: string;
  password: string;
}

const Login = () => {
  const [isLoading, setIsLoading] = useState(false);

  const onFinish = (credentials: Credentials) => {
    console.log(credentials);
    setIsLoading(true);
  };

  // @ts-ignore
  const onFinishFailed = (errorInfo) => {
    console.log(errorInfo);
  };

  return (
    <div className="login">
      <div className="authSupform">
        <Title level={2}>Log In</Title>
      </div>
      <Form
        name="loginForm"
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
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
