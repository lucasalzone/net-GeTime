//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeTime
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commesse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commesse()
        {
            this.giorniCommesse = new HashSet<giorniCommesse>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }
        public Nullable<int> capienza { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<giorniCommesse> giorniCommesse { get; set; }
    }
}