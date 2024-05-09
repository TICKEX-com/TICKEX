"use client"


import DataTable from "@/components/DataTable";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { eventData } from "@/core/constantes/Table.const";
import { ListFilter, PlusCircle, File } from "lucide-react";
import Link from "next/link";
import Image from "next/image";
import React from "react";
import { useAppSelector } from "@/lib/hooks";
import api from "@/lib/api";
import { useQuery } from "@tanstack/react-query";

function page() {


  const oganiserId = useAppSelector(state =>state.persistedReducer.auth.userInfo?.id);
  const id = oganiserId;
  const fetchEvents = async ()=> {
    try {
      const res = await api.get(`event-service/Organizer/${id}/Events`)
      console.log(res.data);
      return res?.data
      
    } catch (error) {
      throw error;
    }
    
  }

  const{data,error,isLoading}=useQuery({queryKey:["events"],queryFn: fetchEvents});

  
  const eventsInfo = data;
  
  const dataLength = eventsInfo?.length;
  return (
    <div className="w-full">
      <div className="flex items-center">
        <div className="ml-auto flex items-center gap-2">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline" size="sm" className="h-8 gap-1">
                <ListFilter className="h-3.5 w-3.5" />
                <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
                  Filter
                </span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Filter by</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuCheckboxItem checked>
                Active
              </DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem>Draft</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem>Archived</DropdownMenuCheckboxItem>
            </DropdownMenuContent>
          </DropdownMenu>
          <Button size="sm" variant="outline" className="h-8 gap-1">
            <File className="h-3.5 w-3.5" />
            <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
              Export
            </span>
          </Button>
          <Link
            href="./events/addevent"
            className="sr-only sm:not-sr-only sm:whitespace-nowrap"
          >
            <Button size="sm" className="h-8 gap-1">
              <PlusCircle className="h-3.5 w-3.5" />
              Add event
            </Button>
          </Link>
        </div>
      </div>
      {dataLength === 0 ? (
        <div className="flex justify-normal items-center flex-col mt-10">
          <div>
            <Image src="/svg/Empty-amico.svg" alt="plus" width={400} height={400} />
          </div>
          <div className="grid gap-4 mt-3 mb-3">
            <h2 className="text-xl font-bold">You haven't added any events yet</h2>
            <div className="flex justify-center">
            <Link
            href="./events/addevent"
            className="sr-only sm:not-sr-only sm:whitespace-nowrap"
          >
            <Button size="sm" className="h-8 gap-1 ">
              <PlusCircle className="h-3.5 w-3.5" />
              Add event
            </Button>
            </Link>
            </div>
          </div>
        </div>
      ) : (
        <>
         {eventsInfo &&   <DataTable data={eventsInfo} />}
        </>
       
      
      )}
    </div>
  );
}

export default page;
