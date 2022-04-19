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
import {Link} from 'react-router-dom';

import './Registration.css';

const {Title} = Typography;

type gender = 'male' | 'female' | 'other' | 'would rather not say';

interface Credentials {
  username: string;
  password: string;
  gender: gender;
  age?: number;
}

const Registration = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [radioValue, setRadioValue] = useState<gender>('would rather not say');

  const registerUser = (credentials: Credentials) => {
    console.log(credentials);
    setIsLoading(true);
  };

  // @ts-ignore
  const displayErrors = (errorInfo) => {
    console.log(errorInfo);
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
        <Form.Item name="gender">
          <Radio.Group
            onChange={setGender}
            defaultValue="would rather not say"
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
