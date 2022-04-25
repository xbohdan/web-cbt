import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {User} from '../../types/User';

// Initial state of User slice
// Get data from localStorage or initialize with empty values
export const initialState: User = {
  username: localStorage.getItem('USERNAME') || '',
  accessToken: localStorage.getItem('TOKEN') || undefined,
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User>) => {
      state.username = action.payload.username;
      state.accessToken = action.payload.accessToken;
    },
  },
});

export const {setUser} = userSlice.actions;

export default userSlice.reducer;
