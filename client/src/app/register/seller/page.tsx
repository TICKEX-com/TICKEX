"use client";

import React, { useState } from "react";
import Link from "next/link";
import Image from "next/image";
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
import { useRouter } from "next/navigation";
import { useMutation } from "@tanstack/react-query";
import api from "@/lib/api";
import { toast } from "sonner";
import { User } from "@/core/types/authentication.types";
import { AxiosError } from "axios";
import { uploadFile } from "@/lib/fileUpload";

function page() {
  const [value] = useState();
  const [docUpload, setDocUpload] = useState<FileList | null>(null);
  const [docUrl, setDocUrl] = useState<string>();
  const [profileUpload, setProfileUpload] = useState<FileList | null>(null);
  const [profileUrl, setProfileUrl] = useState<string>();

  const router = useRouter();
  const { mutate, isPending, isError, error } = useMutation({
    mutationFn: async (user: User) => {
      try {
        const response = await api.post(
          "/authentication-service/api/auth/Register/Organizer",
          user
        );
        toast.success("Account has been created");
        router.push("/login");
      } catch (error) {
        console.error("Error while sending data:", error);
        throw error;
      }
    },
  });
  const onSubmit = (data: FormData) => {
    const user: User = {
      username: data.get("username") as string,
      email: data.get("email") as string,
      firstname: data.get("firstname") as string,
      lastname: data.get("lastname") as string,
      organizationName: data.get("organizationName") as string,
      phoneNumber: data.get("phoneNumber") as string,
      ville: data.get("city") as string,
      currency: data.get("currency") as string,
      password: data.get("password") as string,
      confirmPassword: data.get("confirmPassword") as string,
    };
    const userData = {
      ...user,
      certificate: docUrl,
      profileImage: profileUrl,
    };
    mutate(userData);
  };
  return (
    <div className="flex justify-center h-screen items-center">
      <Card className="w-[40%]">
        <form action={onSubmit}>
          <CardHeader className="space-y-1">
            <CardTitle className="text-2xl">
              Sign up to Ticke<span className="text-purple-600">X</span>
            </CardTitle>
          </CardHeader>

          <CardContent className="grid grid-cols-2 gap-4">
            <div className="grid gap-2">
              <Label htmlFor="firstname">First name</Label>
              <Input id="lastname" type="text" name="firstname" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="lastname">Last name</Label>
              <Input id="lastname" type="text" name="lastname" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="username">Username</Label>
              <Input id="username" type="text" name="username" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="username">Organisation Name</Label>
              <Input id="username" type="text" name="organizationName" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="city">City</Label>
              <Input id="city" type="text" name="city" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="currency">
                Currency 
              </Label>
              <Input id="currency" type="text" name="currency" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="email">Email</Label>
              <Input id="email" type="email" name="email" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="">Phone number</Label>
              <PhoneInput
                inputClassName=" w-full focus:outline focus:border-purple-500 focus:ring-2 focus:ring-purple-500 "
                className=" grid gap-2 "
                defaultCountry="ma"
                value={value}
                name="phoneNumber"
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="password">Password</Label>
              <Input id="password" type="password" name="password" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="confirmPassword">Confirm password</Label>
              <Input
                id="confirmPassword"
                type="password"
                name="confirmPassword"
              />
            </div>
          </CardContent>
          <CardFooter>
            {(docUrl && profileUrl) && (
              <Button disabled={isPending} type="submit" className="w-full">
                {isPending ? "Loading..." : "Sign up"}
              </Button>
            )}
          </CardFooter>
        </form>
        <CardContent>
        <div className="grid grid-cols-2 gap-2">
          <label className="block">
            <Label htmlFor="confirmPassword">Justification Document</Label>
            <div className="flex justify-between items-center gap-2">
              <input
                type="file"
                onChange={(event) => {
                  setDocUpload(event.target.files);
                }}
                className="block w-full text-sm text-slate-500 mt-2
                                file:mr-4 file:py-2 file:px-4
                                file:rounded-full file:border-0
                                file:text-sm file:font-semibold
                            file:bg-violet-50 file:text-violet-700
                            hover:file:bg-violet-100"
              />

              <button
                className="bg-purple-600 text-sm rounded-full  "
                onClick={() => uploadFile(docUpload, setDocUrl)}
              >
                <Image
                  src="/svg/upload.svg"
                  width={30}
                  height={30}
                  alt="upload"
                />
              </button>
            </div>
          </label>
          <label className="block">
            <Label htmlFor="confirmPassword">Profile Image</Label>
            <div className="flex justify-between items-center gap-2">
              <input
                type="file"
                onChange={(event) => {
                  setProfileUpload(event.target.files);
                }}
                className="block w-full text-sm text-slate-500 mt-2
                                file:mr-4 file:py-2 file:px-4
                                file:rounded-full file:border-0
                                file:text-sm file:font-semibold
                            file:bg-violet-50 file:text-violet-700
                            hover:file:bg-violet-100"
              />

              <button
                className="bg-purple-600 text-sm rounded-full  "
                onClick={() => uploadFile(profileUpload, setProfileUrl)}
              >
                <Image
                  src="/svg/upload.svg"
                  width={30}
                  height={30}
                  alt="upload"
                />
              </button>
            </div>
          </label>
        </div>
        </CardContent>
        <CardContent>
          {isError && error instanceof AxiosError && error.response && (
            <AlertCard
              title="Ops Something went wrong..."
              desc={error.response.data.message}
              isError={isError}
            />
          )}
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
