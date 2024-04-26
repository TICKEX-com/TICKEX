import React from "react";
import Image from "next/image";
import { CardProp } from "@/core/types/dashboard";
function ContentCard({ icon, title, number }: CardProp) {
  return (
    <div className="bg-white rounded-3xl p-4 flex justify-around shadow-xlitems-center">
      <div className="bg-brand-50 p-3 rounded-3xl">
        <Image width={40} height={28} alt="icon..." src={icon} loading="lazy" />
      </div>
      <div>
        <span className="text-gray-400 text-sm">{title}</span>
        <h2 className="font-bold text-2xl">{number}</h2>
      </div>
    </div>
  );
}

export default ContentCard;
