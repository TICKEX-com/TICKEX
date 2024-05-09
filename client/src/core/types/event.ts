export type event_card = {
	city: String|null;
	eventDate: Date|null;
	eventType: String|null;
	id: Number|null;
	poster: String|null;
	title: String|null;
  minPrice:number|null;
};

export type events_list = Array<event_card>


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
  id:number
  title: string;
  description: string;
  address: string;
  duration: number;
  city: string;
  eventDate: Date | null | undefined;
  time: string;
  eventType: string;
  designId:string ;
  categories: {
    id?: string;
    name: string;
    seats: number;
    price: number;
    color?: string;
  }[];
  poster: string;
  organizer:organizerType
};

export type EventState = {
  eventInfo: eventInfoType;
};

export type organizerType= {
  id: string,
  email: string ,
  organizationName: string ,
  phoneNumber: string
}