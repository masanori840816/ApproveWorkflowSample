﻿// <auto-generated />
using System;
using ApprovementWorkflowSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApprovementWorkflowSample.Migrations
{
    [DbContext(typeof(ApprovementWorkflowContext))]
    partial class ApprovementWorkflowContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ApprovementWorkflowSample.Applications.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mail");

                    b.Property<DateTime>("LastUpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_update_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Organization")
                        .HasColumnType("text")
                        .HasColumnName("organization");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.Approver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("date")
                        .HasColumnName("approved_date");

                    b.Property<int>("ApproverGroupId")
                        .HasColumnType("integer")
                        .HasColumnName("approver_group_id");

                    b.Property<int>("ApproverRoleId")
                        .HasColumnType("integer")
                        .HasColumnName("approver_role_id");

                    b.Property<DateTime>("LastUpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_update_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("ApproverGroupId");

                    b.HasIndex("ApproverRoleId");

                    b.ToTable("Approver");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.ApproverGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer")
                        .HasColumnName("workflow_id");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowId");

                    b.ToTable("ApproverGroup");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.ApproverRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("ApproverRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Author"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Checker"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Approver"
                        });
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.Workflow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("date")
                        .HasColumnName("approved_date");

                    b.Property<DateTime>("LastUpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_update_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("WorkflowTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("workflow_type_id");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowTypeId");

                    b.ToTable("Workflow");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.WorkflowType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("WorkflowType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "WorkflowTypeA"
                        },
                        new
                        {
                            Id = 2,
                            Name = "WorkflowTypeA"
                        });
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.Approver", b =>
                {
                    b.HasOne("ApprovementWorkflowSample.Approvements.ApproverGroup", "ApproverGroup")
                        .WithMany("Approvers")
                        .HasForeignKey("ApproverGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApprovementWorkflowSample.Approvements.ApproverRole", "ApproverRole")
                        .WithMany("Approvers")
                        .HasForeignKey("ApproverRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApproverGroup");

                    b.Navigation("ApproverRole");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.ApproverGroup", b =>
                {
                    b.HasOne("ApprovementWorkflowSample.Approvements.Workflow", "Workflow")
                        .WithMany("ApproverGroups")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.Workflow", b =>
                {
                    b.HasOne("ApprovementWorkflowSample.Approvements.WorkflowType", "WorkflowType")
                        .WithMany("Workflows")
                        .HasForeignKey("WorkflowTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkflowType");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.ApproverGroup", b =>
                {
                    b.Navigation("Approvers");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.ApproverRole", b =>
                {
                    b.Navigation("Approvers");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.Workflow", b =>
                {
                    b.Navigation("ApproverGroups");
                });

            modelBuilder.Entity("ApprovementWorkflowSample.Approvements.WorkflowType", b =>
                {
                    b.Navigation("Workflows");
                });
#pragma warning restore 612, 618
        }
    }
}
