import React from "react";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
type Props = {
  title: string;
  desc: string;
  isError: boolean;
};

function AlertCard({ title, desc, isError }: Props) {
  const style = `${isError ? "bg-red-600" : "bg-green-600"} mb-3`;
  return (
    <Alert className={style}>
      <AlertTitle className="text-white">{title}</AlertTitle>
      <AlertDescription className="text-white">{desc}</AlertDescription>
    </Alert>
  );
}

export default AlertCard;
