import React from 'react';
import {Form, Typography, Input, Button} from 'antd';

import './Settings.css';
import useSettings from '../../hooks/useSettings';

const {Title} = Typography;

const Settings = () => {
  const [form] = Form.useForm();
  const {
    isLoading,
    isEditing,
    setEditingState,
    userLogin,
    onSubmit,
    onSubmitFailed,
  } = useSettings();

  return (
    <div className="settings">
      <div className="authSupform">
        <Title level={2}>Manage Account</Title>
      </div>
      <Form
        form={form}
        name="settingsForm"
        onFinish={onSubmit}
        onFinishFailed={onSubmitFailed}
        autoComplete="off"
        layout="horizontal"
      >
        <Form.Item
          name="login"
          rules={[{type: 'email', message: 'Email is not valid'}]}
        >
          <Input defaultValue={userLogin} disabled={isLoading || !isEditing} />
        </Form.Item>
        <Form.Item name="password">
          <Input.Password
            placeholder="Password"
            disabled={isLoading || !isEditing}
          />
        </Form.Item>
        <Form.Item name="gender">
          <Input
            list="browsers"
            disabled={isLoading || !isEditing}
            placeholder="would rather not say"
            readOnly
          />
          <datalist id="browsers">
            <option value="male" />
            <option value="female" />
            <option value="other" />
            <option value="would rather not say" />
          </datalist>
        </Form.Item>
        <Form.Item name="age">
          <Input defaultValue="25" disabled={isLoading || !isEditing} />
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
