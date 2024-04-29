export type CardProp ={
    icon: string ;
    title:string ;
    number :string;
}

export type chartData = {
    date?: string;
    value: number;
    type?: 'Cinema' | 'Sport' | 'Culture' | 'Music';
  };
  
  export type groupColumnProp = {
    title: string;
    data: chartData[] | dailyData[];
    rate?: number;
  };
  export type dailyData = {
    type: 'Cinema' | 'Sport' | 'Culture' | 'Music';
    value: number;
  };
  
  