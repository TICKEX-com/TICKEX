import * as React from "react"

import { Card, CardContent } from "@/components/ui/card"
import Image from "next/image";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel"

const images=["/Music.svg","/Soccer.svg","/Conference.svg","/Party.svg","/comedy.svg"]
const title=["Music Show","Soccer Game","Conference","Party","Comedy show"]

export default function Slider() {
  return (
    <Carousel
      opts={{
        align: "start",
      }}
      className="w-[80%] mb-10"
    >
      <CarouselContent>
        {images.map((val, index) => (
          <CarouselItem key={index} className="sm:basis-1/2 md:basis-1/3 lg:basis-1/4 items-center flex flex-col group transition-transform duration-300 transform">
              <Card className="rounded-full h-36 w-36 ">
                <CardContent className="flex aspect-square items-center justify-center">
                <Image src={val} alt={val} width={(index!=1&&index!=2) ? 140 :100} height={(index!=1&&index!=2)  ?140 :100} className="transition-transform duration-300 transform group-hover:scale-95" ></Image>
                </CardContent>
              </Card>
              <h2 className="text-sm font-semibold group-hover:opacity-60">{title[index]}</h2>
          </CarouselItem>
        ))}
      </CarouselContent>
      <CarouselPrevious />
      <CarouselNext />
    </Carousel>
  )
}
