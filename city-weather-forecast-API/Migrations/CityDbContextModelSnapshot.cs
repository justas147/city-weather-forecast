﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using city_weather_forecast_API.Database;

namespace city_weather_forecast_API.Migrations
{
    [DbContext(typeof(CityDbContext))]
    partial class CityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("city_weather_forecast_API.Models.City", b =>
                {
                    b.Property<string>("PlaceCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PlaceCode");

                    b.ToTable("CityItem");
                });
#pragma warning restore 612, 618
        }
    }
}
