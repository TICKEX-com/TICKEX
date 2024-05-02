import React from "react";
import {Slider} from "@nextui-org/react";

export default function ProgressSlider() {
  return (
    <Slider 
      label="Price"
      step={50} 
      color="purple"
      minValue={0} 
      maxValue={4000} 
      defaultValue={[100, 1000]} 
      formatOptions={{style: "currency", currency: "MAD"}}
      className="max-w-md "
    />
  );
}
