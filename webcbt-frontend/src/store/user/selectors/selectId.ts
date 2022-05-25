import {RootState} from '../../store';

const selectId = (state: RootState): number => state.user.userId;

export default selectId;
