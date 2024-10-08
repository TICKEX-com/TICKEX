"use client";

import React, { useEffect } from "react";
import Image from "next/image";
import Link from "next/link";
import {
	Breadcrumb,
	BreadcrumbItem,
	BreadcrumbLink,
	BreadcrumbList,
	BreadcrumbPage,
	BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import {
	Home,
	LineChart,
	Package,
	Package2,
	PanelLeft,
	ShoppingCart,
	Users2,
} from "lucide-react";
import { usePathname, useRouter } from "next/navigation";
import { useAppSelector } from "@/lib/hooks";
import { useMutation } from "@tanstack/react-query";
import api from "@/lib/api";
import { useDispatch } from "react-redux";
import { logout } from "@/lib/features/auth/authSlice";
import ShortProfile from "./ShortProfile";

function TopNav() {
	const pathname = usePathname();
	const segments = pathname.split("/").filter((item) => item !== "");
	const profileImg = useAppSelector((state) => state.auth.userInfo);
	const router = useRouter();
	const dispatch = useDispatch();
	useEffect(() => {
		console.log(profileImg);
	}, []);
	const { mutate } = useMutation({
		mutationFn: async () => {
			try {
				const response = await api.post(
					`authentication-service/api/auth/logout`
				);
				console.log(response);
				dispatch(logout());
				router.push("/login");
			} catch (error) {
				console.log(error);
				throw error;
			}
		},
	});

	const handleLogOut = async () => {
		mutate();
	};
	return (
		<div className="flex flex-col sm:gap-4 sm:py-4 sm:pl-14">
			<header className="sticky top-0 z-30 flex justify-between h-14 items-center gap-4 border-b bg-background px-4 sm:static sm:h-auto sm:border-0 sm:bg-transparent sm:px-6">
				<Sheet>
					<SheetTrigger asChild>
						<Button size="icon" variant="outline" className="sm:hidden">
							<PanelLeft className="h-5 w-5" />
							<span className="sr-only">Toggle Menu</span>
						</Button>
					</SheetTrigger>
					<SheetContent side="left" className="sm:max-w-xs">
						<nav className="grid gap-6 text-lg font-medium">
							<Link
								href="#"
								className="group flex h-10 w-10 shrink-0 items-center justify-center gap-2 rounded-full bg-primary text-lg font-semibold text-primary-foreground md:text-base"
							>
								<Package2 className="h-5 w-5 transition-all group-hover:scale-110" />
								<span className="sr-only">Acme Inc</span>
							</Link>
							<Link
								href="#"
								className="flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground"
							>
								<Home className="h-5 w-5" />
								Dashboard
							</Link>
							<Link
								href="#"
								className="flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground"
							>
								<ShoppingCart className="h-5 w-5" />
								Orders
							</Link>
							<Link
								href="#"
								className="flex items-center gap-4 px-2.5 text-foreground"
							>
								<Package className="h-5 w-5" />
								Products
							</Link>
							<Link
								href="#"
								className="flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground"
							>
								<Users2 className="h-5 w-5" />
								Customers
							</Link>
							<Link
								href="#"
								className="flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground"
							>
								<LineChart className="h-5 w-5" />
								Settings
							</Link>
						</nav>
					</SheetContent>
				</Sheet>
				<Breadcrumb className="hidden md:flex">
					<BreadcrumbList>
						<BreadcrumbItem>
							<BreadcrumbLink asChild>
								<Link href="/dashboard">Dashboard</Link>
							</BreadcrumbLink>
						</BreadcrumbItem>
						{segments.length !== 0 && (
							<>
								<BreadcrumbSeparator />
								<BreadcrumbItem>
									<BreadcrumbPage>{segments[1]}</BreadcrumbPage>
								</BreadcrumbItem>
							</>
						)}
					</BreadcrumbList>
				</Breadcrumb>
				<DropdownMenu>
					<ShortProfile></ShortProfile>
				</DropdownMenu>
			</header>
		</div>
	);
}

export default TopNav;
