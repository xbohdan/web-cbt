import {RootState} from '../../store';

const selectGender = (state: RootState): string => state.user.gender;

export default selectGender;
