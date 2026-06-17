using Microsoft.EntityFrameworkCore;
using PaperaX.Domain.Entities;
using System;

namespace PaperaX.Infrastructure.Persistence
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Seed Users (Customers)
            modelBuilder.Entity<User>().HasData(
                new User { Id = -1, FullName = "Aarav Sharma", Company = "N/A", Email = "aarav.sharma@gmail.com", Phone = "+91 98765 43210", Type = "Retail", OrdersCount = 12, TotalSpent = 45000m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -2, FullName = "Vikram Mehta", Company = "Paper Solutions Ltd", Email = "v.mehta@papersolutions.com", Phone = "+91 22 4567 8901", Type = "B2B", OrdersCount = 24, TotalSpent = 850000m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -3, FullName = "Meera Kapoor", Company = "N/A", Email = "meera.k@yahoo.com", Phone = "+91 87654 32109", Type = "Retail", OrdersCount = 5, TotalSpent = 12400m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -4, FullName = "John Doe", Company = "Global Export Inc.", Email = "j.doe@globalexport.com", Phone = "+1 415 555 2671", Type = "B2B", OrdersCount = 8, TotalSpent = 1420000m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -5, FullName = "Rohan Joshi", Company = "N/A", Email = "rohan.j@rediffmail.com", Phone = "+91 76543 21098", Type = "Retail", OrdersCount = 9, TotalSpent = 76500m, Status = "Inactive", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -6, FullName = "Ananya Sen", Company = "Creative Prints", Email = "ananya@creativeprints.in", Phone = "+91 33 2445 6677", Type = "B2B", OrdersCount = 18, TotalSpent = 340000m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -7, FullName = "Aditya Birla", Company = "Birla Publications", Email = "aditya@birlapub.com", Phone = "+91 22 8877 6655", Type = "B2B", OrdersCount = 32, TotalSpent = 1890000m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" },
                new User { Id = -8, FullName = "Pooja Hegde", Company = "N/A", Email = "pooja.h@outlook.com", Phone = "+91 99887 76655", Type = "Retail", OrdersCount = 3, TotalSpent = 8900m, Status = "Active", CreatedAt = DateTime.UtcNow, Role = "Customer", IsEmailVerified = true, PasswordHash = "" }
            );

            // Seed Orders (Portal/Admin Orders)
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = -1, StringId = "#PX-ORDER-4921", DateString = "May 14, 2026", TotalString = "₹54,000", Status = "shipped", EstDelivery = "May 19, 2026", Destination = "Main Corporate Hub, Hassan Industrial Area, KA", ItemSummary = "JK Excel Bond A4 +1 more", ItemsJson = "[{\"name\":\"JK Excel Bond A4 Office Paper\",\"qty\":\"20 Boxes\",\"price\":\"₹44,000\"},{\"name\":\"Double Coated Art Sheet (250GSM)\",\"qty\":\"2 Packs\",\"price\":\"₹10,000\"}]", CreatedAt = DateTime.UtcNow },
                new Order { Id = -2, StringId = "#PX-ORDER-4811", DateString = "Apr 28, 2026", TotalString = "₹32,500", Status = "delivered", EstDelivery = "May 02, 2026", Destination = "Logistics Center, Whitefield, BLR", ItemSummary = "Premium Specialty Kraft Rolls", ItemsJson = "[{\"name\":\"Premium Specialty Kraft Rolls\",\"qty\":\"4 Rolls\",\"price\":\"₹32,500\"}]", CreatedAt = DateTime.UtcNow },
                new Order { Id = -3, StringId = "#PX-101", DateString = "24 Apr, 10:30 AM", CustomerName = "Aarav S.", TotalString = "₹4,500", Payment = "UPI", Status = "Processing", CreatedAt = DateTime.UtcNow },
                new Order { Id = -4, StringId = "#PX-102", DateString = "24 Apr, 09:15 AM", CustomerName = "B2B: Paper Solutions", TotalString = "₹85,000", Payment = "NEFT", Status = "Shipped", CreatedAt = DateTime.UtcNow },
                new Order { Id = -5, StringId = "#PX-103", DateString = "23 Apr, 06:45 PM", CustomerName = "Meera K.", TotalString = "₹1,200", Payment = "COD", Status = "Delivered", CreatedAt = DateTime.UtcNow },
                new Order { Id = -6, StringId = "#PX-104", DateString = "23 Apr, 04:20 PM", CustomerName = "Global Export", TotalString = "₹4,20,000", Payment = "Wire", Status = "Pending", CreatedAt = DateTime.UtcNow },
                new Order { Id = -7, StringId = "#PX-105", DateString = "22 Apr, 11:00 AM", CustomerName = "Rohan J.", TotalString = "₹8,500", Payment = "Card", Status = "Delivered", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
