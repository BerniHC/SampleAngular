//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HidalgoCastro.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auth
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Token { get; set; }
        public string IpAddress { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string OSName { get; set; }
        public string OsVersion { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual User User { get; set; }
    }
}
