"use client"
import DataTable from "@/components/DataTable";
import TopNav from "@/components/TopNav";
import UsersTable from "@/components/UsersTable";
import { userColumns, userData } from "@/core/constantes/Table.const";
import api from "@/lib/api";
import { useQuery } from "@tanstack/react-query";
import React from "react";

export default function page() {
  const fetchOrganiser = async ()=> {
    try {
      const res = await api.get("authentication-service/api/users/organizers")
      console.log(res);
      
    } catch (error) {
      throw error;
    }
  }

  const{data,error,isLoading}=useQuery({queryKey:["organisers"],queryFn: fetchOrganiser});

  return (
    <div>
      <TopNav />
      <section className="flex flex-col sm:gap-4 sm:py-4 sm:pl-14">
        <h1 className="text-3xl font-bold text-[#8444ec]">
          Welcome Back Admin
        </h1>
        <p className="text-[#8444ec]">Please Accept Our New Sellers </p>
        <UsersTable
          title="User List"
          desc="List of registered users"
          columns={userColumns}
          data={userData}
          isAdmin={true}
        />
      </section>
    </div>
  );
}
