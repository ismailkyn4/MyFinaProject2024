using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //Bu metot hangi veritabanıyla ilgili olduğunu belirttiğimiz yer
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
            //optionsBuilder.UseSqlServer(@"Server=ISMAILKOYUN\SQLEXPRESS;Database=Northwind;Trusted_Connection=true;Trust Server Certificate=True");
            //SqlServera nasıl bağlanacağımızı belirttiiğimiz yer. @ koyarak ters slaş "\" işaretini algılatmak istediğimiz için yazarız.
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
