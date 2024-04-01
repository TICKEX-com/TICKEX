"use client";

import { NavLinks } from "@/components/NavLinks";
import Navbar1 from "@/components/Navbar1";

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
				<div className="w-5/12 "></div>
				<div className="w-7/12 "></div>
			</div>

			<Footer></Footer>
		</div>
	);
}
