//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace portfolio_manager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entity_Historyprice
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public System.DateTime Date { get; set; }
        public double ClosePrice { get; set; }
        public string CompanyName { get; set; }
    }
}
