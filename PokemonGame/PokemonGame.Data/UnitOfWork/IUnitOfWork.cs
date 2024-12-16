using PokemonGame.Data.Repositories.Interfaces;
using System;

namespace PokemonGame.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IPokemonRepository Pokemones { get; }
        IEntrenadorPokemonRepository EntrenadoresPokemones { get; }
        IMensajeRepository Mensajes { get; }
        IPokedexRepository Pokedex { get; }
        ISolicitudAtencionRepository SolicitudesAtencion { get; }

        void Save();
    }
}

