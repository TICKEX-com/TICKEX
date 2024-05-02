"use client";
import React, { useEffect } from "react";
import Link from "next/link";
import { useDispatch } from "react-redux";
import { icons } from "@/components/ui/icons";
import { useLoginMutation } from "@/lib/features/auth/userApiSlice";
import { setCredentials } from "@/lib/features/auth/authSlice";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import AlertCard from "@/components/Alert";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { toast } from "sonner";
import { useAppSelector } from "@/lib/hooks";


function page() {
  const router = useRouter();
  const dispatch = useDispatch();
  const [login, { isLoading, isError, error }] = useLoginMutation();
  const userData = useAppSelector((state) => state.auth.userInfo); // Move useSelector outside of onSubmit

  useEffect(() => {
    console.log(userData);
    
    if (userData) {
      const role = userData?.role;
      console.log(role);
      
      if (role === "CLIENT") {
        router.push("/");
      } else if (role === "ORGANIZER") {
        router.push("/dashboard");
      } else {
        router.push("/");
      }
    }
  }, [userData])
  const onSubmit = async (formData: FormData) => {
    const user = {
      username: formData.get("username") as string,
      password: formData.get("password") as string,
    };
    try {
      const res = await login(user).unwrap();
      const userInfo = res.result.user;
      
      dispatch(setCredentials({ ...userInfo }));
      toast.success("you logged in with success");
    } catch (error) {
      console.log(error?.data?.message);
    }
  };

  return (
    <div className="flex justify-center h-screen items-center">
      <Card className="w-1/4 p-6 ">
        <form action={onSubmit}>
          <CardHeader className="space-y-1">
            <CardTitle className="text-2xl">
              Log in to Ticke<span className="text-purple-600">X</span>
            </CardTitle>
          </CardHeader>
          <CardContent className="grid gap-4">
            <div className="grid gap-2">
              <Label htmlFor="username">User name</Label>
              <Input id="username" type="username" name="username" />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="password">Password</Label>
              <Input id="password" name="password" type="password" />
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
            <Button disabled={isLoading} className="w-full">
              {isLoading ? "Loading..." : "Log in"}
            </Button>
          </CardFooter>
        </form>
        <CardContent>
          <span className="bg-background px-2 text-muted-foreground">
            Need a Ticke
            <span className="text-purple-600 font-bold">X</span> account?
            <Link href="/register" className=" text-purple-600">
              {" "}
              Sign up here
            </Link>
          </span>
          {isError && (
            <AlertCard
              title="Ops Something went wrong..."
              desc={error?.data?.message}
              isError={isError}
            />
          )}
        </CardContent>
      </Card>
    </div>
  );
}

export default page;
