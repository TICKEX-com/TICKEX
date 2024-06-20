import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';


const baseQuery = fetchBaseQuery(
    { 
    baseUrl:'/api/authentication-service/api/auth',
});

export const apiSlice = createApi({
  baseQuery,
  tagTypes: ['User'],
  endpoints: (builder) => ({}),
});