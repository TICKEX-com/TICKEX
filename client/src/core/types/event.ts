type event_card = {
	city: String|null;
	date: Date;
	eventType: String|null;
	id: Number;
	poster: String|null;
	title: String|null;
};

type events_list = Array<event_card>


export type eventType = {
  title: string;
  desc: string;
  address: string;
  city: string;
  currency: string;
  date: string;
  time: string;
  type: string;
  categories: string[];
  image: string;
};

export type Category = {
  name: string;
  stock: number;
  price: number;
};
export type eventInfoType = {
  title: string;
  description: string;
  address: string;
  duration: number;
  city: string;
  eventDate: string;
  time: string;
  eventType: string;
  categories: {
    id?: string;
    name: string;
    seats: number;
    price: number;
    color?: string;
  }[];
  poster: string;
};

export type EventState = {
  eventInfo: eventInfoType;
};
