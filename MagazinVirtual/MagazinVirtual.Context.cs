﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MagazinVirtual
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MagazinVirtualEntities : DbContext
    {
        public MagazinVirtualEntities()
            : base("name=MagazinVirtualEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SpecialTags> SpecialTags { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersProducts> OrdersProducts { get; set; }
    
        public virtual int USP_CreateFK(string fKName, string childTableName, string childColumnName, string parentTableName, string parentColumnName, Nullable<bool> cascadeDelete)
        {
            var fKNameParameter = fKName != null ?
                new ObjectParameter("FKName", fKName) :
                new ObjectParameter("FKName", typeof(string));
    
            var childTableNameParameter = childTableName != null ?
                new ObjectParameter("ChildTableName", childTableName) :
                new ObjectParameter("ChildTableName", typeof(string));
    
            var childColumnNameParameter = childColumnName != null ?
                new ObjectParameter("ChildColumnName", childColumnName) :
                new ObjectParameter("ChildColumnName", typeof(string));
    
            var parentTableNameParameter = parentTableName != null ?
                new ObjectParameter("ParentTableName", parentTableName) :
                new ObjectParameter("ParentTableName", typeof(string));
    
            var parentColumnNameParameter = parentColumnName != null ?
                new ObjectParameter("ParentColumnName", parentColumnName) :
                new ObjectParameter("ParentColumnName", typeof(string));
    
            var cascadeDeleteParameter = cascadeDelete.HasValue ?
                new ObjectParameter("CascadeDelete", cascadeDelete) :
                new ObjectParameter("CascadeDelete", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("USP_CreateFK", fKNameParameter, childTableNameParameter, childColumnNameParameter, parentTableNameParameter, parentColumnNameParameter, cascadeDeleteParameter);
        }
    }
}