//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PokemonGame.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SolicitudesAtencion
    {
        public int SolicitudId { get; set; }
        public int UsuarioId { get; set; }
        public int PokemonId { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
    
        public virtual Pokemone Pokemone { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
