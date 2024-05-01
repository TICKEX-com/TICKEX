"use client";

import React, { useEffect, useState } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";
import { Label } from "./ui/label";
import { categories } from "@/core/constantes/Event.const";
import SelectBox from "./SelectBox";
import { setEventInfo } from "@/lib/features/events/eventSlice";
import { useDispatch } from "react-redux";




function EventCat() {
  const dispatch= useDispatch()
  const [type,setType]=useState({
    type:""
  });
  useEffect(() => {
    dispatch(setEventInfo( type ));
  }, [type, dispatch]);
  return (
    <>
      <Card x-chunk="dashboard-07-chunk-2">
        <CardHeader>
          <CardTitle>Event Type</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid gap-6 sm:grid-cols-3">
            <div className="grid gap-3">
              <Label htmlFor="category">Category</Label>
              <SelectBox
                items={categories}
                element="type"
                title="select your category"
                setDataValue={setType}
              />
            </div>
          </div>
        </CardContent>
      </Card>
    </>
  );
}

export default EventCat
