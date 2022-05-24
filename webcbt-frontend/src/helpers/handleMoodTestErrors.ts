import {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import {toast} from 'react-toastify';

const handleMoodTestErrors = (err: FetchBaseQueryError) => {
  if (err.status === 400) {
    toast.error('Invalid input');
  } else if (err.status === 403) {
    toast.error('Access forbidden');
  } else if (err.status === 404) {
    toast.error('404: Not found');
  } else if (err.status === 500) {
    toast.error('failure');
  } else if (err.status === 503) {
    toast.error('Server is currently down');
  } else if ('error' in err) {
    toast.error(err.error);
  } else {
    toast.error('Unknown error. Please try again later');
  }
};

export default handleMoodTestErrors;
