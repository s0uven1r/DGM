﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Resource.Infrastructure.Persistence;

namespace Resource.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Resource.Domain.Entities.Account.AccountCountTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AccountCountTables");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.AccountHead", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("AccountHeads");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.AccountType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.ClosingBalance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AccountNumber")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(15,4)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ClosingBalances");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.CustomerPayment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("DiscountPercent")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("DueAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDiscountAvail")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPercentDiscount")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("PackageAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CustomerPayments");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("DueAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVehicle")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.TransactionDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("AmountCredit")
                        .HasColumnType("decimal(15,4)");

                    b.Property<decimal>("AmountDebit")
                        .HasColumnType("decimal(15,4)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ReferenceAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionDetails");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.Course", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAdvanceCourse")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RequiredDocuments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseTypeId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.CourseType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CourseTypes");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.Package", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Duration")
                        .HasColumnType("decimal(15,4)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(15,4)");

                    b.Property<int>("TotalDay")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.PackagePromoOffer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(15,4)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndDateNp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasDiscountPercent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PackageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PromoCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartDateNp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("PromoCode")
                        .IsUnique()
                        .HasFilter("[PromoCode] IS NOT NULL");

                    b.ToTable("PackagePromoOffers");
                });

            modelBuilder.Entity("Resource.Domain.Entities.VehicleInventory.VehicleDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Capacity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChasisNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EngineNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ManufacturedYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisterDateEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisterDateNP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("VehicleDetails");
                });

            modelBuilder.Entity("Resource.Domain.Entities.VehicleInventory.VehicleMaintenanceDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisterDateEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisterDateNP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleMaintenaceDetails");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.AccountHead", b =>
                {
                    b.HasOne("Resource.Domain.Entities.Account.AccountType", "AccountType")
                        .WithMany("AccountHeads")
                        .HasForeignKey("AccountTypeId");

                    b.Navigation("AccountType");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.TransactionDetail", b =>
                {
                    b.HasOne("Resource.Domain.Entities.Account.Transaction", "Transaction")
                        .WithMany("TransactionDetails")
                        .HasForeignKey("TransactionId");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.Course", b =>
                {
                    b.HasOne("Resource.Domain.Entities.PackageCourse.CourseType", "CourseType")
                        .WithMany("Courses")
                        .HasForeignKey("CourseTypeId");

                    b.Navigation("CourseType");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.Package", b =>
                {
                    b.HasOne("Resource.Domain.Entities.PackageCourse.Course", "Course")
                        .WithMany("Packages")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.PackagePromoOffer", b =>
                {
                    b.HasOne("Resource.Domain.Entities.PackageCourse.Package", "Package")
                        .WithMany("PackagePromoOffers")
                        .HasForeignKey("PackageId");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Resource.Domain.Entities.VehicleInventory.VehicleMaintenanceDetail", b =>
                {
                    b.HasOne("Resource.Domain.Entities.VehicleInventory.VehicleDetail", "Vehicle")
                        .WithMany("VehicleMaintenance")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.AccountType", b =>
                {
                    b.Navigation("AccountHeads");
                });

            modelBuilder.Entity("Resource.Domain.Entities.Account.Transaction", b =>
                {
                    b.Navigation("TransactionDetails");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.Course", b =>
                {
                    b.Navigation("Packages");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.CourseType", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Resource.Domain.Entities.PackageCourse.Package", b =>
                {
                    b.Navigation("PackagePromoOffers");
                });

            modelBuilder.Entity("Resource.Domain.Entities.VehicleInventory.VehicleDetail", b =>
                {
                    b.Navigation("VehicleMaintenance");
                });
#pragma warning restore 612, 618
        }
    }
}
