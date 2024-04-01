"use client";

import React, { useState } from "react";
import Link from "next/link";
import { icons } from "@/components/ui/icons";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { PhoneInput } from "react-international-phone";
import "react-international-phone/style.css";
import AlertCard from "@/components/Alert";

function page() {
  const [value, setValue] = useState();
  return (
    <div className="flex justify-center h-screen items-center">
      <Card className="w-[40%]">
        <CardHeader className="space-y-1">
          <CardTitle className="text-2xl">
            Sign up to Ticke<span className="text-purple-600">X</span>
          </CardTitle>
        </CardHeader>
        <CardContent className="grid grid-cols-2 gap-4">
          <div className="grid gap-2">
            <Label htmlFor="firstname">First name</Label>
            <Input id="lastname" type="text" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="lastname">Last name</Label>
            <Input id="lastname" type="text" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="username">Username</Label>
            <Input id="username" type="text" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="email">Email</Label>
            <Input id="email" type="email" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="">Phone number</Label>
            <PhoneInput
              inputClassName=" w-full focus:outline focus:border-purple-500 focus:ring-2 focus:ring-purple-500 "
              className=" grid gap-2 "
              defaultCountry="ma"
              value={value}
            />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="password">Password</Label>
            <Input id="password" type="password" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="confirmPassword">Confirm password</Label>
            <Input id="confirmPassword" type="password" />
          </div>
          <div className="grid gap-2">
            <label className="block">
              <Label htmlFor="confirmPassword">Justification Document</Label>
              <input
                type="file"
                className="block w-full text-sm text-slate-500 mt-2
                                file:mr-4 file:py-2 file:px-4
                                file:rounded-full file:border-0
                                file:text-sm file:font-semibold
                            file:bg-violet-50 file:text-violet-700
                            hover:file:bg-violet-100"
              />
            </label>
          </div>
        </CardContent>
        <CardFooter>
          <Button className="w-full">Sign up</Button>
        </CardFooter>
        <CardContent>
          <span className="bg-background px-2 text-muted-foreground">
            Already have a Ticke
            <span className="text-purple-600 font-bold">X</span> account?
            <Link href="/login" className="underline text-purple-600">
              {" "}
              Log in here
            </Link>
          </span>
        </CardContent>
      </Card>
    </div>
  );
}

export default page;
