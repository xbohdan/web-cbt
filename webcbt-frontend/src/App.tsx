import {
  AudioMutedOutlined,
  BugOutlined,
  CoffeeOutlined,
  FacebookOutlined,
  FireOutlined,
  LikeOutlined,
} from '@ant-design/icons';
import React from 'react';
import {Routes, Route, Navigate} from 'react-router-dom';
import Footer from './components/Footer/Footer';
import Header from './components/Header/Header';
import Layout from './components/Layout/Layout';
import MoodTestForm from './components/MoodTestForm/MoodTestForm';
import {
  addictionsQuestions,
  angerQuestions,
  anxietyQuestions,
  depressionQuestions,
  happinessQuestions,
  relationshipsQuestions,
} from './helpers/moodQuestions';
import useAppSelector from './hooks/useAppSelector';
import MoodTests from './pages/MoodTests/MoodTests';
import Registration from './pages/Registration/Registration';
import Login from './pages/Login/Login';

import './App.css';
import selectIsAuth from './store/user/selectors/selectIsAuth';

function App() {
  const isAuth = useAppSelector(selectIsAuth);

  return (
    <div className="App">
      <Layout>
        <Header />
        <Routes>
          <Route path="/registration" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          {!isAuth && (
            <Route path="*" element={<Navigate to="/login" replace />} />
          )}
          {isAuth && (
            <>
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
                    character={
                      <FireOutlined style={{transform: 'scaleX(-1)'}} />
                    }
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

        <Footer />
      </Layout>
    </div>
  );
}

export default App;
