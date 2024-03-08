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
            Log in to Ticke<span className="text-purple-600">X</span>
          </CardTitle>
        </CardHeader>
        <CardContent className="grid gap-4">
        
          <div className="grid gap-2">
            <Label htmlFor="email">Email</Label>
            <Input id="email" type="email" placeholder="tickex@example.com" />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="password">Password</Label>
            <Input id="password" type="password" />
          </div>
          <div className="relative">
            <div className="absolute inset-0 flex items-center">
              <span className="w-full border-t" />
            </div>
            <div className="relative flex justify-center text-xs uppercase">
              <span className="bg-background px-2 text-muted-foreground">
                Or continue with
              </span>
            </div>
          </div>
          <Button variant="outline">
            <icons.google className="mr-2 h-4 w-4" />
            Google
          </Button>
        </CardContent>
        <CardFooter>
          <Button className="w-full">Log in</Button>
        </CardFooter>
        <CardContent>
          <span className="bg-background px-2 text-muted-foreground">
            Need a Ticke
            <span className="text-purple-600 font-bold">X</span> account?  
             <Link href='/register' className="underline text-purple-600"> Sign up here</Link>
          </span>
        </CardContent>
      </Card>
    </div>
  );
}

export default page;
