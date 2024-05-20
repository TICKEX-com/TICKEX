import React from "react";
import { Button } from "./ui/button";
import { Minus, Plus } from "lucide-react";

export default function SeatCard() {
	return (
		<div className="p-4 border-l-4 bg-[rgb(238,238,238)] border-primary mb-3 rounded-md flex flex-row space-x-5 items-center">
			<div className="flex flex-col">
				<p className=" font-thin text-xs text-gray-500">Position</p>
				<p className="font-thin text-md text-black pt-3 w-full text-wrap max-w-25">
					Upper stand
				</p>
			</div>
			<div className="flex flex-col items-center">
				<p className=" font-thin text-xs text-gray-500">Category</p>
				<p className="font-thin text-md text-black pt-3">VIP</p>
			</div>
			<div className="flex flex-col items-center">
				<p className=" font-thin text-xs text-gray-500">Price</p>
				<p className="font-thin text-md text-black pt-3">200 DH</p>
			</div>
			<div className="flex flex-col items-center">
				<p className=" font-thin text-xs text-gray-500">The Number</p>
				<div className="flex justify-end flex-row space-x-3 items-center">
						<Minus size={12}></Minus>
					<p>1</p>
						<Plus size={12} ></Plus>
				</div>{" "}
			</div>
			<div className="flex flex-col items-center ">
				<Button className="py-0">Book</Button>
			</div>
		</div>
	);
}
