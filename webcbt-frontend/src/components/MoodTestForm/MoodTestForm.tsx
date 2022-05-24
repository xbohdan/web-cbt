import {Button, Form, Rate, Typography} from 'antd';
import './MoodTestForm.css';
import {ReactNode} from 'react';
import useMoodTest from '../../hooks/useMoodTest';
import {MoodTestCategory} from '../../types/MoodTest';

const {Title} = Typography;

interface Props {
  title: MoodTestCategory;
  allQuestions: string[];
  character?: ReactNode;
}

const MoodTestForm = ({title, allQuestions, character}: Props) => {
  const [form] = Form.useForm();
  const {isLoading, onSubmit, onSubmitFailed} = useMoodTest(form, title);

  return (
    <div className="moodTestForm">
      <Title>{title}</Title>
      <Form
        form={form}
        name="moodTestForm"
        onFinish={onSubmit}
        onFinishFailed={onSubmitFailed}
      >
        {allQuestions.map((question, index) => {
          return (
            <div key={index} className="moodTestQuestion">
              <span className="questionText">{question}</span>
              <Form.Item
                name={`question${index + 1}`}
                rules={[{required: true, message: 'Required'}]}
              >
                <Rate className="moodTestRate" character={character} />
              </Form.Item>
            </div>
          );
        })}
        <Form.Item>
          <Button
            className="moodTestSubmit"
            type="primary"
            htmlType="submit"
            loading={isLoading}
          >
            Submit
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default MoodTestForm;
