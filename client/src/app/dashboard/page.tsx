import ColumnChartCard from "@/components/ColumnChartCard";
import ContentCard from "@/components/ContentCard";
import LineChartCard from "@/components/LineChartCard";
import { salesData, weeklyData } from "@/core/constantes/Event.const";
import React from "react";

function page() {
  return (
    <div className="p-2 py-6 w-full max-w-full">
      <div className="grid grid-cols-4 gap-6">
        <ContentCard
          icon="/svg/customers.svg"
          title="Total Sales"
          number="15342"
        />
        <ContentCard
          icon="/svg/dollar.svg"
          title="Total Income"
          number="$ 56180"
        />
        <ContentCard
          icon="/svg/stats.svg"
          title="Total ...."
          number="2882182"
        />
        <ContentCard icon="/svg/ticket.svg" title="Total Events" number="35" />
      </div>
      <div className="mt-5 grid grid-cols-2 gap-4">
        <LineChartCard title="Sales Per Category" data={weeklyData} />
        <ColumnChartCard title="Sales Per Day" data={salesData} />
      </div>
    </div>
  );
}

export default page;
