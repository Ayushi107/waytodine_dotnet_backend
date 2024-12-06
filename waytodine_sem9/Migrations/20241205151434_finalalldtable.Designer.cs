﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using waytodine_sem9.Data;

#nullable disable

namespace waytodine_sem9.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241205151434_finalalldtable")]
    partial class finalalldtable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("waytodine_sem9.Models.admin.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CategoryId");

                    b.ToTable("categories", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.DeliveryPerson", b =>
                {
                    b.Property<int>("DeliveryPersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DeliveryPersonId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DriverEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DriverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DrivingLicenseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("LicenseDocument")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DeliveryPersonId");

                    b.HasIndex("UserId");

                    b.ToTable("DeliveryPerson");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FeedbackId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("feedback", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.MenuItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("item_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ItemId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<int>("IsVeg")
                        .HasColumnType("integer")
                        .HasColumnName("is_veg");

                    b.Property<string>("ItemImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("item_image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer")
                        .HasColumnName("restaurant_id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("menu_item", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int?>("DeliveryPersonId")
                        .HasColumnType("integer")
                        .HasColumnName("delivery_person_id");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("numeric")
                        .HasColumnName("discount");

                    b.Property<bool>("IsAccept")
                        .HasColumnType("boolean")
                        .HasColumnName("isAccept");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer")
                        .HasColumnName("order_status");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer")
                        .HasColumnName("payment_status");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer")
                        .HasColumnName("restaurant_id");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryPersonId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("orders", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("restaurant_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RestaurantId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("phone_number");

                    b.Property<string>("RestaurantDocument")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("restaurant_document");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("RestaurantId");

                    b.ToTable("restaurants", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.RestaurantDetails", b =>
                {
                    b.Property<int>("RestaurantDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("restaurant_details_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RestaurantDetailsId"));

                    b.Property<string>("BannerImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("banner_image");

                    b.Property<double?>("CurrentOfferDiscountRate")
                        .HasColumnType("double precision")
                        .HasColumnName("current_offer_discount_rate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Mission")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mission");

                    b.Property<string>("OpeningHoursWeekdays")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("opening_hours_weekdays");

                    b.Property<string>("OpeningHoursWeekends")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("opening_hours_weekends");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer")
                        .HasColumnName("restaurant_id");

                    b.Property<string>("Specialities")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("specialities");

                    b.HasKey("RestaurantDetailsId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("restaurant_details", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReviewId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int?>("DeliveryPersonId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReviewId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryPersonId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Tracking", b =>
                {
                    b.Property<int>("TrackingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TrackingId"));

                    b.Property<decimal>("CurrentLatitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("CurrentLongitude")
                        .HasColumnType("numeric");

                    b.Property<int>("DeliveryPersonId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("TrackingId");

                    b.HasIndex("DeliveryPersonId");

                    b.ToTable("Tracking");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("ProfilePic")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("profile_pic");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("UserId");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.DeliveryPerson", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Feedback", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.User", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("waytodine_sem9.Models.admin.Restaurant", "Restaurant")
                        .WithMany("Feedbacks")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.MenuItem", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.Category", "Category")
                        .WithMany("MenuItems")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("waytodine_sem9.Models.admin.Restaurant", "Restaurant")
                        .WithMany("MenuItems")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Order", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("waytodine_sem9.Models.admin.DeliveryPerson", "DeliveryPerson")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryPersonId");

                    b.HasOne("waytodine_sem9.Models.admin.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("DeliveryPerson");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.RestaurantDetails", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.Restaurant", "Restaurant")
                        .WithMany("RestaurantDetails")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Review", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("waytodine_sem9.Models.admin.DeliveryPerson", "DeliveryPerson")
                        .WithMany("Reviews")
                        .HasForeignKey("DeliveryPersonId");

                    b.HasOne("waytodine_sem9.Models.admin.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("DeliveryPerson");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Tracking", b =>
                {
                    b.HasOne("waytodine_sem9.Models.admin.DeliveryPerson", "DeliveryPerson")
                        .WithMany("Trackings")
                        .HasForeignKey("DeliveryPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryPerson");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Category", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.DeliveryPerson", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("Trackings");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.Restaurant", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("MenuItems");

                    b.Navigation("RestaurantDetails");
                });

            modelBuilder.Entity("waytodine_sem9.Models.admin.User", b =>
                {
                    b.Navigation("Feedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
