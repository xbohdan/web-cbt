import './AdminMoodTests.css';
import {Button, Table} from 'antd';
import {useLayoutEffect, useState} from 'react';
import {isDev} from '../../config';
import mockMoodTests from '../../helpers/mockMoodTests';
import {useGetAllMoodTestsMutation, useDeleteMoodTestMutation} from '../../store/services/evaluation';
import {MoodTestResponse} from '../../types/MoodTest';

const columns = [
  {
    title: 'User ID',
    dataIndex: 'userId',
    key: 'userId',
  },
  {
    title: 'Evaluation ID',
    dataIndex: 'evaluationId',
    key: 'evaluationId',
  },
  {
    title: 'Category',
    dataIndex: 'category',
    key: 'category',
  },
  {
    title: 'Question 1',
    dataIndex: 'question1',
    key: 'question1',
  },
  {
    title: 'Question 2',
    dataIndex: 'question2',
    key: 'question2',
  },
  {
    title: 'Question 3',
    dataIndex: 'question3',
    key: 'question3',
  },
  {
    title: 'Question 4',
    dataIndex: 'question4',
    key: 'question4',
  },
  {
    title: 'Question 5',
    dataIndex: 'question5',
    key: 'question5',
  },
  {
    key: 'delete',
    // @ts-ignore
    render: (_, record: MoodTestResponse) => (
      <Button onClick={async () => {
        const [deleteMoodTest] = useDeleteMoodTestMutation();
        await deleteMoodTest(record.evaluationId);
        }} type="text" danger>
        Delete
      </Button>
    ),
  },
];

const AdminMoodTests = () => {
  const [getAllMoodTests] = useGetAllMoodTestsMutation();
  let [allMoodTests, setAllMoodTests] = useState<MoodTestResponse[]>([]);

  useLayoutEffect(() => {
    const fetchAllMoodTests = async () => {
      setAllMoodTests(await getAllMoodTests().unwrap());
    };
    fetchAllMoodTests();
  }, [getAllMoodTests]);

  return (
    <Table
      className="adminMoodTestsTable"
      dataSource={isDev ? mockMoodTests : allMoodTests}
      columns={columns}
    />
  );
};

export default AdminMoodTests;
