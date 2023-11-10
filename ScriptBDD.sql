-- Crear la base de datos Tacos_Blanquita si no existe
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Tacos_Blanquita')
BEGIN
    ALTER DATABASE Tacos_Blanquita SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Tacos_Blanquita;
END

CREATE DATABASE Tacos_Blanquita;
GO

USE Tacos_Blanquita;


-- Crear la tabla TipoProducto
CREATE TABLE TipoProducto (
    IdTipoProducto INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    Clave NVARCHAR(3) NOT NULL,
    Descripcion NVARCHAR(50) NOT NULL
);


-- Crear la tabla Producto con relación de clave externa
CREATE TABLE Producto (
    IdProducto INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    IdTipoProducto INT NOT NULL,
    Descripcion NVARCHAR(50) NULL,
    Precio FLOAT NOT NULL,
    FOREIGN KEY (IdTipoProducto) REFERENCES TipoProducto (IdTipoProducto)
);

-- Crear la tabla Combo
CREATE TABLE Combo (
    IdCombo INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    Descripcion NVARCHAR(50) NULL,
    Total FLOAT NULL
);

-- Crear la tabla ProductoCombo con relaciones de clave externa
CREATE TABLE ProductoCombo (
    IdProductoCombo INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    IdProducto INT NOT NULL,
    IdCombo INT NOT NULL,
    FOREIGN KEY (IdCombo) REFERENCES Combo (IdCombo),
    FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto)
);

-- Crear la tabla Perfil
CREATE TABLE Perfil (
    IdPerfil INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    Clave VARCHAR(3) NOT NULL,
    Nombre VARCHAR(50) NOT NULL
);


-- Crear la tabla Usuario con relación de clave externa
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Correo VARCHAR(50) NOT NULL,
    Contrasena VARCHAR(500) NOT NULL,
    IdPerfil INT NOT NULL,
    FOREIGN KEY (IdPerfil) REFERENCES Perfil (IdPerfil)
);

CREATE TABLE Orden(
		IdOrden INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
		IdUsuario INT NOT NULL,
		Total FLOAT NOT NULL,
		Fecha DATETIME NOT NULL,
		FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
)

-- Crear la tabla OrdenCombo con relación de clave externa
CREATE TABLE OrdenCombo (
    IdOrdenCombo INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    IdOrden INT NOT NULL,
    IdCombo INT NOT NULL,
    FOREIGN KEY (IdOrden) REFERENCES Orden (IdOrden),
    FOREIGN KEY (IdCombo) REFERENCES Combo (IdCombo)
);


