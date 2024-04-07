"use client";

import React, { useState } from "react";
import Link from "next/link";
import { icons } from "@/components/ui/icons";
import Image from 'next/image'
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
import { getDownloadURL, ref, uploadBytes } from "firebase/storage";
import { storage } from "@/lib/firebase";
import { v4 } from 'uuid';

function page() {
  const [value, setValue] = useState();
  const [fileUpload, setFileUpload] = useState<FileList | null>(null);
  const [fileUrl, setFileUrl] = useState<string>();
  const uploadFile = () => {
    if (!fileUpload || fileUpload.length === 0) return;
    const file = fileUpload[0];
    const fileType = file.type.split('/')[0];

    const uniqueFileName = `${fileType}s/${file.name}-${v4()}`;
    const fileRef = ref(storage, uniqueFileName);

    uploadBytes(fileRef, file).then((snapshot) => {
      getDownloadURL(snapshot.ref).then((url) => {
        setFileUrl(url);
        console.log(url);

      });
    });
  };

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
              <div className="flex justify-between items-center gap-2">
                <input
                  type="file"
                  onChange={(event) => { setFileUpload(event.target.files) }}
                  className="block w-full text-sm text-slate-500 mt-2
                                file:mr-4 file:py-2 file:px-4
                                file:rounded-full file:border-0
                                file:text-sm file:font-semibold
                            file:bg-violet-50 file:text-violet-700
                            hover:file:bg-violet-100"
                />
                
                <button className="bg-purple-600 text-sm rounded-full  "onClick={uploadFile}>
                  
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
        <CardFooter>
          {fileUrl &&  <Button className="w-full" >Sign up</Button> }
          
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
