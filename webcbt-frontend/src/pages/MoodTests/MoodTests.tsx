import {Typography} from 'antd';

import './MoodTests.css';
import MoodTestBlock from '../../components/MoodTestBlock/MoodTestBlock';

// @ts-ignore
import depressionImg from './depression.jpg';
// @ts-ignore
import anxietyImg from './anxiety.jpg';
// @ts-ignore
import addictionsImg from './addictions.jpg';
// @ts-ignore
import angerImg from './anger.jpg';
// @ts-ignore
import relationshipsImg from './relationships.jpg';
// @ts-ignore
import happinessImg from './happiness.jpg';

const {Title} = Typography;

const MoodTests = () => {
  return (
    <div className="moodTestsContent">
      <Title>Mood Tests</Title>
      <div className="moodTestsBlocks">
        <MoodTestBlock
          title="Depression"
          cover={<img alt="depression image" src={depressionImg} />}
          link="/tests/depression"
        />
        <MoodTestBlock
          title="Anxiety"
          cover={<img alt="anxiety image" src={anxietyImg} />}
          link="/tests/anxiety"
        />
        <MoodTestBlock
          title="Addictions"
          cover={<img alt="depression image" src={addictionsImg} />}
          link="/tests/addictions"
        />
        <MoodTestBlock
          title="Anger"
          cover={<img alt="depression image" src={angerImg} />}
          link="/tests/anger"
        />
        <MoodTestBlock
          title="Relationships"
          cover={<img alt="depression image" src={relationshipsImg} />}
          link="/tests/relationships"
        />
        <MoodTestBlock
          title="Happiness"
          cover={<img alt="depression image" src={happinessImg} />}
          link="/tests/happiness"
        />
      </div>
    </div>
  );
};

export default MoodTests;
