"use client";
import { groupColumnProp } from "@/core/types/dashboard";
import React from "react";
import { Column } from "@ant-design/plots";

function ColumnChartCard({ title, data }: groupColumnProp) {
  const config = {
    data,
    isStack: true,
    xField: "date",
    height: 250,
    width: 550,
    lineWidth: 1,
    yField: "value",
    color: "#8444ec",
    legend: false,
    axis: {
      y: {
        labelFormatter: ".0%",
      },
    },

    yAxis: {
      grid: { line: { style: { lineWidth: 0 } } },
    },
  } as any;
  return (
    <div className="bg-white rounded-3xl shadow-xl p-5 px-6">
      <div className="flex justify-between items-center">
        <h3 className="font-semibold text-xl">{title}</h3>
      </div>
      <div className="mt-8">
        <Column {...config} />
      </div>
    </div>
  );
}

export default ColumnChartCard;
