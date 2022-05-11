import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {toast} from 'react-toastify';
import {User} from '../../types/User';

// Initial state of User slice
// Get data from localStorage or initialize with empty values
export const initialState: User = {
  login: localStorage.getItem('LOGIN') || '',
  accessToken: localStorage.getItem('TOKEN') || undefined,
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User>) => {
      state.login = action.payload.login;
      state.accessToken = action.payload.accessToken;
    },
    logout: (state) => {
      state.login = '';
      state.accessToken = undefined;
      localStorage.removeItem('LOGIN');
      localStorage.removeItem('TOKEN');
      toast.warn('Logged out');
    },
  },
});

export const {setUser, logout} = userSlice.actions;

export default userSlice.reducer;
