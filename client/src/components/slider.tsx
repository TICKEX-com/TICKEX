import * as React from "react";

import { Card, CardContent } from "@/components/ui/card";
import Image from "next/image";
import {
	Carousel,
	CarouselContent,
	CarouselItem,
	CarouselNext,
	CarouselPrevious,
} from "@/components/ui/carousel";
import { motion, Variants } from "framer-motion";
import { event_types, event_types_images } from "@/core/constants/pages_constants";


const cardVariants: Variants = {
	offscreen: {
		opacity: 0,
	},
	onscreen: {
		opacity: 1,
		transition: {
			duration: 0.8,
		},
	},
};
export default function Slider() {
	return (
		<Carousel
			opts={{
				align: "start",
			}}
			className="w-[80%] mb-10"
		>
			<CarouselContent>
				{event_types_images.map((val, index) => (
						<CarouselItem
							key={index}
							className="sm:basis-1/2 md:basis-1/3 lg:basis-1/4 items-center flex flex-col group transition-transform duration-300 transform"
						>
							<Card className="rounded-full h-36 w-36 ">
								<CardContent className="flex aspect-square items-center justify-center">
									<Image
										src={val}
										alt={val}
										width={index != 1 && index != 2 ? 140 : 100}
										height={index != 1 && index != 2 ? 140 : 100}
										className="transition-transform duration-300 transform group-hover:scale-95"
									></Image>
								</CardContent>
							</Card>
							<h2 className="text-sm font-semibold group-hover:opacity-60">
								{event_types[index]}
							</h2>
						</CarouselItem>
				))}
			</CarouselContent>
			<CarouselPrevious children={undefined} />
			<CarouselNext children={undefined} />
		</Carousel>
	);
}
