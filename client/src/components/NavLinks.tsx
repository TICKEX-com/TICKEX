"use client";

import * as React from "react";
import Link from "next/link";
import Image from "next/image";

import { cn } from "@/lib/utils";
import {
	NavigationMenu,
	NavigationMenuContent,
	NavigationMenuItem,
	NavigationMenuLink,
	NavigationMenuList,
	NavigationMenuTrigger,
	navigationMenuTriggerStyle,
} from "@/components/ui/navigation-menu";

const components: { title: string; href: string; description: string }[] = [
	{
		title: "How it works ?",
		href: "/docs/primitives/alert-dialog",
		description:
			"A modal dialog that interrupts the user with important content and expects a response.",
	},
	{
		title: "Add New Event",
		href: "/docs/primitives/hover-card",
		description:
			"For sighted users to preview content available behind a link.",
	},
	{
		title: "Event Promotion",
		href: "/docs/primitives/hover-card",
		description:
			"For sighted users to preview content available behind a link.",
	},
	{
		title: "Seat Map App",
		href: "/docs/primitives/hover-card",
		description:
			"For sighted users to preview content available behind a link.",
	}, 
];

export function NavLinks() {
	return (
		<NavigationMenu className="font-roboto">
			<NavigationMenuList>
				<NavigationMenuItem>
				<NavigationMenuTrigger chevron={true} className="relative group-aria-expanded:opacity-100  hover:bg-transparent text-lg font-semibold">
						Features
						<Image
							src={"/next.svg"}
							alt="d"
							width={0}
							height={0}
							className="group-aria-expanded:opacity-100 opacity-0 transition-all "
							style={{  width: "80%",position:"absolute", top:30 }}
						></Image>
					</NavigationMenuTrigger>
					<NavigationMenuContent>
						<ul className="grid w-[200px] gap-3 p-4 md:w-[200px] grid-cols-1 lg:w-[250px] ">
							{components.map((component) => (
								<ListItem
									key={component.title}
									title={component.title}
									href={component.href}
								>
									{component.description}
								</ListItem>
							))}
						</ul>
					</NavigationMenuContent>
				</NavigationMenuItem>
				<NavigationMenuItem className="relative  hover:bg-transparent ">
					<Link href="/docs" legacyBehavior passHref>
					<NavigationMenuTrigger  className="relative group hover:bg-transparent text-lg font-semibold">
						Pricing
						<Image
							src={"/next.svg"}
							alt="s"
							width={0}
							height={0}
							className="opacity-0  transition-all group-aria-expanded:opacity-100"
							style={{  width: "80%",position:"absolute", top:30 }}
						></Image>
					</NavigationMenuTrigger>
					</Link>
				</NavigationMenuItem>
				<NavigationMenuItem  className="flex flex-col align-center justify-center items-center">
					<NavigationMenuTrigger chevron={true} className="relative group hover:bg-transparent text-lg font-semibold">
						About Us
						<Image
							src={"/next.svg"}
							alt="d"
							width={0}
							height={0}
							className="group-aria-expanded:opacity-100 opacity-0 transition-all "
							style={{  width: "80%",position:"absolute", top:30 }}
						></Image>
					</NavigationMenuTrigger>
          			
					<NavigationMenuContent>
						<ul className="grid grid-cols-1  gap-3 p-4 md:w-[300px] lg:w-[400px] sm:w-[200px]">
						
							<ListItem href="/docs" title="Blog">
								Re-usable components built using Radix UI and Tailwind CSS.
							</ListItem>
							<ListItem href="/docs/installation" title="Who Are We ?">
								How to install dependencies and structure your app.
							</ListItem>
							<ListItem href="/docs/primitives/typography" title="Support">
								Styles for headings, paragraphs, lists...etc
							</ListItem>
						</ul>
					</NavigationMenuContent>
				</NavigationMenuItem>
			</NavigationMenuList>
		</NavigationMenu>
	);
}

const ListItem = React.forwardRef<
	React.ElementRef<"a">,
	React.ComponentPropsWithoutRef<"a">
>(({ className, title, children, ...props }, ref) => {
	return (
		<li>
			<NavigationMenuLink asChild>
				<a
					ref={ref}
					className={cn(
						"block select-none space-y-1 rounded-md p-3 leading-none no-underline outline-none transition-colors hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground",
						className
					)}
					{...props}
				>
					<div className="text-sm font-medium leading-none">{title}</div>
					<p className="line-clamp-2 text-sm leading-snug text-muted-foreground">
						{children}
					</p>
				</a>
			</NavigationMenuLink>
		</li>
	);
});
ListItem.displayName = "ListItem";
