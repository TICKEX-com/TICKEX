import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';


const baseQuery = fetchBaseQuery(
    { 
    baseUrl:'http://localhost:8080/AUTHENTICATION-SERVICE/api/auth',
});

export const apiSlice = createApi({
  baseQuery,
  tagTypes: ['User'],
  endpoints: (builder) => ({}),
});