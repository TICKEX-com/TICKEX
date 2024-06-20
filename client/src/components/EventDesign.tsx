"use client";

import React, { useEffect, useState } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";
import { Label } from "./ui/label";
import { categories } from "@/core/constantes/Event.const";
import SelectBox from "./SelectBox";
import { setDesign, setEventInfo } from "@/lib/features/events/eventSlice";
import { useDispatch } from "react-redux";
import { useAppSelector } from "@/lib/hooks";
import api from "@/lib/api";
import { useQuery } from "@tanstack/react-query";
import SelectDesigns from "./SelectDesigns";

function EventDesign() {
  const dispatch = useDispatch();
  const [eventDesign, setEventDesign] = useState(0);
  const fetchDesigns = async () => {
    try {
      const res = await api.get(`event-service/MyDesigns`);
      console.log(res.data);
      return res?.data;
    } catch (error) {
      throw error;
    }
  };

  const { data, error, isLoading } = useQuery({
    queryKey: ["events"],
    queryFn: fetchDesigns,
  });
  useEffect(() => {
    dispatch(setDesign(eventDesign));
  }, [eventDesign, dispatch]);
  const designId = useAppSelector((state) => state.event.eventInfo.designId);
  return (
    <>
      <Card x-chunk="dashboard-07-chunk-2">
        <CardHeader>
          <CardTitle>Event Design</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid gap-6 sm:grid-cols-3">
            <div className="grid gap-3">
              <Label htmlFor="category">
                Please choose your event's design
              </Label>
              {data && (
                <SelectDesigns
                  items={data}
                  element="Chose your design"
                  title="Select your design"
                  setDataValue={setEventDesign}
                  value={designId}
                />
              )}
            </div>
          </div>
        </CardContent>
      </Card>
    </>
  );
}

export default EventDesign;
