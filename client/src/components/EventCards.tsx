import React, { useEffect, useRef } from "react";
import Image from "next/image";

import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "@/components/ui/card";
import { DropdownMenu } from "@radix-ui/react-dropdown-menu";
import { Button } from "./ui/button";
import { Badge } from "./ui/badge";
import { formatDate } from "@/helpers/formatters";
import { event_card } from "@/core/types/event";

type Props= {
	event?:event_card
}

export default function EventCard({event}:Props) {
	return (
		<Card className="max-w-xl min-w-xl font-roboto transition-all ease-in-out duration-300  border-2 border-transparent pointer hover:border-gray-200 hover:scale-95 m-2">
			<CardHeader className="p-0">
				<Image
					src={event?.poster}
					alt="two"
					width={100}
					height={70}
					className="w-full"
				></Image>
			</CardHeader>
			<CardContent className=" flex flex-col ">
				<div className="tags flex flex-row max-w-xs overflow-hidden"><Badge className="bg-[#2ab3e9] rounded-md px-2 mr-2" >{event?.eventType}</Badge><Badge className="bg-[#26C281] rounded-md px-2 mr-2" >Just Added</Badge></div>
				<div className="name text-md max-w-xs font-bold">{event?.title}</div>
				<div className="mt-2 date text-[10px]  text-gray-600">{formatDate(event?.eventDate)}</div>
				<div className="mt-2 location text-xs ">{event?.city}</div>
				<div className="price-range text-xs font-bold mt-6 mb-2">From {event?.minPrice}MAD</div>
			</CardContent>
		</Card>
	);
}
