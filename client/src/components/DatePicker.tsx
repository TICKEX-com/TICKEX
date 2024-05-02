"use client";
import * as React from "react";
import { format } from "date-fns";
import { CalendarDays, Calendar as CalendarIcon } from "lucide-react";

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
	Popover,
	PopoverContent,
	PopoverTrigger,
} from "@/components/ui/popover";

export function DatePickerDemo() {
	const [date, setDate] = React.useState<Date>();
	return (
		<Popover>
			<PopoverTrigger asChild>
				<Button
					variant={"outline"}
					className={cn(
						" justify-start text-left font-normal px-0 py-0 my-0 w-fit hover:bg-transparent h-fit"
					)}
				>
					<CalendarDays height={15} />
					{date ? format(date, "PPP") : <span className={cn(!date && "text-muted-foreground")}>Pick a date</span>}
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
	);
}
