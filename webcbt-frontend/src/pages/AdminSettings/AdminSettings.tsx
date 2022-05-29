import React from 'react';
import {Button, Space, Table} from 'antd';
import {GetUserResponse} from '../../types/User';
import './AdminSettings.css';
import Title from 'antd/es/typography/Title';
import useAdminSettings from '../../hooks/useAdminSettings';

const AdminSettings = () => {
  const {
    userData,
    OnDelete
  } = useAdminSettings();

  return(
    <div className="adminSettings">
      <Title level={2}>Manage Accounts</Title>
      <Table showHeader className="table" size="middle" bordered dataSource={userData}>
        <Table.Column title="User ID" dataIndex="userId" key="userId" />
        <Table.Column title="Login" dataIndex="login" key="login" />
        <Table.Column title="Age" dataIndex="age" key="age" />
        <Table.Column title="Gender" dataIndex="gender" key="gender" />
        <Table.Column
          title="Is Admin"
          key="userStatus"
          dataIndex="userStatus"
          render={text => text ? "Yes" : "No"}
        />
        <Table.Column
          title="Is Banned"
          key="banned"
          dataIndex="banned"
          render={text => text ? "Yes" : "No"}
          />
        <Table.Column
          title=""
          align="center"
          key="action"
          render={(text, record:GetUserResponse) => (
            <Space size="middle">
              <Button type="text" className="deleteButton" onClick={()=> OnDelete(record.userId)}>Delete</Button>
            </Space>
          )}
        />
      </Table>
    </div>
  );
}

export default AdminSettings;
