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

function UsersTable({
  title,
  desc,
  columns,
  data,
  isAdmin,
}: {
  title: string;
  desc: string;
  columns: { key: string; label: string }[];
  data: any[]; // Change the type according to your data structure
  isAdmin: boolean;
}) {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [itemsPerPage, setItemsPerPage] = useState<number>(5);

  const lastItemIndex = currentPage * itemsPerPage;
  const firstItemIndex = lastItemIndex - itemsPerPage;
  const currentItems = data.slice(firstItemIndex, lastItemIndex);

  return (
    <main className="grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8">
      <Tabs defaultValue="all">
        <TabsContent value="all">
          <Card x-chunk="dashboard-06-chunk-0">
            <CardHeader>
              <CardTitle>{title}</CardTitle>
              <CardDescription>{desc}</CardDescription>
            </CardHeader>
            <CardContent>
              <Table>
                <TableHeader>
                  <TableRow>
                    {columns.map((column) => (
                      <TableHead key={column.key}>{column.label}</TableHead>
                    ))}
                    <TableHead>
                      <span className="sr-only">Actions</span>
                    </TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {currentItems.map((item, index) => (
                    <TableRow key={index}>
                      <TableCell>{index + 1}</TableCell>
                      <TableCell>{item.username}</TableCell>
                      <TableCell>{item.email}</TableCell>
                      {item.isActive ? (
                        <TableCell className="text-green-500">Active</TableCell>
                      ) : (
                        <TableCell className="text-red-500">
                          {" "}
                          Not Active
                        </TableCell>
                      )}

                      <TableCell>
                        <a
                          className="text-[#8444ec] cursor-pointer "
                          href={item.certificate}
                          target="#"
                        >
                          Justification
                        </a>
                      </TableCell>

                      {!item.isActive && (
                        <TableCell>
                          <Action id={item.id} admin={isAdmin} />
                        </TableCell>
                      )}
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </CardContent>
            <CardFooter>
              <PaginationSection
                totalItems={data.length}
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

export default UsersTable;
