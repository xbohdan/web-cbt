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
import validatePassword from '../../helpers/validatePassword';
import useRegistration from '../../hooks/useRegistration';
import {Gender} from '../../types/User';

const {Title} = Typography;

const Registration = () => {
  const [form] = Form.useForm();
  const [radioValue, setRadioValue] = useState<Gender>('would rather not say');
  const {isLoading, onSubmit, onSubmitFailed} = useRegistration(form);

  const setGender = (e: RadioChangeEvent) => {
    setRadioValue(e.target.value);
  };

  return (
    <div className="registration">
      <div className="authSupform">
        <Title level={2}>Sign Up</Title>
      </div>
      <Form
        form={form}
        name="registrationForm"
        onFinish={onSubmit}
        onFinishFailed={onSubmitFailed}
        initialValues={{
          gender: 'would rather not say',
        }}
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
