import './MoodTestBlock.css';
import {Card} from 'antd';
import React, {ReactNode} from 'react';
import {Link} from 'react-router-dom';

interface Props {
  title: string;
  cover: ReactNode;
  link: string;
}

const MoodTestBlock = ({title, cover, link}: Props) => {
  return (
    <Link to={link}>
      <Card
        className="moodTestBlock"
        title={title}
        cover={cover}
        hoverable
        bordered={false}
      />
    </Link>
  );
};

export default MoodTestBlock;
