import {configureStore} from '@reduxjs/toolkit';
import {authApi} from './services/auth';
import userReducer from './user/slice';

export const store = configureStore({
  reducer: {
    [authApi.reducerPath]: authApi.reducer,
    user: userReducer,
  },
  // Add RTK-query middleware to enable additional automatic features, such as caching.
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authApi.middleware),
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
