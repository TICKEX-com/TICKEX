import { configureStore } from "@reduxjs/toolkit";
import authReducer from './features/auth/authSlice';
import eventReducer from "./features/events/eventSlice";
import {apiSlice} from './features/auth/apiSlice';

export const store = configureStore({
    reducer:{
        auth: authReducer,
        [apiSlice.reducerPath]:apiSlice.reducer,
        event: eventReducer,
    },
    middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),
    devTools :true
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch
