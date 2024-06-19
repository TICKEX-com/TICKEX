"use client";

import { NavLinks } from "@/components/NavLinks";
import Navbar1 from "@/components/Navbar1";
import { MapPin, Search, Ticket } from "lucide-react";
import Image from "next/image";
import React, { useEffect, useState } from "react";
import Footer from "@/components/footer";
import {
	Dialog,
	DialogContent,
	DialogOverlay,
	DialogTrigger,
} from "@/components/ui/dialog";
import { cities, event_types } from "@/core/constants/pages_constants";
import ProgressSlider from "@/components/ProgressSlider";
import EventsGrid from "@/components/EventsGrid";
import Paginate from "@/components/Paginate";
import SearchBar from "@/components/SearchBar";
import { fetchByFilter } from "./_page";
import { useQuery } from "@tanstack/react-query";
import { useSearchParams } from 'next/navigation'


export default function page() {
	const searchParams = useSearchParams()

	const city = searchParams.get('city')
	const category = searchParams.get('category')
	const title = searchParams.get('title')


	const [minprice, setMinPrice] = useState(0)
	const [maxprice, setMaxPrice] = useState(0)
	const [eventtype, setEventType] = useState<string>(category??"null")
	const [_city, setCity] = useState<string>(city??"null")
	const [pagenumber, setPageNumber] = useState<number>(1)

	const { data, isLoading, error, refetch } = useQuery({
		queryKey: ["fetch_events"],
		queryFn: () => fetchByFilter(minprice,maxprice,eventtype,_city,pagenumber),
	});
	useEffect(() => {
	  refetch()
	}, [eventtype,_city,maxprice,minprice])
	const handleValueChange =(value: number[])=>{
		setMinPrice(value[0])
		setMaxPrice(value[1])
	}
	const handleUnsetParams= ()=>{
	}
	return (
		<Dialog>
			<div className="font-roboto  flex flex-col items-center">
				<Navbar1>
					<NavLinks></NavLinks>
				</Navbar1>
				<DialogOverlay className="container fixed inset-0 z-50 bg-black/80  data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0">
					<DialogContent className="fixed left-[50%] top-[50%] z-50 grid w-full max-w-lg translate-x-[-50%] translate-y-[-50%] gap-4 border bg-background p-6 shadow-lg duration-200 data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0 data-[state=closed]:zoom-out-95 data-[state=open]:zoom-in-95 data-[state=closed]:slide-out-to-left-1/2 data-[state=closed]:slide-out-to-top-[48%] data-[state=open]:slide-in-from-left-1/2 data-[state=open]:slide-in-from-top-[48%] sm:rounded-lg">
						<SearchBar></SearchBar>
					</DialogContent>
				</DialogOverlay>
				<div className="container relative main text-sm w-fill font-medium mt-20 mx-auto flex flex-row items-center space-x-3 ">
					<DialogTrigger asChild className="w-fit mx-auto my-4">
						<div className="flex flex-col space-y-6">
							<h1 className="text-2xl font-extrabold ">Find Out What's On</h1>
							<div>
								<div className="flex flex-row align-center items-center">
									<Search height={15}></Search>
									<input
										type="search"
										name="search"
										id=""
										className="py-0 px-2 outline-none bg-transparent"
										placeholder="search events..."
									/>
								</div>
								<Image
									src={"/line.svg"}
									alt="line"
									width={350}
									height={100}
								></Image>
								<Image
									src={"/halfSquare.svg"}
									alt="pannels"
									width={0}
									height={0}
									layout="cover"
									style={{
										width: "10vw",
										height: "10vh",
									}}
									className="absolute left-[10%] top-0 transform scale-x-[-1] scale-y-[-1] "
								></Image>
								<Image
									src={"/dots.svg"}
									alt="pannels"
									width={0}
									height={0}
									layout="cover"
									style={{
										width: "6vw",
										height: "6vh",
									}}
									className="absolute left-[20%] top-[10%] transform  "
								></Image>
								<Image
									src={"/twoplus.svg"}
									alt="twoplus"
									width={0}
									height={0}
									layout="cover"
									style={{
										width: "10vw",
										height: "10vh",
									}}
									className="absolute right-[20%] top-[60%] transform  "
								></Image>
								<Image
									src={"/halfSquare.svg"}
									alt="pannels"
									width={0}
									height={0}
									layout="cover"
									style={{
										width: "10vw",
										height: "10vh",
									}}
									className="absolute right-[10%] top-[80%] transform  "
								></Image>
							</div>
						</div>
					</DialogTrigger>
				</div>
				<div className="container main flex mt-8 flex-row">
					<div className="w-4/12 flex flex-col">
						<div className="flex flex-col w-[25%] mt-10">
							<h1 className="text-xl font-roboto font-bold w-auto">Filters</h1>
							<Image
								src={"/twolines.svg"}
								alt="two"
								width={0}
								height={0}
								className="w-auto"
							></Image>
						</div>
						<form className="w-2/3 space-y-6">
							<div className="flex flex-row w-fit items-center mt-3">
								<MapPin height={15} />
								<h1 className="text-md font-roboto font-bold w-auto">
									Price Range{" "}
								</h1>
							</div>
							<ProgressSlider handleValueChange={handleValueChange}></ProgressSlider>
							<div className="flex flex-row w-fit items-center mt-3">
								<MapPin height={15} />
								<h1 className="text-md font-roboto font-bold w-auto">City</h1>
							</div>
							<div className="space-y-2 pl-6">
								{cities.map((citye) => (
									<div className="flex items-center space-x-2">
										<input
											type="radio"
											name="city"
											id={citye}
											value={citye}
											className="accent-violet-600 h-4 w-4"
											onChange={(e)=>setCity(e.currentTarget.value)}
											defaultChecked={citye==city&&_city==city}
										/>
										<label htmlFor={citye} className="text-sm">
											{citye}
										</label>
									</div>
								))}
							</div>
							<div className="flex flex-row w-fit items-center mt-3">
								<Ticket height={15} />
								<h1 className="text-md font-roboto font-bold w-auto">Event</h1>
							</div>
							<div className="space-y-2 pl-6">
								{event_types.map((event) => (
									<div className="flex items-center space-x-2">
										<input
											type="radio"
											name="event_type"
											id={event}
											value={event}
											className="accent-violet-600 h-4 w-4"
											onChange={(e)=>setEventType(e.currentTarget.value)}
											defaultChecked={event==category&&eventtype==category}
										/>
										<label htmlFor={event} className="text-sm">
											{event}
										</label>
									</div>
								))}
							</div>
						</form>
					</div>
					<div className="w-8/12 flex flex-col ">
					    <EventsGrid events={data}></EventsGrid>
						{data&&<Paginate></Paginate>}
					</div>
				</div>
				<Footer></Footer>
			</div>
		</Dialog>
	);
}
