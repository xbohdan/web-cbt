import {RootState} from '../../store';

const selectLogin = (state: RootState): string => state.user.login;

export default selectLogin;
