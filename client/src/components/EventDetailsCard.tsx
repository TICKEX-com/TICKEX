"use client";
import React, { useState } from "react";
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

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";

function EventDetailsCard({
  title,
  description,
}: {
  title: string;
  description: string;
}) {
  const [date, setDate] = useState<Date>();
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
              <Label htmlFor="name">Title</Label>
              <Input id="name" type="text" className="w-full" defaultValue="" />
            </div>
            <div className="grid gap-3">
              <Label htmlFor="description">Description</Label>
              <Textarea id="description" defaultValue="" className="min-h-32" />
            </div>
            <div className="grid gap-3">
              <Label htmlFor="address">Address</Label>
              <Input
                id="address"
                type="text"
                className="w-full"
                defaultValue=""
              />
            </div>
            <div className="grid gap-3">
              <Label htmlFor="city">City</Label>
              <Input id="city" type="text" className="w-full" defaultValue="" />
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
                        !date && "text-muted-foreground"
                      )}
                    >
                      <CalendarIcon className="mr-2 h-4 w-4" />
                      {date ? format(date, "PPP") : <span>Pick a date</span>}
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-auto p-0">
                    <Calendar
                      mode="single"
                      selected={date}
                      onSelect={setDate}
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
                  className="p-1.5 rounded-md border border-gray-200 text-gray-400 focus:outline focus:outline-2 focus:outline-offset-2 focus:outline-[#8444ec]"
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
