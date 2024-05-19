"use client";

import { NavLinks } from "@/components/NavLinks";
import Navbar1 from "@/components/Navbar1";
import { Calendar, Heart, MapPin, Minus, Plus, Save, Share, Timer } from "lucide-react";
import Image from "next/image";
import React from "react";
import Footer from "@/components/footer";
import { Button } from "@/components/ui/button";
import EventCommonDetails from "@/components/EventCommonDetails";
import { useParams } from "next/navigation";
import { fetchEventsById } from "./_page";
import { useQuery } from "@tanstack/react-query";
import { eventInfoType } from "@/core/types/event";
import { formatDate } from "@/helpers/formatters";
import { Badge } from "@/components/ui/badge";
import { eventData } from "@/core/constantes/Table.const";

export default function page({ params }: { params: { slug: string } }) {
	const { data, isLoading, error, refetch } = useQuery<eventInfoType>({
		queryKey: ["event"],
		queryFn: () => fetchEventsById(params.slug),
	});
	return (
		<div className="font-roboto  flex flex-col">
			<Navbar1></Navbar1>
			<div className=" container main ">
				<div className="main h-[75vh] mt-20">
					<Image
						src={"/m.jpg"}
						alt="plus"
						width={500}
						height={500}
						className="h-full w-full rounded-md"
					></Image>
				</div>
			</div>
			<div className="container main mt-10 flex flex-row">
				<div className="w-9/12 ">
					<div className="flex flex-col">
						<h1 className="text-2xl w-11/12 text-wrap align-center">
						{data?.title} <Badge className="bg-primary rounded-md px-2 mr-2" >{data?.eventType}</Badge>
						</h1>
						<div className="flex flex-row mt-5 space-x-6">
							<div className="flex flex-row align-center items-center space-x-3">
								<Calendar size={16}></Calendar>
								<p className="text-sm">{formatDate(data?.eventDate)}</p>
							</div>
							<div className="flex flex-row align-center items-center space-x-3">
								<MapPin size={16} />
								<p className="text-sm">{data?.city} - {data?.address}</p>
							</div>
						</div>
						<div className="mt-6">
							<Image
								src={"/line2.svg"}
								alt="l"
								width={0}
								height={0}
								className="w-11/12"
							></Image>
						</div>
					</div>
					<EventCommonDetails id={data?.id} description={data?.description} duration={data?.duration} organizer={data?.organizer} eventType={data?.eventType}></EventCommonDetails>
				</div>	
				<div className="w-3/12 flex flex-col ">
                    <div className="flex flex-row space-x-4 w-full justify-end">
                        <Heart size={20} ></Heart>
                        <Share size={20}></Share>
                    </div>
                    <div className="flex flex-col border p-3 rounded-md space-y-3 mt-6">
                        {data?.categories.map((category)=><div className="flex flex-col rounded-md border-primary border-2  p-3  space-y-1">
                            <div className="flex flex-row items-center ">
                                <p className="flex-1">{category.name}</p>
                                <div className="flex flex-1 justify-end flex-row space-x-3 items-center">
                                    <Button className="p-0 "><Minus size={16}></Minus></Button>
                                    <p>1</p>
                                    <Button className="p-[2px]"><Plus size={16}></Plus></Button>
                                </div>
                                
                            </div>
                            <p className="font-bold text-sm">{category.price} DH</p>
                        </div>)}
                        
                        <Button>Check Out 200 DH</Button>
                    </div>
                </div>
			</div>

			<Footer></Footer>
		</div>
	);
}
