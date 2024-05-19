import React from "react";
import {Slider} from "@nextui-org/react";

export default function ProgressSlider({handleValueChange}:{handleValueChange:(value:  number[])=>void}) {
  const handleChange = (value:  number[]) => {
    // Pass the current value to the handleValueChange function
    handleValueChange(value);
  };
  return (
    <Slider 
      label="Price"
      step={50} 
      color="purple"
      minValue={0} 
      onChange={handleChange} // Use handleChange instead of an anonymous function
      maxValue={4000} 
      defaultValue={[100, 1000]} 
      formatOptions={{style: "currency", currency: "MAD"}}
      className="max-w-md "
    />
  );
}
