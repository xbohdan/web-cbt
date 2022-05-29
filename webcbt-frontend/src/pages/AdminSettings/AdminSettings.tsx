import React, {useEffect, useState} from 'react';
import {Button, Space, Table} from 'antd';
import {isDev} from '../../config';
import {useDeleteUserMutation, useGetUserAdminMutation} from '../../store/services/auth';
import {GetUserResponse} from '../../types/User';
import {toast} from 'react-toastify';
import './AdminSettings.css';
import Title from 'antd/es/typography/Title';
import returnDataWithDelay from '../../helpers/returnDataWithDelay';

const AdminSettings = () => {
  const [getUserData] = useGetUserAdminMutation();
  const [userData, setUserData] = useState<GetUserResponse[]>([]);
  const [deleteUser] = useDeleteUserMutation();

  useEffect(() => {
    const GetUserData = async () =>
    {
      let userDataList: GetUserResponse[];
      try {
        if (isDev) {
          userDataList = [
            {
              userId: 100,
              login: "mockLogin0",
              age: 18,
              gender: "male",
              userStatus: 0,
              banned: false
            },
            {
              userId: 101,
              login: "mockLogin1",
              age: 20,
              gender: "female",
              userStatus: 0,
              banned: false
            },
            {
              userId: 102,
              login: "mockLogin2",
              age: 23,
              gender: "other",
              userStatus: 0,
              banned: false
            }
          ]
        } else {
          userDataList = await getUserData().unwrap();
        }
        setUserData(userDataList);
      } catch (err) {
        toast.error('Unknown error. Please try later');
      }
    };

    GetUserData().then(r => {});
  })

  const OnDelete = async (userId: number) => {
    try {
      if (isDev) {
        await returnDataWithDelay(204, 'fast 3G');
      }else{
        await deleteUser(userId.toString()).unwrap();
      }
      toast.success('User was successfully deleted');
    } catch (err) {
      toast.error('Unknown error. Please try later');
    }
  };

  return(
    <div className="adminSettings">
      <Title level={2}>Manage Users</Title>
      <Table size="middle" bordered dataSource={userData}>
        <Table.Column title="ID" dataIndex="userId" key="userId" />
        <Table.Column title="Login" dataIndex="login" key="login" />
        <Table.Column title="Age" dataIndex="age" key="age" />
        <Table.Column title="Gender" dataIndex="gender" key="gender" />
        <Table.Column
          title="Action"
          key="action"
          render={(text, record:GetUserResponse) => (
            <Space size="middle">
              <Button className="deleteButton" onClick={()=> OnDelete(record.userId)}>Delete</Button>
            </Space>
          )}
        />
      </Table>
    </div>
  );
}

export default AdminSettings;
