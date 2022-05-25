import {RootState} from '../../store';

const selectBanned = (state: RootState): boolean => state.user.banned;

export default selectBanned;
