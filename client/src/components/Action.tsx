import React from "react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { MoreHorizontal } from "lucide-react";
import Link from "next/link";
import ConfirmationModel from "./ConfirmationModel";

function Action({ id, admin }: { id: string; admin?: boolean }) {
  const handelDelete = () => {
    console.log("has been deleted");
  };
  return (
    <div>
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Button aria-haspopup="true" size="icon" variant="ghost">
            <MoreHorizontal className="h-4 w-4" />
            <span className="sr-only">Toggle menu</span>
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
          <DropdownMenuLabel>Actions</DropdownMenuLabel>
          {admin ? (
            <ConfirmationModel
              title="Are you sure To remove This Organiser"
              desc="this operation will delete this Organiser from your waitlist "
              onConfirm={()=>handelDelete}
            >
              <DropdownMenuItem>Accept</DropdownMenuItem>
            </ConfirmationModel>
          ) : (
            <Link href={`./events/${id}`}>
              <DropdownMenuItem>Edit</DropdownMenuItem>
            </Link>
          )}

          <DropdownMenuItem>Delete</DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  );
}

export default Action;
