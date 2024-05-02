"use client";
import React, { useEffect, useState } from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "./ui/card";
import { Label } from "./ui/label";
import { Input } from "./ui/input";
import { Textarea } from "./ui/textarea";
import { format } from "date-fns";
import { Calendar as CalendarIcon } from "lucide-react";
import { useDispatch } from "react-redux";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import SelectBox from "./SelectBox";
import { cities } from "@/core/constantes/Event.const";
import { setEventInfo } from "@/lib/features/events/eventSlice";

function EventDetailsCard({
  title,
  description,
}: {
  title: string;
  description: string;
}) {
  const dispatch = useDispatch();
  const [fdate, setFdate] = useState<Date>();
  const [Data, setData] = useState({
    title: "",
    description: "",
    address: "",
    city: "",
    time: "",
  });
  const eventDate =fdate?.toISOString()?.split("T")[0]
  useEffect(() => {
    const eventDetails = { ...Data, eventDate };
    dispatch(setEventInfo(eventDetails));
  }, [Data, fdate, dispatch]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  return (
    <>
      <Card x-chunk="dashboard-07-chunk-0">
        <CardHeader>
          <CardTitle>{title}</CardTitle>
          <CardDescription>{description}</CardDescription>
        </CardHeader>
        <CardContent>
          <div className="grid gap-6">
            <div className="grid gap-3">
              <Label htmlFor="title">Title</Label>
              <Input
                id="title"
                name="title"
                type="text"
                className="w-full"
                onChange={handleChange}
              />
            </div>
            <div className="grid gap-3">
              <Label htmlFor="description">Description</Label>
              <Textarea
                id="description"
                name="description"
                className="min-h-32"
                onChange={handleChange}
              />
            </div>
            <div className="grid gap-3">
              <Label htmlFor="address">Address</Label>
              <Input
                id="address"
                type="text"
                name="address"
                className="w-full"
                onChange={handleChange}
              />
            </div>
            <div className="grid grid-cols-2 gap-3">
              <div className="grid gap-3">
                <Label htmlFor="city">City</Label>
                <SelectBox
                  items={cities}
                  element="city"
                  title="Select a city"
                  setDataValue={setData}
                  value={Data.city}
                />
              </div>
              <div className="grid gap-3">
              <Label htmlFor="duration">Duration</Label>
              <Input
                id="duration"
                type="number"
                name="duration"
                className="w-full"
                onChange={handleChange}
              />
            </div>
            </div>
            <div className="grid grid-cols-2 gap-3">
              <div className="grid gap-3">
                <Label htmlFor="date">Event's Date</Label>
                <Popover>
                  <PopoverTrigger asChild>
                    <Button
                      variant={"outline"}
                      className={cn(
                        "w-[280px] justify-start text-left font-normal",
                        !fdate && "text-muted-foreground"
                      )}
                    >
                      <CalendarIcon className="mr-2 h-4 w-4" />
                      {fdate ? format(fdate, "PPP") : <span>Pick a date</span>}
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-auto p-0">
                    <Calendar
                      mode="single"
                      selected={fdate}
                      onSelect={setFdate}
                      initialFocus
                    />
                  </PopoverContent>
                </Popover>
              </div>
              <div className="grid gap-3">
                <Label htmlFor="time">Event's Time</Label>
                <input
                  id="time"
                  type="time"
                  name="time"
                  className="p-1.5 rounded-md border border-gray-200 text-gray-400 focus:outline focus:outline-2 focus:outline-offset-2 focus:outline-[#8444ec]"
                  onChange={handleChange}
                  value={Data.time}
                />
              </div>
            </div>
          </div>
        </CardContent>
      </Card>
    </>
  );
}

export default EventDetailsCard;
