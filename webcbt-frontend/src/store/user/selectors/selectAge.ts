import {RootState} from '../../store';

const selectAge = (state: RootState): string | number | undefined => state.user.age;

export default selectAge;
