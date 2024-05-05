import React from "react";
import ThemeToggle from "./ThemeToggle";
import Image from "next/image";
import { Button } from "./ui/button";
import ShortProfile from "./ShortProfile";

type props = {
  children?: React.ReactNode;
  with_NewEvent?: Boolean;
};
const icons = ["/notifications.svg", "/panier.svg"];
export default function Navbar1({ children, with_NewEvent }: props) {
  return (
    <nav
      className={`w-full container mx-auto fixed h-fit flex flex-row py-2 pb-0 px-3 items-center bg-white z-50`}
    >
      <div className="container flex-1 w-full flex flex-row space-x-3 align-center my-auto justify-start">
        <div className="text-2xl text-black font-semibold">
          Tick<span className="text-purple-600">X</span>
        </div>
        <div className="w-full flex flex-row align-center items-center">
          {children}
        </div>
      </div>
      <div className="container flex-1 w-full flex flex-row align-center items-center justify-end">
        {with_NewEvent == true && (
          <div className="flex flex-row text-black align-center justify-end  relative items-center">
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
          </div>
        )}

        {icons.map((val, indx) => (
          <div className="items-center">
            <button className="relative group inline-flex h-12 w-16 items-center justify-center rounded-md bg-background px-2 py-2 text-sm font-medium transition-colors focus:bg-accent focus:text-accent-foreground focus:outline-none disabled:pointer-events-none disabled:opacity-50 hover:bg-accent/50 data-[active]:bg-accent/50">
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
          {/* <button className="relative group text-md font-semibold inline-flex h-12 w-16 items-center justify-center rounded-md bg-background px-4 py-2  transition-colors focus:bg-accent focus:text-accent-foreground focus:outline-none disabled:pointer-events-none disabled:opacity-50 hover:bg-accent/50 data-[active]:bg-accent/50">
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
					</button> */}
          <ShortProfile></ShortProfile>
        </div>
        {/* <div className="px-3">
					<ThemeToggle></ThemeToggle>
				</div> */}
      </div>
    </nav>
  );
}
