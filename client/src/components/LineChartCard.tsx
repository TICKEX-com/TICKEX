"use client";
import React from "react";
import Image from "next/image";
import { Line } from "@ant-design/plots";
import { groupColumnProp } from "@/core/types/dashboard";

function LineChartCard({ title, data }: groupColumnProp) {
  const config = {
    data,
    height: 250,
    xField: "date",
    yField: "value",
    seriesField: "type",
    colorField: "type",
    color: ["#0F1CBA", "#BEB0FF", "#80D8FF", ""],
    legend: {
      position: "top",
    },
    smooth: true,
    animation: {
      appear: {
        animation: "path-in",
        duration: 4000,
      },
    },
    yAxis: {
      grid: { line: { style: { lineWidth: 0 } } },
    },
  } as any;
  return (
    <div className="bg-white rounded-3xl shadow-xl p-5">
      <div className="flex justify-between items-center">
        <div className="flex justify-between gap-4 items-center">
          <h3 className="font-semibold text-lg">{title}</h3>
        </div>
      </div>
      <div className="mt-8">
        <Line {...config} />
      </div>
    </div>
  );
}

export default LineChartCard;
