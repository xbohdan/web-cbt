import React from 'react';
import {Link} from 'react-router-dom';
import {Typography} from 'antd';

import './Header.css';

const {Title} = Typography;

const Header = () => {
  return (
    <header className="header">
      <Link to="/">
        <Title level={3}>Some cool header</Title>
      </Link>
    </header>
  );
};

export default Header;
