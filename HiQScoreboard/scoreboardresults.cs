//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HiQScoreboard
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScoreboardResults
    {
        public int Id { get; set; }
        public Nullable<decimal> Q1 { get; set; }
        public Nullable<decimal> Q2 { get; set; }
        public Nullable<decimal> Q3 { get; set; }
        public Nullable<decimal> Q4 { get; set; }
        public Nullable<decimal> Q5 { get; set; }
        public int OfficesId { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Offices Offices { get; set; }
    }
}
