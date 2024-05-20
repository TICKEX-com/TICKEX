import { categories } from "@/core/constantes/Event.const";
import { EventState } from "@/core/types/event";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { v4 as uuidv4 } from "uuid";


const initialState: EventState = {
  eventInfo: {
    title: "",
    description: "",
    address: "",
    duration: 0,
    city: "",
    eventDate: "",
    time: "",
    eventType: "",
    categories: [],
    poster: "",
  },
};

const eventSlice = createSlice({
  name: "event",
  initialState,
  reducers: {
    setType: (state, action: PayloadAction<string>) => {
      state.eventInfo.eventType = action.payload;
    },
    setCategoriesInfo: (state, action: PayloadAction<string[]>) => {
      const newCategories = action.payload.map((name) => ({
        id: uuidv4(),
        name,
        seats: 0,
        price: 99.99
      }));
      state.eventInfo.categories.push(...newCategories);
    },
    setImage: (state, action: PayloadAction<string>) => {
      state.eventInfo.poster = action.payload;
    },
    setEventInfo: (
      state,
      action: PayloadAction<Partial<EventState["eventInfo"]>>
    ) => {
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
