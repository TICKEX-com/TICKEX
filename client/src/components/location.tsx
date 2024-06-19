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
import Link from "next/link"

export default function Location() {
  return (
    <Select>
      <SelectTrigger className="w-[180px] h-fit border-0 p-0 outline-none">
        <SelectValue placeholder="Select a location" className="text-gray-600"/>
      </SelectTrigger>
      <SelectContent>
        <SelectGroup className="w-[200px]" >
         <Link className="w-full rounded-sm py-1.5 pl-8 pr-2 text-sm outline-none focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50 hover:bg-slate-200" href={`/filter?city=Rabat`}>Rabat</Link>
         <Link className="w-full rounded-sm py-1.5 pl-8 pr-2 text-sm outline-none focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50 hover:bg-slate-200" href={`/filter?city=Casablanca`}>Casablanca</Link>
         <Link className="w-full rounded-sm py-1.5 pl-8 pr-2 text-sm outline-none focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50 hover:bg-slate-200" href={`/filter?city=Marrakech`}>Marrakech</Link>
         <Link className="w-full rounded-sm py-1.5 pl-8 pr-2 text-sm outline-none focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50 hover:bg-slate-200" href={`/filter?city=Agadir`}>Agadir</Link>
         <Link className="w-full rounded-sm py-1.5 pl-8 pr-2 text-sm outline-none focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50 hover:bg-slate-200" href={`/filter?city=Houceima`}>Houceima</Link>
        </SelectGroup>
      </SelectContent>
    </Select>
  )
}
["Rabat","Casablanca","Marrakech","Agadir","Houceima"]