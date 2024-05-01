import { categories } from "@/core/constantes/Event.const";
import { EventState } from "@/core/types/event";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: EventState = {
  eventInfo: {
    title: "",
    desc: "",
    address: "",
    city: "",
    date: "",
    time: "",
    type: "",
    categories: [],
    image: "",
  },
};

const eventSlice = createSlice({
  name: "event",
  initialState,
  reducers: {
    setType: (state, action: PayloadAction<string>) => {
      state.eventInfo.type = action.payload;
    },
    setCategoriesInfo: (state, action: PayloadAction<string[]>) => {
      state.eventInfo.categories.push(...action.payload.map(name => ({ name, stock: 100, price: 99.99 })));
    },
    setImage: (state, action: PayloadAction<string>) => {
      state.eventInfo.image = action.payload;
    },
    setEventInfo: (state, action: PayloadAction<Partial<EventState["eventInfo"]>>) => {
      state.eventInfo = { ...state.eventInfo, ...action.payload };
    },
    clearEventInfo: (state) => {
      state.eventInfo = initialState.eventInfo;
    },
  },
});

export const {
  setEventInfo,
  setType,
  setCategoriesInfo,
  setImage,
  clearEventInfo,
} = eventSlice.actions;

export default eventSlice.reducer;