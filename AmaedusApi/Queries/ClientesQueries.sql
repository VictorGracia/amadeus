-- Creación de la tabla de clientes con 5 tipos de datos diferentes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Email NVARCHAR(255) NULL,           
    TipoDocumento NVARCHAR(20) NOT NULL,
    NumeroDocumento NVARCHAR(20) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Pais NVARCHAR(50) NOT NULL,
    Saldo DECIMAL(18, 2) NOT NULL DEFAULT 0.00,
    EsActivo BIT NOT NULL DEFAULT 1    
);