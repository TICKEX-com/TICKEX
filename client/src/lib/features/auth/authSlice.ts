import { createSlice } from '@reduxjs/toolkit';

const storedUserInfo =localStorage.getItem('userInfo')
const parsedUserInfo = storedUserInfo ? JSON.parse(storedUserInfo): null;

const initialState = {
  userInfo: parsedUserInfo
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setCredentials: (state, action) => {
      state.userInfo = action.payload;
      localStorage.setItem('userInfo', JSON.stringify(action.payload));
    },
    logout: (state, action) => {
      state.userInfo = null;
      localStorage.removeItem('userInfo');
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;

export default authSlice.reducer;