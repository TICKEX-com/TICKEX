import React from "react";
import EventCard from "./EventCards";
import Paginate from "./Paginate";
import Link from "next/link";
import { events_list } from "@/core/types/event";

type Props={
    events ?: events_list
}

export default function EventsGrid({events}:Props) {
	return (
		<div
			className="justify-center container  mx-auto 
flex flex-wrap gap-4 h-fit items-center  scroll-smooth pb-20"
		>
            {events?.map((event,indx)=><Link href={`/details/categories/${event.id}`} className="border-0"><EventCard key={indx} event={event}></EventCard></Link>)}
		</div>
	);
}
