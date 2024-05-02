import React from "react";
import Image from "next/image";
import { Timer } from "lucide-react";
import Map from "@/components/Map";

export default function EventCommonDetails() {
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
				<p>2 hours</p>
			</div>
			<div className="text-left w-11/12 mt-7">
				<p className="text-sm">
					Hey there! Are you ready for an exciting event happening in Tanger,
					المغرب? Join us for a fun-filled day at 835780030016989217748266! This
					in-person event will bring together people from all walks of life to
					enjoy a day of activities, networking, and more. Whether you're a
					local or just visiting, this event is a great opportunity to connect
					with others and have a fantastic time. Don't miss out on this unique
					experience in Tanger, المغرب!
				</p>
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
				<p className="text-sm">Contact the organizer to request a refund.</p>
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
			<div className="text-left">
				<p className="text-sm">Contact the organizer to request a refund.</p>
			</div>
			<div className="flex flex-col w-40 mb-4 mt-5">
				<h1 className="text-lg font-roboto font-bold w-auto">Location</h1>
				<Image
					src={"/twolines.svg"}
					alt="two"
					width={0}
					height={0}
					className="w-fit"
				></Image>
			</div>
			<div className="max-w-11/12">
				<Map></Map>
			</div>
		</div>
	);
}
