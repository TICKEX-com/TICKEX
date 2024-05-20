import React from "react";
import Image from "next/image";
import { Timer } from "lucide-react";
import Map from "@/components/Map";
import { eventInfoType, organizerType } from "@/core/types/event";
import Link from "next/link";
import { AvatarDemo } from "./ui/avatar";
import EventsGrid from "./EventsGrid";
import { useQuery } from "@tanstack/react-query";
import axios, { AxiosResponse } from "axios";

export default function EventCommonDetails({
	id,
	eventType,
	description,
	duration,
	organizer,
}: {
	id:number |undefined;
	eventType:string | undefined;
	description: string | undefined;
	duration: number | undefined;
	organizer: organizerType | undefined;
}) {
	const fetchSimilarEvents = async() => {
		let response: AxiosResponse;
		try {
			response = await axios.get(`/api/event-service/Events/Filter?EventType=${eventType}&MinPrice=0&MaxPrice=0&pageNumber=1`);
			return response.data;
		} catch (error) {
			return null;
		}
	};
	const { data, isLoading, error, refetch } = useQuery({
		queryKey: ["similar_events"],
		queryFn: () => fetchSimilarEvents(),
	});
	return (
		<div className="flex flex-col">
			<div className="flex flex-col  w-32  mb-7 mt-5">
				<h1 className="text-lg font-roboto font-bold w-auto">About Event</h1>
				<Image
					src={"/twolines.svg"}
					alt="two"
					width={0}
					height={0}
					className="w-fit"
				></Image>
			</div>
			<div className="flex flex-row items-center space-x-3">
				<div className="border border-transparent bg-black p-1 rounded-md">
					<Timer size={20} color="white"></Timer>
				</div>
				<p>{duration} hours</p>
			</div>
			<div className="text-left w-11/12 mt-7">
				<p className="text-sm">{description}</p>
			</div>
			<div className="flex flex-col w-40  mb-4 mt-5">
				<h1 className="text-lg font-roboto font-bold w-auto">Refund Policy</h1>
				<Image
					src={"/twolines.svg"}
					alt="two"
					width={0}
					height={0}
					className="w-fit"
				></Image>
			</div>
			<div className="text-left">
				<p className="text-sm">
					Contact the organizer to request a refund. via{" "}
					<Link href={""} className="text-primary border-0 text-sm">
						{organizer?.email}
					</Link>
				</p>
			</div>
			<div className="flex flex-col w-40 mb-4 mt-5">
				<h1 className="text-lg font-roboto font-bold w-auto">Organized by</h1>
				<Image
					src={"/twolines.svg"}
					alt="two"
					width={0}
					height={0}
					className="w-fit"
				></Image>
			</div>
			<div className="text-left border-1 rounded-md flex flex-row w-fit items-center space-x-2 px-6 p-3">
				<AvatarDemo></AvatarDemo>
				<div className="flex flex-col">
					<h1>{organizer?.organizationName}</h1>
					<p className="text-[11px]">{organizer?.email}</p>
					<p className="text-[11px]">{organizer?.phoneNumber}</p>
				</div>
			</div>
			<div className="flex flex-col w-40 mb-4 mt-5">
				<h1 className="text-lg font-roboto font-bold w-auto">Similar Events</h1>
				<Image
					src={"/twolines.svg"}
					alt="two"
					width={0}
					height={0}
					className="w-fit"
				></Image>
			</div>
			<div className="max-w-11/12">
				<EventsGrid events={data?.filter((event:eventInfoType)=>event.id!=id)}></EventsGrid>
			</div>
		</div>
	);
}
