"use client"
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
    CommandSeparator,
    CommandShortcut,
  } from "@/components/ui/searchitems"
import { CommandDialog } from "cmdk"
import Link from "next/link"
import { useEffect, useState } from "react"
  
export default function SearchBar({data}:{data:any}) {

  return (
    <Command>
      <CommandInput placeholder="Type a command or search..." />
      <CommandList>
        <CommandEmpty>No results found.</CommandEmpty>
        <CommandGroup heading="Suggestions">
          {data?.map((event:any)=><CommandItem><Link href={`/details/categories/${event.id}`}>{event.title}</Link></CommandItem>)}
        </CommandGroup>
      </CommandList>
    </Command>
  )
}

  