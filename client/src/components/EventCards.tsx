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

export default function EventCard() {
	return (
		<Card className="max-w-xl min-w-xl font-roboto transition-all ease-in-out duration-300  border-2 border-transparent pointer hover:border-gray-200 hover:scale-95 m-2">
			<CardHeader>
				<Image
					src={"/music.svg"}
					alt="two"
					width={0}
					height={0}
					className="w-full"
				></Image>
			</CardHeader>
			<CardContent className="pl-2 flex flex-col ">
				<div className="tags flex flex-row max-w-xs overflow-hidden"><Badge className="bg-[#26C281] rounded-md px-2 mr-2" >Just Added</Badge></div>
				<div className="name text-md max-w-xs font-bold">14th International Harbour Masters Congress 2024.</div>
				<div className="mt-2 date text-[10px]  text-gray-600">Sun 16 Feb, 6:00 pm</div>
				<div className="mt-2 location text-xs ">Tangier,old medina</div>
				<div className="price-range text-xs font-bold mt-6 mb-2">From £800.00</div>
			</CardContent>
		</Card>
	);
}
