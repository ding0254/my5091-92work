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
    
    public partial class Entity_rate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entity_rate()
        {
            this.Entity_instrument = new HashSet<Entity_instrument>();
        }
    
        public int Id { get; set; }
        public double Tenor { get; set; }
        public double Interest_rate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entity_instrument> Entity_instrument { get; set; }
    }
}
