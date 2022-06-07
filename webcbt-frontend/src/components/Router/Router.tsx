import {
  AudioMutedOutlined,
  BugOutlined,
  CoffeeOutlined,
  FacebookOutlined,
  FireOutlined,
  LikeOutlined,
} from '@ant-design/icons';
import React from 'react';
import {Navigate, Route, Routes} from 'react-router-dom';
import {
  addictionsQuestions,
  angerQuestions,
  anxietyQuestions,
  depressionQuestions,
  happinessQuestions,
  relationshipsQuestions,
} from '../../helpers/moodQuestions';
import useAppSelector from '../../hooks/useAppSelector';
import AdminMoodTests from '../../pages/AdminMoodTests/AdminMoodTests';
import Login from '../../pages/Login/Login';
import Registration from '../../pages/Registration/Registration';
import Settings from '../../pages/Settings/Settings';
import UserMoodTests from '../../pages/UserMoodTests/UserMoodTests';
import selectIsAuth from '../../store/user/selectors/selectIsAuth';
import selectStatus from '../../store/user/selectors/selectStatus';
import MoodTestForm from '../MoodTestForm/MoodTestForm';

const Router = () => {
  const isAuth = useAppSelector(selectIsAuth);
  const userStatus = useAppSelector(selectStatus);

  return (
    <Routes>
      {!isAuth && (
        <>
          <Route path="/registration" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          <Route path="*" element={<Navigate replace to="/login" />} />
        </>
      )}
      {isAuth && userStatus === 0 && (
        <>
          <Route path="/settings" element={<Settings />} />
          <Route path="/tests" element={<UserMoodTests />} />
          <Route
            path="/tests/depression"
            element={
              <MoodTestForm
                title="Depression"
                allQuestions={depressionQuestions}
                character={<FacebookOutlined />}
              />
            }
          />
          <Route
            path="/tests/anxiety"
            element={
              <MoodTestForm
                title="Anxiety"
                allQuestions={anxietyQuestions}
                character={<AudioMutedOutlined />}
              />
            }
          />
          <Route
            path="/tests/addictions"
            element={
              <MoodTestForm
                title="Addictions"
                allQuestions={addictionsQuestions}
                character={<CoffeeOutlined />}
              />
            }
          />
          <Route
            path="/tests/anger"
            element={
              <MoodTestForm
                title="Anger"
                allQuestions={angerQuestions}
                character={<BugOutlined />}
              />
            }
          />
          <Route
            path="/tests/relationships"
            element={
              <MoodTestForm
                title="Relationships"
                allQuestions={relationshipsQuestions}
                character={<FireOutlined style={{transform: 'scaleX(-1)'}} />}
              />
            }
          />
          <Route
            path="/tests/happiness"
            element={
              <MoodTestForm
                title="Happiness"
                allQuestions={happinessQuestions}
                character={<LikeOutlined />}
              />
            }
          />
        </>
      )}
      {isAuth && userStatus === 1 && (
        <>
          <Route path="/tests" element={<AdminMoodTests />} />
        </>
      )}
    </Routes>
  );
};

export default Router;
