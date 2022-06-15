import './AdminMoodTests.css';
import {Button, Table} from 'antd';
import {MoodTestResponse} from '../../types/MoodTest';
import useAdminMoodTests from '../../hooks/useAdminMoodTests';

const AdminMoodTests = () => {
  const {allMoodTests, OnDelete} = useAdminMoodTests();

  return (
    <Table
      className="adminMoodTestsTable"
      dataSource={allMoodTests}
    >
      <Table.Column title="User ID" dataIndex="userId" key="userId" />
      <Table.Column
        title="Evaluation ID"
        dataIndex="evaluationId"
        key="evaluationId"
      />
      <Table.Column title="Category" dataIndex="category" key="category" />
      <Table.Column title="Question 1" dataIndex="question1" key="question1" />
      <Table.Column title="Question 2" dataIndex="question2" key="question2" />
      <Table.Column title="Question 3" dataIndex="question3" key="question3" />
      <Table.Column title="Question 4" dataIndex="question4" key="question4" />
      <Table.Column title="Question 5" dataIndex="question5" key="question5" />
      <Table.Column
        key="delete"
        render={(_, record: MoodTestResponse) => (
          <Button
            type="text"
            onClick={() => OnDelete(record.evaluationId)}
            danger
          >
            Delete
          </Button>
        )}
      />
    </Table>
  );
};

export default AdminMoodTests;