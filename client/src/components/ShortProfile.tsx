import {
	Cloud,
	CreditCard,
	Github,
	Keyboard,
	LifeBuoy,
	LogOut,
	Mail,
	MessageSquare,
	Plus,
	PlusCircle,
	Settings,
	User,
	UserPlus,
	Users,
} from "lucide-react";

import { Button } from "@/components/ui/button";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuGroup,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuPortal,
	DropdownMenuSeparator,
	DropdownMenuShortcut,
	DropdownMenuSub,
	DropdownMenuSubContent,
	DropdownMenuSubTrigger,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { AvatarDemo } from "./ui/avatar";
import {  useRouter } from "next/navigation";
import Link from "next/link";
import { useDispatch } from "react-redux";
import { useMutation } from "@tanstack/react-query";
import { logout } from "@/lib/features/auth/authSlice";
import api from "@/lib/api";

export default function ShortProfile() {
  const router = useRouter();
  const dispatch = useDispatch();
	const { mutate } = useMutation({
		mutationFn: async () => {
			try {
				const response = await api.post(
					`authentication-service/api/auth/logout`
				);
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
		<DropdownMenu>
			<DropdownMenuTrigger className="outline-0">
				<AvatarDemo></AvatarDemo>
			</DropdownMenuTrigger>
			<DropdownMenuContent className="w-56 mr-5">
				<DropdownMenuLabel>My Account</DropdownMenuLabel>
				<DropdownMenuSeparator />
				<DropdownMenuGroup>
					<DropdownMenuItem>
						<User className="mr-2 h-4 w-4" />
						<span>Profile</span>
					</DropdownMenuItem>
					<DropdownMenuItem>
						<Settings className="mr-2 h-4 w-4" />
						<span>Settings</span>
						<DropdownMenuShortcut>⌘S</DropdownMenuShortcut>
					</DropdownMenuItem>
					<DropdownMenuItem>
						<LifeBuoy className="mr-2 h-4 w-4" />
						<Link href={"/dashboard"}>Dashoard</Link>
					</DropdownMenuItem>
					<DropdownMenuItem>
						<LogOut className="mr-2 h-4 w-4" />
						<span onClick={handleLogOut}>Log out</span>
						<DropdownMenuShortcut>⇧⌘Q</DropdownMenuShortcut>
					</DropdownMenuItem>
				</DropdownMenuGroup>
			</DropdownMenuContent>
		</DropdownMenu>
	);
}
