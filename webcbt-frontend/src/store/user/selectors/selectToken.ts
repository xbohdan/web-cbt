import {RootState} from '../../store';

const selectToken = (state: RootState): string | undefined | null =>
  state.user.accessToken;

export default selectToken;
