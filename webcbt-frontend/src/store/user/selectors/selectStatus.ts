import {RootState} from '../../store';

const selectStatus = (state: RootState): number => state.user.userStatus;

export default selectStatus;
