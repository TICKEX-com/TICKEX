"use client";

import react, { useState } from "react";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
interface SelectBoxProps {
  items: string[];
  element: string;
  title: string;
  setDataValue: React.Dispatch<React.SetStateAction<{
  
}>>,
  value: string;
}

function SelectBox({ items, element, title, value, setDataValue }: SelectBoxProps) {
  
    const handleSelectChange = (selectedValue: string) => {
        setDataValue((prevData) => ({
          ...prevData,
          [element]: selectedValue,
        }));
      };

  return (
    <Select value={value} onValueChange={handleSelectChange}>
      <SelectTrigger className="w-[180px]">
        <SelectValue placeholder={title} />
      </SelectTrigger>
      <SelectContent>
        <SelectGroup>
          <SelectLabel>{element}</SelectLabel>
          {items.map((item, index) => (
            <SelectItem key={index} value={item}>
              {item}
            </SelectItem>
          ))}
        </SelectGroup>
      </SelectContent>
    </Select>
  );
}

export default SelectBox;
