using Microsoft.EntityFrameworkCore;
using stock_management_system.Models;

namespace stock_management_system.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //CheckoutList
            builder.Entity<CheckoutList>()
                .HasKey(ckl => new { ckl.CheckoutId, ckl.ProductSku });

            // Supplier
            builder.Entity<Supplier>().HasIndex(s => s.Email).IsUnique();
            builder.Entity<Supplier>().HasCheckConstraint("CK_Supplier_PhoneNumber_Min_Value", "[Phone] >= 0");
            builder.Entity<Supplier>().HasCheckConstraint("CK_Supplier_PhoneNumber_Max_Value", "[Phone] <= 9999999999");

            // User
            builder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            builder.Entity<Employee>().HasCheckConstraint("CK_Employee_PhoneNumber_Min_Value", "[Phone] >= 0");
            builder.Entity<Employee>().HasCheckConstraint("CK_Employee_PhoneNumber_Max_Value", "[Phone] <= 9999999999");

            // adding values to TABLES

            // Employee
            builder.Entity<Employee>().HasData(new Employee { Id = 1, Name = "Juan", Lastname = "Tremols", Email = "2018-0133@est.itsc.edu.do", Phone = 8097057474 });
            builder.Entity<Employee>().HasData(new Employee { Id = 2, Name = "Pedro", Lastname = "Reyes", Email = "pedroreyes@gmail.com", Phone = 8097057424 });
            builder.Entity<Employee>().HasData(new Employee { Id = 3, Name = "Gabriel", Lastname = "Wakanda", Email = "gabrielwakanda@gmail.com", Phone = 8093057474 });

            // Category
            builder.Entity<Category>().HasData(new Category { Id = 1, Name = "Insumos", Description = "Productos para el consumo de la gente"});
            builder.Entity<Category>().HasData(new Category { Id = 2, Name = "Limpieza", Description = "Productos para la higiene de la casa" });
            builder.Entity<Category>().HasData(new Category { Id = 3, Name = "Higiene", Description = "Para el cuidado de la higiene personal" });

            //Supplier
            builder.Entity<Supplier>().HasData(new Supplier { Id = 1, Name = "Compañia de higiene personal colgaishon", Email = "colgaishon@est.itsc.edu.do", Phone = 8097057424 });
            builder.Entity<Supplier>().HasData(new Supplier { Id = 2, Name = "Compañia de insumos papita y bebida", Email = "fritolay@est.itsc.edu.do", Phone = 8094677424 });

            //Product
            builder.Entity<Product>().HasData(new Product { Sku = "IS000001", Name = "Papita fritolay papa", Description = "Una paquete de papitas de papa pequeñas", CategoryId = 1, AlertQuantity = 5, Units = 25, SellingPrice = 250 });
            builder.Entity<Product>().HasData(new Product { Sku = "IS000002", Name = "Papita doritos original", Description = "Una paquete de papitas de dorito original pequeñas", CategoryId = 1, AlertQuantity = 5, Units = 25, SellingPrice = 350 });
            builder.Entity<Product>().HasData(new Product { Sku = "IS000003", Name = "Pasta dental colgate pequeña", Description = "Una paquete de pasta dental colgate en tamaño pequeñas", CategoryId = 3, AlertQuantity = 5, Units = 10, SellingPrice = 500 });

            //Stock
            builder.Entity<Stock>().HasData(new Stock { Id = 1, Quantity = 10, ProductSku = "IS000001" });
            builder.Entity<Stock>().HasData(new Stock { Id = 2, Quantity = 15, ProductSku = "IS000002" });
            builder.Entity<Stock>().HasData(new Stock { Id = 3, Quantity = 5, ProductSku = "IS000003" });






        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Checkin> Checkin { get; set; }
        public DbSet<CheckinList> CheckinLists { get; set; }
        public DbSet<Checkout> Checkout { get; set; }
        public DbSet<CheckoutList> CheckoutLists { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
