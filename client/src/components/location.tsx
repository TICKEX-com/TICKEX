import * as React from "react"

import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"

export default function Location() {
  return (
    <Select>
      <SelectTrigger className="w-[180px] h-fit border-0 p-0 outline-none">
        <SelectValue placeholder="Select a location" className="text-gray-600"/>
      </SelectTrigger>
      <SelectContent>
        <SelectGroup>
          <SelectItem value="apple">Rabat</SelectItem>
          <SelectItem value="banana">Casablance</SelectItem>
          <SelectItem value="blueberry">Marrakech</SelectItem>
          <SelectItem value="grapes">Agadir</SelectItem>
          <SelectItem value="pineapple">Houceima</SelectItem>
        </SelectGroup>
      </SelectContent>
    </Select>
  )
}
