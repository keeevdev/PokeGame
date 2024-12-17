-- swe crea la bd
CREATE DATABASE PokemonGameDB;
GO

-- se usa la bd de pokemon
USE PokemonGameDB;
GO

-- Usuarios
CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(40) NOT NULL,
    ContraseñaHasheada NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(15) CHECK (Rol IN ('Entrenador', 'Administrador', 'Enfermera')) NOT NULL,
    Nombre NVARCHAR(20) NOT NULL,
	Foto VARBINARY(MAX)
);
GO

-- Pokemones
CREATE TABLE Pokemones (
    PokemonId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(20) NOT NULL,
	Foto VARBINARY(MAX),
    Tipo NVARCHAR(30) NOT NULL,
    Debilidad NVARCHAR(110) NOT NULL,
    Evoluciones NVARCHAR(200),
    Peso DECIMAL(5,2),
    Numero INT NOT NULL
);
GO

-- Tabla entrenadores-pokemones relacion entre los entrenadores y sus pokemones
CREATE TABLE EntrenadoresPokemones (
    EntrenadorPokemonId INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    PokemonId INT NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (PokemonId) REFERENCES Pokemones(PokemonId)
);
GO

-- Tabla mensajes, mensaje entre entrenadores
CREATE TABLE Mensajes (
    MensajeId INT PRIMARY KEY IDENTITY(1,1),
    Contenido NVARCHAR(MAX) NOT NULL,
    FechaEnvio DATETIME NOT NULL DEFAULT GETDATE(),
    EmisorId INT NOT NULL,
    ReceptorId INT NOT NULL,
    FOREIGN KEY (EmisorId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (ReceptorId) REFERENCES Usuarios(UsuarioId)
);
GO

-- Tabla Pokedex - solo lo pueden usar los admin
CREATE TABLE Pokedex (
    PokedexId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(30) NOT NULL,
    Tipo NVARCHAR(30) NOT NULL,
    Debilidad NVARCHAR(55) NOT NULL,
    Evoluciones NVARCHAR(255),
    Peso DECIMAL(5,2),
    Numero INT NOT NULL
);
GO

-- Tabla para manejar las solicitudes a enfermeras
CREATE TABLE SolicitudesAtencion (
    SolicitudId INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    PokemonId INT NOT NULL,
    FechaSolicitud DATETIME NOT NULL DEFAULT GETDATE(),
    Estado NVARCHAR(20) CHECK (Estado IN ('Pendiente', 'Atendida')) NOT NULL DEFAULT 'Pendiente',
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (PokemonId) REFERENCES Pokemones(PokemonId)
);
GO




INSERT INTO SolicitudesAtencion (UsuarioId, PokemonId)
VALUES (1, 1);

SELECT * FROM dbo.SolicitudesAtencion;

INSERT INTO SolicitudesAtencion (UsuarioId, PokemonId)
VALUES (1, 101);

SELECT * FROM dbo.Pokemones;

INSERT INTO SolicitudesAtencion (UsuarioId, PokemonId)
VALUES (1, 2);

INSERT INTO SolicitudesAtencion (UsuarioId, PokemonId)
VALUES (1, 3);

INSERT INTO SolicitudesAtencion (UsuarioId, PokemonId)
VALUES (1, 3);

SELECT * FROM dbo.Usuarios;

DELETE FROM Usuarios
WHERE UsuarioId = 6;

