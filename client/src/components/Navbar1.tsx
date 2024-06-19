import React from "react";
import Image from "next/image";
import ShortProfile from "./ShortProfile";
import { useAppSelector } from "@/lib/hooks";
import Link from "next/link";
import Cookies from "js-cookie";

type props = {
  children?: React.ReactNode;
  with_NewEvent?: Boolean;
};
const icons = ["/notifications.svg", "/panier.svg"];
export default function Navbar1({ children, with_NewEvent }: props) {
  const myCookie = Cookies.get("jwtToken");
  return (
    <nav
      className={`w-full z-50 bg-white justify-between right-0 left-0  fixed h-fit flex flex-row py-2 pb-0 px-3  `}
    >
      <div className="container flex-1 w-full flex flex-row space-x-3 align-center my-auto justify-start">
        <Link href={"/"} className="text-2xl text-black font-semibold">
          Tick<span className="text-purple-600">X</span>
        </Link>
        <div className="w-full flex flex-row align-center items-center">
          {children}
        </div>
      </div>
      <div className="container flex-1 w-full flex flex-row align-center items-center justify-end">
        {with_NewEvent == true&&myCookie && (
          <Link href={"/dashboard/events/addevent"}className="flex border-0 flex-row text-black align-center justify-end  relative items-center">
            <Image
              src={"/ticket2.svg"}
              alt="d"
              width={0}
              height={0}
              style={{ height: "35%", width: "35%" }}
            ></Image>
            <p className="absolute text-white right-4 text-sm font-semibold cursor-pointer">
              New Event
            </p>
          </Link>
        )}

        {myCookie&&icons.map((val, indx) => (
          <div className="items-center">
            <button className="relative border-0 group inline-flex h-12 w-16 items-center justify-center rounded-md bg-background px-2 py-2 text-sm font-medium transition-colors focus:bg-accent focus:text-accent-foreground focus:outline-none disabled:pointer-events-none disabled:opacity-50 hover:bg-accent/50 data-[active]:bg-accent/50">
              <Image src={val} alt={val} width={25} height={25}></Image>
              <Image
                src={"/next.svg"}
                alt={val}
                width={0}
                height={0}
                className="opacity-0  transition-all group-hover:opacity-100"
                style={{
                  width: "90%",
                  height: "50%",
                  position: "absolute",
                  top: 25,
                }}
              ></Image>
            </button>
          </div>
        ))}

        <div className="items-center">
          {!myCookie?
          <div>
          <Link href={"/login"} className="relative border-0 group text-md font-semibold inline-flex h-12 w-16 items-center justify-center rounded-md bg-background px-4 py-2  transition-colors focus:bg-accent focus:text-accent-foreground focus:outline-none disabled:pointer-events-none disabled:opacity-50 hover:bg-accent/50 data-[active]:bg-accent/50">
						Login
						<Image
							src={"/next.svg"}
							alt="s"
							width={0}
							height={0}
							className="opacity-0  transition-all group-hover:opacity-100"
							style={{
								width: "90%",
								height: "50%",
								position: "absolute",
								top: 25,
							}}
						></Image>
					</Link>
          <Link href={"/register"} className="relative border-0 group text-md font-semibold inline-flex h-12 w-16 items-center justify-center rounded-md bg-background px-4 py-2  transition-colors focus:bg-accent focus:text-accent-foreground focus:outline-none disabled:pointer-events-none disabled:opacity-50 hover:bg-accent/50 data-[active]:bg-accent/50">
          Signup
          <Image
            src={"/next.svg"}
            alt="s"
            width={0}
            height={0}
            className="opacity-0  transition-all group-hover:opacity-100"
            style={{
              width: "90%",
              height: "50%",
              position: "absolute",
              top: 25,
            }}
          ></Image>
        </Link></div>:
          <ShortProfile></ShortProfile>}
        </div>
      </div>
    </nav>
  );
}
