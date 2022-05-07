import {RootState} from '../../store';

const selectIsAuth = (state: RootState): boolean =>
  Boolean(state.user.accessToken);

export default selectIsAuth;
