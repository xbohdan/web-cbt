import {
  LogoutOutlined,
  ReconciliationOutlined,
  SettingOutlined,
  UserOutlined,
} from '@ant-design/icons';
import React, {useState} from 'react';
import {Link, useNavigate} from 'react-router-dom';
import {Popover, Typography, Button, Avatar} from 'antd';

import './Header.css';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import selectIsAuth from '../../store/user/selectors/selectIsAuth';
import selectLogin from '../../store/user/selectors/selectLogin';
import {logout} from '../../store/user/slice';

const {Title} = Typography;

const Header = () => {
  const isAuth = useAppSelector(selectIsAuth);
  const login = useAppSelector(selectLogin);
  const [isVisible, setIsVisible] = useState(false);

  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const logoutUser = () => {
    dispatch(logout());
    setIsVisible(false);
    navigate('/');
  };

  return (
    <header className="header">
      <Link to="/">
        <Title level={3}>Web CBT</Title>
      </Link>
      {isAuth && (
        <div className="headerActions">
          <Link className="linkToTests" to="/tests">
            <ReconciliationOutlined />{' '}
            <span className="moodTestsParagraph">Mood tests</span>
          </Link>

          <Popover
            className="headerPopover"
            title={login}
            visible={isVisible}
            trigger="click"
            onVisibleChange={(isVisible) => setIsVisible(isVisible)}
            content={
              <div className="popoverContent">
                <Link to="/settings">
                  <SettingOutlined /> &nbsp;Manage account
                </Link>
                <Button
                  className="popoverLogout"
                  danger
                  type="link"
                  onClick={logoutUser}
                >
                  <LogoutOutlined /> Log out
                </Button>
              </div>
            }
            placement="bottomRight"
          >
            <Avatar
              className="headerAvatar"
              icon={<UserOutlined className="avatarIcon" />}
            />
            <Button className="headerButton">
              <UserOutlined />
              Profile
            </Button>
          </Popover>
        </div>
      )}
    </header>
  );
};

export default Header;
