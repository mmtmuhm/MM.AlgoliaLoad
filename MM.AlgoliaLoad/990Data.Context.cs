﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MM.AlgoliaLoad
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mmxEntities : DbContext
    {
        public mmxEntities()
            : base("name=mmxEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ntee_outcomes_lookup> ntee_outcomes_lookup { get; set; }
        public DbSet<mmx_map_data> mmx_map_data { get; set; }
    }
}
