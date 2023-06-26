using ecommerce.models.Entities;
using ecommerce.models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.repository
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity => base.Set<TEntity>();

        // Entity 

        public DbSet<UsersEntity> Users { get; set; }
    }
}
