"use client";

import React, { ReactNode } from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { useState } from "react";
import { Provider } from "react-redux";
import { store, persistor } from "./store";
import { PersistGate } from "redux-persist/integration/react";

function Providers({ children }: { children: ReactNode }) {
  const [queryClinet] = useState(() => new QueryClient());

  return (
    <Provider store={store}>
      <PersistGate loading={<div>{children}</div>} persistor={persistor}>
        <QueryClientProvider client={queryClinet}>
          <ReactQueryDevtools initialIsOpen={false} />
          {children}
        </QueryClientProvider>
      </PersistGate>
    </Provider>
  );
}

export default Providers;
