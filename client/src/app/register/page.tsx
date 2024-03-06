import React from "react";
import Link from 'next/link'
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

function page() {
  return (
    <div className="flex justify-center h-screen items-center">
      <Card className="w-1/4">
        <CardHeader className="space-y-1">
          <CardTitle className="text-2xl">
            Sign up to Ticke<span className="text-purple-600">X</span>
          </CardTitle>
        </CardHeader>
        <CardContent className="grid gap-4">
        <div className="grid gap-2">
            <Label htmlFor="username">Username</Label>
            <Input id="username" type="username" placeholder="tickex" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="email">Email</Label>
            <Input id="email" type="email" placeholder="tickex@example.com" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="password">Password</Label>
            <Input id="password" type="password" />
          </div>
        </CardContent>
        <CardFooter>
          <Button className="w-full">Sign up</Button>
        </CardFooter>
        <CardContent>
          <span className="bg-background px-2 text-muted-foreground">
           Already have a Ticke
            <span className="text-purple-600 font-bold">X</span> account?
             <Link href='/login' className="underline text-purple-600"> Log in here</Link>
          </span>
        </CardContent>
      </Card>
    </div>
  );
}

export default page;