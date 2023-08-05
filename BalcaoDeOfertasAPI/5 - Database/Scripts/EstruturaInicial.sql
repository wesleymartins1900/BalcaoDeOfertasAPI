USE [master];
GO

/* ALTERE O @Path PARA O CAMINHO QUE O ARQUIVO DE DATABASE DEVE SER CRIADO */
DECLARE @Path NVARCHAR(MAX) = 'D:\Development\EclipseWorks\DesafioTecnico\BalcaoDeOfertasAPI\BalcaoDeOfertasAPI\5 - Database\';

-- @ArquivoDados e @ArquivoLog já está no padrão que o sistema irá utilizar
DECLARE @ArquivoDados NVARCHAR(MAX) = @Path + 'BalcaoDb.mdf';
DECLARE @ArquivoLog NVARCHAR(MAX) = @Path + 'BalcaoDb_log.ldf';

-- Cria os arquivos MDF e LDF caso não existam
IF ((SELECT name FROM sys.databases WHERE name = 'BalcaoDb') IS NULL)
BEGIN
	DECLARE @SQL NVARCHAR(MAX);
	SET @SQL = N'CREATE DATABASE BalcaoDb ON
				(NAME = N''BalcaoDb'', 
					FILENAME = N''' + @ArquivoDados + N''', 
					SIZE = 8192KB , 
					MAXSIZE = UNLIMITED, 
					FILEGROWTH = 65536KB )
				LOG ON
				(NAME = N''BalcaoDb_log'', 
					FILENAME = N''' + @ArquivoLog + N''', 
					SIZE = 8192KB , 
					MAXSIZE = 2048GB , 
					FILEGROWTH = 65536KB );';

	EXEC sp_executesql @SQL;
END
GO

USE [BalcaoDb];
GO

-- Tabelas
IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
	CREATE TABLE Usuario
	(
		Id UNIQUEIDENTIFIER PRIMARY KEY,
		Nome VARCHAR(60),
	)
	GO

IF OBJECT_ID(N'dbo.Carteira', N'U') IS NULL
	CREATE TABLE Carteira
	(
		Id UNIQUEIDENTIFIER PRIMARY KEY,
		UsuarioId UNIQUEIDENTIFIER NOT NULL,
		Nome VARCHAR(60),

		FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
	)
	GO

IF OBJECT_ID(N'dbo.Moeda', N'U') IS NULL
	CREATE TABLE Moeda
	(
		Id UNIQUEIDENTIFIER PRIMARY KEY,
		Nome VARCHAR(60) NOT NULL,
		QuantidadeTotal INT NOT NULL,
		ValorReal DECIMAL(10, 2),
		CarteiraId UNIQUEIDENTIFIER NOT NULL,

		FOREIGN KEY (CarteiraId) REFERENCES Carteira(Id)
	)
	GO

IF OBJECT_ID(N'dbo.Oferta', N'U') IS NULL
	CREATE TABLE Oferta
	(
		Id BIGINT PRIMARY KEY IDENTITY(1,1),
		PrecoUnitario DECIMAL(10, 2) NOT NULL,
		Quantidade INT NOT NULL,
		DataEHoraInclusao DATETIME2 NOT NULL,
		DataEHoraExclusao DATETIME2 NOT NULL,
		Excluido BIT NOT NULL,
		UsuarioId UNIQUEIDENTIFIER NOT NULL,
		MoedaId UNIQUEIDENTIFIER NOT NULL,

		FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
		FOREIGN KEY (MoedaId) REFERENCES Moeda(Id)
	)
	GO