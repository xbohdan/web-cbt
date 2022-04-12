import React from 'react';

import './Layout.css';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout = ({children}: LayoutProps) => {
  return <div className="grid">{children}</div>;
};

export default Layout;
