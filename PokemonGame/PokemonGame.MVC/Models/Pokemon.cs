using System.Collections.Generic;

namespace PokemonFame.MVC.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public string SpriteUrl { get; set; }
        public List<Move> Moves { get; set; }
        public bool IsAI { get; set; }

        public Pokemon()
        {
            Moves = new List<Move>();
        }
    }

    public class Move
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public string SpecialEffect { get; set; }
    }
}


