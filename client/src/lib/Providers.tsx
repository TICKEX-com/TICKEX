'use client'

import React, { ReactNode } from 'react'
import {
    QueryClient,
    QueryClientProvider,
  } from '@tanstack/react-query'
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { useState } from 'react'
import { Provider } from 'react-redux'
import { store } from './store'



function Providers({children}:{children:ReactNode}) {
    const [queryClinet] = useState(()=>new QueryClient)

  return (
    <Provider store={store}>
    <QueryClientProvider client={queryClinet}>
        <ReactQueryDevtools initialIsOpen={false}/>
      {children}
    </QueryClientProvider>
    </Provider>
  )
}

export default Providers
