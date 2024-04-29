"use client";

import React, { useState } from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "./ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "./ui/table";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog";
import { Label } from "./ui/label";
import { Input } from "./ui/input";
import { TicketCategories } from "@/core/constantes/Event.const";
import { Button } from "./ui/button";
import { PlusCircle } from "lucide-react";
import Image from "next/image";

function TicketCat() {
  const [category, setCategory] = useState<string>("");
  const [categories, setCategories] = useState<string[]>(TicketCategories);
  const handleDeletCat = (unwantedCat: string) => {
    setCategories(categories.filter((cat) => cat !== unwantedCat));
  };
  return (
    <div>
      <Card x-chunk="dashboard-07-chunk-1">
        <CardHeader>
          <CardTitle>Stock</CardTitle>
          <CardDescription>Please Spicify each Ticket Category</CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="w-[100px]">Category</TableHead>
                <TableHead>Stock</TableHead>
                <TableHead>Price</TableHead>
                <TableHead></TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {categories.map((cat, index) => (
                <TableRow key={index}>
                  <TableCell className="font-semibold">{cat}</TableCell>
                  <TableCell>
                    <Label htmlFor={cat} className="sr-only">
                      Stock
                    </Label>
                    <Input id={cat} type="number" defaultValue="100" />
                  </TableCell>
                  <TableCell>
                    <Label htmlFor={cat} className="sr-only">
                      Price
                    </Label>
                    <Input id={cat} type="number" defaultValue="99.99" />
                  </TableCell>
                  <TableCell>
                    <div
                      className="cursor-pointer"
                      onClick={() => handleDeletCat(cat)}
                    >
                      <Image
                        width={45}
                        height={28}
                        alt="icon..."
                        src="/svg/delete.svg"
                        loading="lazy"
                      />
                    </div>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
        <Dialog>
          <CardFooter className="justify-center border-t p-4">
            <DialogTrigger>
              <div className="flex justify-between items-center gap-1">
                <PlusCircle className="h-3.5 w-3.5" />
                <span> Add Category</span>
              </div>
            </DialogTrigger>
          </CardFooter>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Add Category</DialogTitle>
              <DialogDescription>
                Please Add A New Category To Your Event
              </DialogDescription>
            </DialogHeader>

            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="category" className="text-right">
                Category
              </Label>
              <Input
                id="category"
                className="col-span-3"
                onChange={(event) => setCategory(event.target.value)}
                value={category}
              />
            </div>
            <DialogFooter>
              <DialogClose asChild>
                <Button
                  type="submit"
                  onClick={() => {
                    setCategories([...categories, category]);
                    setCategory("");
                  }}
                >
                  Save changes
                </Button>
              </DialogClose>
            </DialogFooter>
          </DialogContent>
        </Dialog>
      </Card>
    </div>
  );
}

export default TicketCat;
