﻿// <auto-generated />
using CountingApp.Server.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CountingApp.Server.Migrations
{
    [DbContext(typeof(CountingAppDbContext))]
    partial class CountingAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CountingApp.Server.DbModels.TransactionDbModel", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Timestamp");

                    b.Property<decimal>("TotalAmount");

                    b.Property<string>("TransactionData");

                    b.Property<string>("TransactionType");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("TransactionId");

                    b.ToTable("TransactionDbModels");
                });
#pragma warning restore 612, 618
        }
    }
}
