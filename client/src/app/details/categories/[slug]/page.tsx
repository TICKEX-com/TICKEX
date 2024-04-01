"use client";

import { NavLinks } from "@/components/NavLinks";
import Navbar1 from "@/components/Navbar1";
import { Calendar, Heart, MapPin, Minus, Plus, Save, Share, Timer } from "lucide-react";
import Image from "next/image";
import React from "react";
import Map from "@/components/Map";
import Footer from "@/components/footer";
import { Button } from "@/components/ui/button";

export default function page({ params }: { params: { slug: string } }) {
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
			<div className="container main mt-10 flex flex-row ">
				<div className="w-9/12 ">
					<div className="flex flex-col">
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
						<div className="mt-6">
							<Image
								src={"/line2.svg"}
								alt="l"
								width={0}
								height={0}
								className="w-11/12"
							></Image>
						</div>
						<div className="flex flex-col">
							<div className="flex flex-col  w-32  mb-7 mt-5">
								<h1 className="text-lg font-roboto font-bold w-auto">
									About Event
								</h1>
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
									Hey there! Are you ready for an exciting event happening in
									Tanger, المغرب? Join us for a fun-filled day at
									835780030016989217748266! This in-person event will bring
									together people from all walks of life to enjoy a day of
									activities, networking, and more. Whether you're a local or
									just visiting, this event is a great opportunity to connect
									with others and have a fantastic time. Don't miss out on this
									unique experience in Tanger, المغرب!
								</p>
							</div>
							<div className="flex flex-col w-40  mb-4 mt-5">
								<h1 className="text-lg font-roboto font-bold w-auto">
									Refund Policy
								</h1>
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
									Contact the organizer to request a refund.
								</p>
							</div>
							<div className="flex flex-col w-40 mb-4 mt-5">
								<h1 className="text-lg font-roboto font-bold w-auto">
									Organized by
								</h1>
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
									Contact the organizer to request a refund.
								</p>
							</div>
							<div className="flex flex-col w-40 mb-4 mt-5">
								<h1 className="text-lg font-roboto font-bold w-auto">
									Location
								</h1>
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
					</div>
				</div>
				<div className="w-3/12 flex flex-col ">
                    <div className="flex flex-row space-x-4 w-full justify-end">
                        <Heart size={20} ></Heart>
                        <Share size={20}></Share>
                    </div>
                    <div className="flex flex-col border p-3 rounded-md space-y-3 mt-6">
                        <div className="flex flex-col rounded-md border-primary border-2  p-3  space-y-1">
                            <div className="flex flex-row items-center ">
                                <p className="flex-1">Category A</p>
                                <div className="flex flex-1 justify-end flex-row space-x-3 items-center">
                                    <Button className="p-0 "><Minus size={16}></Minus></Button>
                                    <p>1</p>
                                    <Button className="p-[2px]"><Plus size={16}></Plus></Button>
                                </div>
                                
                            </div>
                            <p className="font-bold text-sm">100 DH</p>
                        </div>
                        <div className="flex flex-col rounded-md border-primary border-2   p-3  space-y-1">
                            <div className="flex flex-row items-center ">
                                <p className="flex-1">Category B</p>
                                <div className="flex flex-1 justify-end flex-row space-x-3 items-center">
                                    <Button className="p-0 "><Minus size={16}></Minus></Button>
                                    <p>1</p>
                                    <Button className="p-[2px]"><Plus size={16}></Plus></Button>
                                </div>
                                
                            </div>
                            <p className=" text-sm">200 DH</p>
                        </div>
                        <Button>Check Out 200 DH</Button>
                    </div>
                </div>
			</div>

			<Footer></Footer>
		</div>
	);
}
