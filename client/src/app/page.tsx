"use client";

import { NavLinks } from "@/components/NavLinks";
import Navbar1 from "@/components/Navbar1";
import SearchBar from "@/components/SearchBar";
import Location from "@/components/location";
import Slider from "@/components/slider";
import { DialogOverlay } from "@/components/ui/dialog";
import { Dialog, DialogContent, DialogTrigger } from "@radix-ui/react-dialog";
import { Copyright, MapPin, Search, Sliders } from "lucide-react";
import Image from "next/image";
import { TypeAnimation } from "react-type-animation";
import * as React from "react";
import Paginate from "@/components/Paginate";
import { useRef } from "react";
import Footer from "@/components/footer";
import { fetchEvents } from "./_page";
import { useQuery } from "@tanstack/react-query";
import EventsGrid from "@/components/EventsGrid";
import Link from "next/link";

const features = ["Event Promotion", "Live Events", "Seat Map"];

export default function Home() {
	const scrollRef = useRef(null);
	const { data, isLoading, error, refetch } = useQuery({
		queryKey: ["events"],
		queryFn: () => fetchEvents(),
	});
	return (
		<Dialog>
		<div className="font-roboto">
				<div className=" font-roboto relative w-full">
					<Navbar1 with_NewEvent={true}>
						<NavLinks></NavLinks>
					</Navbar1>
					<DialogOverlay className=" fixed inset-0 z-50 bg-black/80  data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0">
						<DialogContent className="fixed  left-[50%] top-[50%] z-50 grid w-full max-w-lg translate-x-[-50%] translate-y-[-50%] gap-4 border bg-background p-6 shadow-lg duration-200 data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0 data-[state=closed]:zoom-out-95 data-[state=open]:zoom-in-95 data-[state=closed]:slide-out-to-left-1/2 data-[state=closed]:slide-out-to-top-[48%] data-[state=open]:slide-in-from-left-1/2 data-[state=open]:slide-in-from-top-[48%] sm:rounded-lg">
							<SearchBar data={data}></SearchBar>
						</DialogContent>
					</DialogOverlay>
					<div className="container flex flex-row align-center items-center justify-center w-full">
						<div className="container">
							<div className="flex flex-col  font-roboto align-center text-4xl space-y-2 font-bold mb-3">
								<h1 className=" flex flex-row space-x-4 align-middle">
									<p>Unlock the excitement</p>
									<Image
										src={"/plus.svg"}
										alt="plus"
										width={30}
										height={30}
									></Image>
								</h1>
								<TypeAnimation
									sequence={[
										"Reserve Your Seat", // Types 'One'
										1000, // Waits 1s
										"Reserve Your Seat Here",
										1000,
										"Reserve Your Seat & Now",
										1000,
									]}
									wrapper="span"
									cursor={true}
									repeat={Infinity}
									style={{ fontSize: "1em", display: "inline-block" }}
								/>
							</div>
							<p className="text-xs  text-gray-500 mb-3">
								Ready for an adventure? Discover exciting events near you!
								Whether it's a concert, festival, or sports match, find the
								perfect event to make memories and share unforgettable moments.
								Don't wait, your next great experience is just a click away!
								
							</p>
							<div className="mt-12 flex flex-row items-center align-middle space-x-2">

							<DialogTrigger asChild>
									<div>
										<div className="flex text-sm font-medium flex-row align-center items-center">
											<Search height={15}></Search>
											<input
												type="search"
												name="search"
												id=""
												className="py-0 px-2 outline-none"
												placeholder="search events..."
											/>
										</div>
										<Image
											src={"/line.svg"}
											alt="line"
											width={250}
											height={100}
										></Image>
									</div>
							    </DialogTrigger>
									<div>
										<div className="flex flex-row align-center items-center mb-2">
											<MapPin height={15} />
											<Location></Location>
										</div>
										<Image
											src={"/line.svg"}
											alt="line"
											width={200}
											height={100}
										></Image>
									</div>
								</div>
							<div className="flex flex-row justify-end mt-10">
								<Image
									src={"/dots.svg"}
									alt="dots"
									width={100}
									height={100}
								></Image>
							</div>
						</div>
						<div className="w-full h-full relative mt-10">
							<Image
								src={"/untitled.svg"}
								alt="d"
								width={0}
								height={0}
								className="rounded-md h-fit"
								style={{ height: "100%", width: "100%" }}
							></Image>
							<Image
								src={"/item1.svg"}
								alt="item1"
								width={0}
								height={0}
								style={{
									width: "20%",
									height: "20%",
									zIndex: -10,
									position: "absolute",
									top: "80%",
									right: 0,
								}}
							></Image>
						</div>
					</div>
				</div>
			<main className="container w-full flex flex-row justify-center mt-14">
				<Slider></Slider>
			</main>
			<main className="container h-full p-0 items-center relative">
				<Image
					src={"/pannel.svg"}
					alt="pannel"
					width={0}
					height={0}
					layout="cover"
					style={{
						width: "100vw",
						height: "50vh",
					}}
				></Image>
				<Image
					src={"/crazyarrow.svg"}
					alt="arrow"
					width={500}
					height={500}
					className="absolute top-[12%] left-[20%]"
				></Image>
				<div className="container absolute top-[30%] flex font-roboto flex-row items-center">
					<div className="flex flex-col">
						<h1 className="text-4xl font-semibold mb-5 ">What We Offer </h1>
						<p className="max-w-[50vw] text-sm text-gray-600 mb-7 items-center">
							You have access to all of our features, whatever size your event,
							free or paid tickets.
						</p>
						<button className="flex relative w-fit">
							<Image
								src={"/bigticket.svg"}
								alt="pannel"
								width={200}
								height={260}
							></Image>
							<p className="absolute text-md font-semibold text-white top-2 left-5 transition-all duration-400 transform hover:opacity-70">
								See All Features
							</p>
						</button>
					</div>
					<div className="flex flex-1 flex-col w-full justify-center">
						{features.map((val, indx) => (
							<div className="flex mx-auto flex-row mb-7 items-center">
								<Image
									src={"/verify.svg"}
									alt={val}
									width={15}
									height={15}
								></Image>
								<p className="pl-4 text-lg font-semibold">{val}</p>
							</div>
						))}
					</div>
				</div>
			</main>
			<main className="container relative h-fit">
				<div className="flex flex-col w-[25%] mb-7 mt-10">
					<h1 className="text-3xl font-roboto font-bold w-auto">
						Incoming Events
					</h1>
					<Image
						src={"/twolines.svg"}
						alt="two"
						width={0}
						height={0}
						className="w-auto"
					></Image>
				</div>
				<Image
					src={"/halfsquare.svg"}
					alt="two"
					width={50}
					height={50}
					className="absolute left-[5%] transform scale-x-[-1] scale-y-[-1] "
				></Image>
				<EventsGrid events={data}></EventsGrid>
				<Image
					src={"/halfsquare.svg"}
					alt="two"
					width={50}
					height={50}
					className="absolute top-[90%] right-[5%]"
				></Image>
			</main>
			<main className="container h-[30vh] bg-black mb-12 flex flex-col items-center justify-start font-roboto">
				<h1 className="text-white my-auto text-lg font-semibold">
					Our Partners
				</h1>
				<div className="flex flex-row"></div>
			</main>
			<main className=" container w-fit h-full p-0 items-center mb-16">
				<div className="flex flex-col w-fit mx-auto my-10 font-roboto text-xl font-extrabold space-x-3">
					<h1>Tickets For Every Event, Big Or Small, All In One Place</h1>
					<div className="flex flex-row space-x-2">
						<Image
							src={"/twolines.svg"}
							alt="begin"
							width={100}
							height={100}
							className="flex-1"
						></Image>
						<Image
							src={"/twolines.svg"}
							alt="begin"
							width={100}
							height={100}
							className="float-right flex-1 transform scale-x-[-1]  "
						></Image>
					</div>
				</div>
				<div className="relative">
					<Image
						src={"/quote-begin.png"}
						alt="begin"
						width={100}
						height={100}
						className="absolute top-[0%]"
					></Image>
					<Image
						src={"/pannel2.svg"}
						alt="pannel2"
						width={0}
						height={0}
						layout="cover"
						style={{
							width: "100%",
							height: "50vh",
						}}
					></Image>
					<div className="flex flex-col items-left absolute top-[30%] left-[25%] ">
						<p className="text-white font-roboto text-lg max-w-xl">
							“I have tried all the major ticketing sites for my film festival
							and found TickX to be head and shoulders above the competition.
							The site is exceptionally user-friendly.”
						</p>
						<h2 className="mt-10 font-roboto  text-lg font-extrabold flex flex-row items-center">
							Anass Chatt
							<p className="text-sm text-gray-300 font-light">
								, Event Organizer
							</p>
						</h2>
					</div>
					<Image
						src={"/quote-end.png"}
						alt="end"
						width={100}
						height={100}
						className="right-0 absolute top-[90%]"
					></Image>
				</div>
			</main>
			<Footer></Footer>
		</div>
		</Dialog>
	);
}
