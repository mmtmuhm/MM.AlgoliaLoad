//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class user
    {
        public user()
        {
            this.powerbi_links = new HashSet<powerbi_links>();
        }
    
        public long userid { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string password2 { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string organization { get; set; }
        public string organizationtype { get; set; }
        public string organizationrole { get; set; }
        public string areasofinterest { get; set; }
        public int acclvl { get; set; }
        public long user_role { get; set; }
        public long usergroupid { get; set; }
        public long created { get; set; }
        public string hash { get; set; }
        public Nullable<sbyte> isFunder { get; set; }
    
        public virtual ICollection<powerbi_links> powerbi_links { get; set; }
    }
}
