﻿// <auto-generated />
using ASPSyncCollegeRoom2018.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ASPSyncCollegeRoom2018.Migrations
{
    [DbContext(typeof(CalendarDBContext))]
    partial class CalendarDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPSyncCollegeRoom2018.Models.ScheduleData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllDay");

                    b.Property<string>("Categorize");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("OwnerId");

                    b.Property<bool>("Recurrence");

                    b.Property<string>("RecurrenceRule");

                    b.Property<string>("RoomId");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.ToTable("ScheduleData");
                });
#pragma warning restore 612, 618
        }
    }
}
