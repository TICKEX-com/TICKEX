import { Button } from "@/components/ui/button";
import { ArrowBigLeft } from "lucide-react";
import Image from "next/image";
import Link from "next/link";

export default function NotFound() {
	return (
		<div className="w-full h-screen flex flex-col items-center justify-center space-y-3">
			<Image src={"/E404.svg"} alt="plus" width={400} height={400}></Image>
			<h2 className="text-lg font-semibold">Sorry Page Not Found ! </h2>
			<Link href="/" ><Button> <ArrowBigLeft></ArrowBigLeft> Return Home </Button></Link>
		</div>
	);
}
