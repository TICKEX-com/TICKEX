export type eventDataType = {
  desc: string | number | readonly string[] | undefined;
  id: string;
  name: string;
  category: "Cinema" | "Sport" | "Music" | "Culture";
  price: number;
  totalSales: number;
  createdAt: string;
};
