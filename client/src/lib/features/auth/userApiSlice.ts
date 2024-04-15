import { apiSlice } from './apiSlice';


export const userApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation({
      query: (data) => ({
        url: '/Login',
        method: 'POST',
        body: data,
      }),
    }),
    logout: builder.mutation({
      query: (data) => ({
        url: '/Logout',
        method: 'POST',
      
      }),
    })
  }),

});

export const {
  useLoginMutation,
  useLogoutMutation
} = userApiSlice;