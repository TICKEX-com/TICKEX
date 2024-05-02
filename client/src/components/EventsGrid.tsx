import React from "react";
import EventCard from "./EventCards";
import Paginate from "./Paginate";
import Link from "next/link";

type Props={
    events ?: events_list
}

export default function EventsGrid({events}:Props) {
	return (
		<div
			className="justify-center container  mx-auto 
flex flex-wrap gap-4 h-fit items-center  scroll-smooth pb-20"
		>
            {/* {events?.map((event)=><Link href={`details/categories/${event.id}`}><EventCard key={event.id} event={event}></EventCard></Link>)} */}
            {Array.from([1,2,3,4,5,6]).map((event)=><Link className="border-0" href={`details/categories/${event}`}><EventCard ></EventCard></Link>)}
		</div>
	);
}
