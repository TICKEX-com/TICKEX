"use client";

import React, { useState } from "react";
import Image from "next/image";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Tabs, TabsContent } from "@/components/ui/tabs";

import { Badge } from "@/components/ui/badge";

import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

import { eventData } from "@/core/constantes/Table.const";
import Action from "./Action";
import PaginationSection from "./PaginationSection";
import { eventDataType } from "@/core/types/tableData";
import { useAppSelector } from "@/lib/hooks";
type Data = {
  data: eventDataType[];
};

function DataTable({ data }: Data) {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [itemsPerPage, setItemsPerPage] = useState<number>(5);

  const lastItemIndex = currentPage * itemsPerPage;
  const firstItemIndex = lastItemIndex - itemsPerPage;
  const currentItems = data.slice(firstItemIndex, lastItemIndex);
  const organiserId = useAppSelector(state=>state.persistedReducer.auth.userInfo?.id)

  return (
    <main className="grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8">
      <Tabs defaultValue="all">
        <TabsContent value="all">
          <Card x-chunk="dashboard-06-chunk-0">
            <CardHeader>
              <CardTitle>Events</CardTitle>
              <CardDescription>
                Manage your Events and view their sales performance.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead className="hidden w-[100px] sm:table-cell">
                      <span className="sr-only">Image</span>
                    </TableHead>
                    <TableHead>Title</TableHead>
                    <TableHead>Category</TableHead>
                    <TableHead className="hidden md:table-cell">
                      Avg Price
                    </TableHead>
                    <TableHead className="hidden md:table-cell">
                      Created at
                    </TableHead>
                    <TableHead>
                      <span className="sr-only">Actions</span>
                    </TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {currentItems.map((event, index) => (
                    <TableRow key={index}>
                      <TableCell className="hidden sm:table-cell">
                        <Image
                          alt="Product image"
                          className="aspect-square rounded-md object-cover"
                          height="64"
                          src={event.poster}
                          width="64"
                          loading="lazy"
                        />
                      </TableCell>
                      <TableCell className="font-medium">
                        {event.title}
                      </TableCell>
                      <TableCell>
                        <Badge variant="outline">{event.eventType}</Badge>
                      </TableCell>
                      <TableCell className="hidden md:table-cell">
                        $ {event.minPrice}
                      </TableCell>
                      <TableCell className="hidden md:table-cell">
                        {event.eventDate.split("T")[0]}
                      </TableCell>
                      <TableCell>
                        <Action id={organiserId} eventId={event.id}  />
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </CardContent>
            <CardFooter>
              <PaginationSection
                totalItems={eventData.length}
                itemsPerPage={itemsPerPage}
                currentPage={currentPage}
                setCurrentPage={setCurrentPage}
              />
            </CardFooter>
          </Card>
        </TabsContent>
      </Tabs>
    </main>
  );
}

export default DataTable;
