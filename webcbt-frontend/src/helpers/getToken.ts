// First attempt to get token from Redux store, otherwise get token from the
// localStorage. Reading from Redux store is faster.
import {RootState} from '../store/store';
import selectToken from '../store/user/selectors/selectToken';

const getToken = (store: RootState): string | null => {
  let token = selectToken(store);
  if (!token) token = localStorage.getItem('TOKEN');
  return token;
};

export default getToken;
