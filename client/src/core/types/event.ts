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
}
export type eventInfoType ={  
  title: string;
  desc: string;
  address: string;
  city: string;
  date: string;
  time: string;
  type: string;
  categories: Category[];
  image: string;
};

export type EventState = {
  eventInfo: eventInfoType
}