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
import Login from '../../pages/Login/Login';
import MoodTests from '../../pages/MoodTests/MoodTests';
import Registration from '../../pages/Registration/Registration';
import Settings from '../../pages/Settings/Settings';
import selectIsAuth from '../../store/user/selectors/selectIsAuth';
import MoodTestForm from '../MoodTestForm/MoodTestForm';
import AdminSettings from '../../pages/AdminSettings/AdminSettings';
import selectStatus from '../../store/user/selectors/selectStatus';

const Router = () => {
  const isAuth = useAppSelector(selectIsAuth);
  const isAdmin = useAppSelector(selectStatus);

  return (
    <Routes>
      {isAuth && isAdmin && (
        <>
          <Route path="/settings" element={<Settings />} />
        </>
      )}
      {isAuth && !isAdmin && (
        <>
          <Route path="/settings" element={<AdminSettings />} />
        </>
      )}
      {!isAuth && (
        <>
          <Route path="/registration" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          <Route path="*" element={<Navigate replace to="/login" />} />
        </>
      )}
      {isAuth && (
        <>
          <Route path="/settings" element={<Settings />} />
          <Route path="/adminsettings" element={<AdminSettings />} />
          <Route path="/tests" element={<MoodTests />} />
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
    </Routes>
  );
};

export default Router;
