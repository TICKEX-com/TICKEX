import React from "react";
import Image from "next/image";
import Link from "next/link";
import { Button } from "@/components/ui/button";
import { ArrowBigLeft, PlusCircle } from "lucide-react";

function page() {
  return (
    <div className="h-screen flex justify-center items-center flex-col">
      <Image
        src="/svg/Waiting-bro.svg"
        alt="plus"
        width={500}
        height={500}
        loading="lazy"
      />
      <div className="grid gap-4 mt-3 mb-3">
        <h2 className="text-xl font-bold">
          Your demand is currently being processed
        </h2>
        <div className="flex justify-center">
          <Link href="/">
            <Button>
              {" "}
              <ArrowBigLeft/> Return Home{" "}
            </Button>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default page;
