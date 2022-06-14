import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {toast} from 'react-toastify';
import {User} from '../../types/User';

// Initial state of User slice
// Get data from localStorage or initialize with empty values
export const initialState: User = {
  login: localStorage.getItem('LOGIN') || '',
  accessToken: localStorage.getItem('TOKEN') || undefined,
  age: localStorage.getItem('AGE') || undefined,
  gender: localStorage.getItem('GENDER') || 'would rather not say',
  userStatus: Number(localStorage.getItem('STATUS')),
  banned: Boolean(localStorage.getItem('BANNED')),
  userId: Number(localStorage.getItem('ID')),
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User>) => {
      state.login = action.payload.login;
      state.accessToken = action.payload.accessToken;
      state.age = action.payload.age;
      state.gender = action.payload.gender;
      state.banned = action.payload.banned;
      state.userStatus = action.payload.userStatus;
      state.userId = action.payload.userId;
    },
    logout: (state) => {
      state.login = '';
      state.accessToken = undefined;
      localStorage.removeItem('LOGIN');
      localStorage.removeItem('TOKEN');
      localStorage.removeItem('GENDER');
      localStorage.removeItem('AGE');
      localStorage.removeItem('STATUS');
      localStorage.removeItem('ID');
      localStorage.removeItem('BANNED');
      toast.warn('Logged out');
    },
  },
});

export const {setUser, logout} = userSlice.actions;

export default userSlice.reducer;
