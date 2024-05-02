"use client";

import { NavLinks } from "@/components/NavLinks";
import Navbar1 from "@/components/Navbar1";

import Image from "next/image";
import React, { useEffect, useRef } from "react";
import Footer from "@/components/footer";
import { fabric } from "fabric";
import { Calendar, Heart, MapPin, Share } from "lucide-react";
import EventCommonDetails from "@/components/EventCommonDetails";
import { ScrollArea } from "@/components/ui/scroll-area";
import SeatCard from "@/components/SeatCard";

export default function page({ params }: { params: { slug: string } }) {
	const canvasID = useRef(null);
	useEffect(() => {
		const { width, height } = canvasID.current?.getBoundingClientRect();
		const canvasObject = new fabric.Canvas(canvasID.current, {
			backgroundColor: "rgb(238,238,238)",
			width: width,
			height: height,
			defaultCursor: "crosshair",
			renderOnAddRemove: true,
		});
		// canvasObject.on("mouse:over",(e:any)=>{e.target.forEachObject((ob:any)=>{if(ob.text){ob.set({"visible":true,"top":"center","left":"center"})}});canvasObject.renderAll();});
		// canvasObject.on("mouse:out",(e:any)=>{e.target.forEachObject((ob:any)=>{if(ob.text)ob.set("visible",false)});canvasObject.renderAll();});
		return () => {
			canvasObject.dispose();
		};
	}, []);
	const tags = Array.from({ length: 10 }).map(
		(_, i, a) => `v1.2.0-beta.${a.length - i}`
	);

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
			<div className="container main mt-10 flex flex-col ">
				<div className="flex flex-row">
					<div className="flex flex-col w-9/12">
						<h1 className="text-2xl w-11/12 text-wrap">
							George Michael By Candlelight at The Monastery, Manchester
						</h1>
						<div className="flex flex-row mt-5 space-x-6">
							<div className="flex flex-row align-center items-center space-x-3">
								<Calendar size={16}></Calendar>
								<p className="text-sm">Fri 19 Jul 2024 8:00 PM</p>
							</div>
							<div className="flex flex-row align-center items-center space-x-3">
								<MapPin size={16} />
								<p className="text-sm">The Monastery, Manchester, M12 2WF</p>
							</div>
						</div>
						<div className="mt-2">
							<Image
								src={"/line2.svg"}
								alt="l"
								width={0}
								height={0}
								className="w-11/12"
							></Image>
						</div>
					</div>
					<div className="flex flex-row space-x-4 justify-end w-3/12">
						<Heart size={20}></Heart>
						<Share size={20}></Share>
					</div>
				</div>
				<div className="flex flex-col w-41  mb-4 mt-6">
					<h1 className="text-lg font-roboto font-bold w-auto">
						Available Seats
					</h1>
					<Image
						src={"/twolines.svg"}
						alt="two"
						width={0}
						height={0}
						className="w-40 "
					></Image>
				</div>
				<div className="flex flex-row  h-[60vh]">
					<div className="w-5/12 h-[60vh]">
						<ScrollArea className="h-full w-full rounded-md ">
							<div className="p-4">
								{tags.map((tag) => (
									<>
										<div key={tag} className="text-sm">
											<SeatCard />
										</div>
										{/* <Separator className="my-2" /> */}
									</>
								))}
							</div>
						</ScrollArea>
					</div>
					<div className="w-7/12 ">
						<canvas className="w-full h-full " ref={canvasID}></canvas>
					</div>
				</div>
				<div className="w-9/12">
					<EventCommonDetails></EventCommonDetails>
				</div>
			</div>
			<Footer></Footer>
		</div>
	);
}
