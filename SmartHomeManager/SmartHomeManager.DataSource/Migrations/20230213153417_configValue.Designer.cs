﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHomeManager.DataSource;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230213153417_configValue")]
    partial class configValue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("DeviceProfile", b =>
                {
                    b.Property<Guid>("DevicesDeviceId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfilesProfileId")
                        .HasColumnType("TEXT");

                    b.HasKey("DevicesDeviceId", "ProfilesProfileId");

                    b.HasIndex("ProfilesProfileId");

                    b.ToTable("DeviceProfile");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Timezone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProfileId");

                    b.HasIndex("AccountId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.Device", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceBrand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceTypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceId");

                    b.HasIndex("AccountId");

                    b.HasIndex("DeviceTypeName");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfiguration", b =>
                {
                    b.Property<string>("ConfigurationKey")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigurationValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeviceBrand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConfigurationKey", "DeviceId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ConfigurationKey", "DeviceBrand", "DeviceModel");

                    b.ToTable("DeviceConfigurations");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfigurationLookUp", b =>
                {
                    b.Property<string>("ConfigurationKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceBrand")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueMeaning")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConfigurationKey", "DeviceBrand", "DeviceModel");

                    b.ToTable("DeviceConfigurationLookUps");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceType", b =>
                {
                    b.Property<string>("DeviceTypeName")
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceTypeName");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DeviceLog", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceActivity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceEnergyUsage")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DeviceState")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("LogId");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceLogs");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.NotificationDomain.Entities.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NotificationMessage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("TEXT");

                    b.HasKey("NotificationId");

                    b.HasIndex("AccountId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.DeviceCoordinate", b =>
                {
                    b.Property<Guid>("DeviceCoordinateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XCoordinate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YCoordinate")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceCoordinateId");

                    b.HasIndex("DeviceId")
                        .IsUnique();

                    b.ToTable("DeviceCoordinates");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RoomId");

                    b.HasIndex("AccountId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.RoomCoordinate", b =>
                {
                    b.Property<Guid>("RoomCoordinateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XCoordinate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YCoordinate")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomCoordinateId");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("RoomCoordinates");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Rule", b =>
                {
                    b.Property<Guid>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("APIKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionTrigger")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationKey")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigurationValue")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScheduleName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("RuleId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ScenarioId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Scenario", b =>
                {
                    b.Property<Guid>("ScenarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RuleList")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ScenarioName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isActive")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScenarioId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Troubleshooter", b =>
                {
                    b.Property<Guid>("TroubleshooterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Recommendation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TroubleshooterId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Troubleshooters");
                });

            modelBuilder.Entity("DeviceProfile", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", null)
                        .WithMany()
                        .HasForeignKey("DevicesDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Profile", null)
                        .WithMany()
                        .HasForeignKey("ProfilesProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Profile", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.Device", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceType", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.RoomDomain.Entities.Room", "Room")
                        .WithOne("Device")
                        .HasForeignKey("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "RoomId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Account");

                    b.Navigation("DeviceType");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfiguration", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfigurationLookUp", "DeviceConfigurationLookUp")
                        .WithMany()
                        .HasForeignKey("ConfigurationKey", "DeviceBrand", "DeviceModel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("DeviceConfigurationLookUp");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DeviceLog", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.NotificationDomain.Entities.Notification", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.DeviceCoordinate", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithOne("DeviceCoordinate")
                        .HasForeignKey("SmartHomeManager.Domain.RoomDomain.Entities.DeviceCoordinate", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.Room", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.RoomCoordinate", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.RoomDomain.Entities.Room", "Room")
                        .WithOne("RoomCoordinate")
                        .HasForeignKey("SmartHomeManager.Domain.RoomDomain.Entities.RoomCoordinate", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Rule", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.SceneDomain.Entities.Scenario", "Scenario")
                        .WithMany()
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Scenario");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Scenario", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Profile", "Profile")
                        .WithMany("Scenarios")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Troubleshooter", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Profile", b =>
                {
                    b.Navigation("Scenarios");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.Device", b =>
                {
                    b.Navigation("DeviceCoordinate")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.Room", b =>
                {
                    b.Navigation("Device")
                        .IsRequired();

                    b.Navigation("RoomCoordinate")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
