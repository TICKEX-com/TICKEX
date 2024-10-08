"use client";

import React, { useEffect, useState } from "react";
import Image from "next/image";
import { ChevronLeft, Upload } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import EventDetailsCard from "@/components/EventDetailsCard";
import EventCat from "@/components/EventCat";
import TicketCat from "@/components/TicketCat";
import Link from "next/link";
import { uploadFile } from "@/lib/fileUpload";
import { useDispatch } from "react-redux";
import { setEventInfo, setImage} from "@/lib/features/events/eventSlice";
import { useAppSelector } from "@/lib/hooks";
import { useQuery } from "@tanstack/react-query";
import api from "@/lib/api";
import { eventInfoType } from "@/core/types/event";

function page({ params }: { params: { modifyEvent: string } }) {

  const dispatch = useDispatch();
  const eventId = params;

  const [imageUpload, setImageUpload] = useState<FileList | null>(null);
  const [poster, setPoster] = useState<string>("");
  const oganiserId = useAppSelector(state =>state.persistedReducer.auth.userInfo?.id);
  const id = oganiserId;
  const fetchEvent = async ()=> {
    try {
      const res = await api.get(`event-service/Organizer/${id}/Events/${eventId.modifyEvent}`)
      console.log(res.data);
      console.log(eventId);
      
    
      return res?.data
      
    } catch (error) {
      throw error;
    }
    
  }

  const{data,error,isLoading}=useQuery({queryKey:["event"],queryFn: fetchEvent});
  
  const eventData : eventInfoType={
    title:data?.title,
    description:data?.description,
    city: data?.city,
    duration:data?.duration,
    address: data?.address,
    eventDate: data?.eventDate,
    time: data?.time,
    eventType: data?.eventType,
    poster: data?.poster,
    categories: data?.categories
  }
  
  useEffect(()=>{
    dispatch(setEventInfo(eventData));
  },[eventData])
  useEffect(()=>{
    const payload: Partial<string> =poster
    dispatch(setImage(payload))
  },[dispatch,poster])

    
  const eventPoster = useAppSelector(state=>state.event.eventInfo.poster);
  return (
    <main className="grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8">
      <div className="mx-auto grid max-w-[59rem] flex-1 auto-rows-max gap-4">
        <div className="flex items-center gap-4">
          <Link href="./">
            <Button variant="outline" size="icon" className="h-7 w-7">
              <ChevronLeft className="h-4 w-4" />
              <span className="sr-only">Back</span>
            </Button>
          </Link>
          <h1 className="flex-1 shrink-0 whitespace-nowrap text-xl font-semibold tracking-tight sm:grow-0">
            Modify Event 
          </h1>
          <div className="hidden items-center gap-2 md:ml-auto md:flex">
            <Button size="sm">Save Event</Button>
          </div>
        </div>
        <div className="grid gap-4 md:grid-cols-[1fr_250px] lg:grid-cols-3 lg:gap-8">
          <div className="grid auto-rows-max items-start gap-4 lg:col-span-2 lg:gap-8">
            <EventDetailsCard
              title="Event Details"
              description="Please Add Your Next Event"
             
            />
            <EventCat  />
            <TicketCat  />
          </div>
          <div className="grid auto-rows-max items-start gap-4 lg:gap-8">
            <Card className="overflow-hidden" x-chunk="dashboard-07-chunk-4">
              <CardHeader>
                <CardTitle>Event Image</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="grid gap-2">
                  <Image
                    alt="Product image"
                    className="aspect-square w-full rounded-md object-cover"
                    height="300"
                    src={poster ? poster : eventPoster}
                    width="300"
                  />
                  <input
                    type="file"
                    onChange={(event) => {
                      setImageUpload(event.target.files);
                    }}
                    className="block w-full text-sm text-slate-500 mt-2
                                  file:mr-4 file:py-2 file:px-4
                                  file:rounded-full file:border-0
                                  file:text-sm file:font-semibold
                              file:bg-violet-50 file:text-violet-700
                              hover:file:bg-violet-100"
                  />
                  <span className="sr-only">Upload</span>
                  <button
                    className="bg-purple-600 text-sm rounded-full flex justify-center "
                    onClick={() => uploadFile(imageUpload, setPoster)}
                  >
                    <Image
                      src="/svg/upload.svg"
                      width={30}
                      height={30}
                      alt="upload"
                    />
                  </button>
                </div>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </main>
  );
}

export default page;
