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
import { useMutation } from "@tanstack/react-query";
import api from "@/lib/api";
import { toast } from "sonner";

function Action({ id, admin, eventId }: { id: string; admin?: boolean ,eventId?:string|undefined}) {
  const { mutate: mutateUpdate } = useMutation({
    mutationFn: async (id: string) => {
      try {
        const response = await api.put(
          `/authentication-service/api/users/Accept/Organizer/${id}`
        );
        toast.success("Account has been Accepted");
      } catch (error) {
        console.error(error);
        throw error;
      }
    },
  });
  const { mutate: mutateDeleteOrganiser } = useMutation({
    mutationFn: async (id: string) => {
      try {
        const response = await api.delete(
          `/authentication-service/api/users/Accept/Organizer/${id}`
        );
        toast.success("Account has been Accepted");
      } catch (error) {
        console.error(error);
        throw error;
      }
    },
  });
  type EventRoute ={
    id :string;
    eventId:string|undefined;
  }
  const { mutate: mutateDeleteEvent} = useMutation({
    mutationFn: async (data: EventRoute) => {
      try {
        const response = await api.delete(
          `/event-service/Organizer/${data?.id}/Events/${data?.eventId}`
        );
        toast.success("event has been deleted");
      } catch (error) {
        console.error(error);
        throw error;
      }
    },
  });
  const handelAccept = () => {
    mutateUpdate(id);
  };
  const handelDeletOrganiser = () => {
    mutateDeleteOrganiser(id);
  };
  const eventArgs :EventRoute={
    id: id,
    eventId: eventId
  }
  const handleDeleteEvent=()=>{
    mutateDeleteEvent(eventArgs)
  }
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
            // <ConfirmationModel
            //   title="Are you sure To remove This Organiser"
            //   desc="this operation will delete this Organiser from your waitlist "
            //   onConfirm={handelAccept}
            // >

            <>
              <DropdownMenuItem onClick={handelAccept}>Accept</DropdownMenuItem>
              <DropdownMenuItem onClick={handelDeletOrganiser}>
                Delete
              </DropdownMenuItem>
            </>
          ) : (
            <>
              <Link href={`./events/${eventId}`}>
                <DropdownMenuItem>Edit</DropdownMenuItem>
              </Link>
              <DropdownMenuItem onClick={handleDeleteEvent}>Delete</DropdownMenuItem>
            </>
          )}
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  );
}

export default Action;
