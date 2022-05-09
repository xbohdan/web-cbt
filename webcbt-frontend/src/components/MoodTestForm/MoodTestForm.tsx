import {Button, Form, Rate, Typography} from 'antd';
import './MoodTestForm.css';
import {ReactNode} from 'react';
import MoodTest from '../../types/MoodTest';

const {Title} = Typography;

interface Props {
  title: string;
  allQuestions: string[];
  character?: ReactNode;
}

const MoodTestForm = ({title, allQuestions, character}: Props) => {
  const [form] = Form.useForm();

  const submitTest = (formData: MoodTest) => {
    console.log(formData);
  };

  return (
    <div className="moodTestForm">
      <Title>{title}</Title>
      <Form form={form} name="moodTestForm" onFinish={submitTest}>
        {allQuestions.map((question, index) => {
          return (
            <div key={index} className="moodTestQuestion">
              <span className="questionText">{question}</span>
              <Form.Item
                name={`q${index}`}
                rules={[{required: true, message: 'Required'}]}
              >
                <Rate className="moodTestRate" character={character} />
              </Form.Item>
            </div>
          );
        })}
        <Form.Item>
          <Button className="moodTestSubmit" type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default MoodTestForm;
