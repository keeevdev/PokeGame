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
    
    public partial class Pokedex
    {
        public int PokedexId { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Debilidad { get; set; }
        public string Evoluciones { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public int Numero { get; set; }
    }
}