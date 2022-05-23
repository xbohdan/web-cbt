import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import {FormInstance} from 'antd';
import {toast} from 'react-toastify';

const handleLoginErrors = (err: FetchBaseQueryError, form: FormInstance) => {
  if (err.status === 401) {
    form.setFields([
      {
        name: 'password',
        errors: ['Invalid username/password supplied'],
      },
    ]);
  } else if (err.status === 403) {
    toast.error('Access forbidden');
  } else if (err.status === 404) {
    toast.error('404: Not found');
  } else if (err.status === 503) {
    toast.error('Server is currently down');
  } else if ('error' in err) {
    toast.error(err.error);
  } else {
    toast.error('Unknown error. Please try again later');
  }
};

export default handleLoginErrors;
