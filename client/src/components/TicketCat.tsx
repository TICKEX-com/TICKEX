"use client";
import React, { useEffect, useState } from "react";
import { v4 as uuidv4 } from "uuid";
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
import { Button } from "./ui/button";
import { PlusCircle } from "lucide-react";
import Image from "next/image";
import { useDispatch } from "react-redux";
import { setEventInfo } from "@/lib/features/events/eventSlice";
import { eventInfoType } from "@/core/types/event";
import ConfirmationModel from "./ConfirmationModel";
import { useAppSelector } from "@/lib/hooks";

function TicketCat() {
  const dispatch = useDispatch();
  const EventCats = useAppSelector(state=>state.event.eventInfo.categories)
  const [category, setCategory] = useState<string>("");
  const [categories, setCategories] = useState<
    { id: string; name: string; seats: number; price: number }[]
  >(EventCats);

  const addCategory = () => {
    const newCategory = { id: uuidv4(), name: category, seats: 0, price: 0 };
    setCategories((prevCategories) => [...prevCategories, newCategory]);
    setCategory("");
  };

  const deleteCategory = (id: string) => {
    setCategories((prevCategories) =>
      prevCategories.filter((cat) => cat.id !== id)
    );
  };

  const updateStock = (id: string, value: string) => {
    setCategories((prevCategories) =>
      prevCategories.map((cat) =>
        cat.id === id ? { ...cat, stock: parseInt(value) } : cat
      )
    );
  };

  const updatePrice = (id: string, value: string) => {
    setCategories((prevCategories) =>
      prevCategories.map((cat) =>
        cat.id === id ? { ...cat, price: parseFloat(value) } : cat
      )
    );
  };


  
  useEffect(() => {
    const payload: Partial<eventInfoType> = {
      categories: categories.map((cat) => ({
        id: cat.id,
        name: cat.name,
        seats: cat.seats,
        price: cat.price,
      })),
    };
    dispatch(setEventInfo(payload));
  }, [categories, dispatch]);
  return (
    <div>
      <Card x-chunk="dashboard-07-chunk-1">
        <CardHeader>
          <CardTitle>Stock</CardTitle>
          <CardDescription>Please Specify each Ticket Category</CardDescription>
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
              {categories.map((cat) => (
                <TableRow key={cat.id}>
                  <TableCell className="font-semibold">{cat.name}</TableCell>
                  <TableCell>
                    <Label htmlFor={`${cat.id}-stock`} className="sr-only">
                      Stock
                    </Label>
                    <Input
                      id={`${cat.id}-stock`}
                      type="number"
                      defaultValue={cat.seats.toString()}
                      onChange={(e) => updateStock(cat.id, e.target.value)}
                    />
                  </TableCell>
                  <TableCell>
                    <Label htmlFor={`${cat.id}-price`} className="sr-only">
                      Price
                    </Label>
                    <Input
                      id={`${cat.id}-price`}
                      type="number"
                      step="0.01"
                      defaultValue={cat.price.toString()}
                      onChange={(e) => updatePrice(cat.id, e.target.value)}
                    />
                  </TableCell>
                  <TableCell>
                    <ConfirmationModel
                      title="Are you sure To remove This Category"
                      desc="this operation will delete this category from your event"
                      onConfirm={() => deleteCategory(cat.id)}
                    >
                      <div
                        className="cursor-pointer"
                      >
                        <Image
                          width={45}
                          height={28}
                          alt="icon..."
                          src="/svg/delete.svg"
                          loading="lazy"
                        />
                      </div>
                    </ConfirmationModel>
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
                <Button type="submit" onClick={addCategory}>
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
