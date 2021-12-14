CREATE TABLE [dbo].[TipoNorma] (
    [TipoNormaId] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]      NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([TipoNormaId] ASC)
);

GO

CREATE TABLE [dbo].[Disolucion] (
    [DisolucionId]                  INT            IDENTITY (1, 1) NOT NULL,
    [OrganizacionId]                INT            NOT NULL,
    [TipoOrganizacionId]            INT            NULL,
    [NumeroOficio]                  INT            NULL,
    [FechaPublicacionDiarioOficial] DATETIME       NULL,
    [FechaEscrituraPublica]         DATETIME       NULL,
    [FechaJuntaSocios]              DATETIME       NULL,
    [Comision]                      BIT            NULL,
    [FechaDisolucion]               DATETIME       NULL,
    [TipoNormaId]                   INT            NULL,
    [NumeroNorma]                   INT            NULL,
    [FechaNorma]                    DATETIME       NULL,
    [Autorizacion]                  NVARCHAR (MAX) NULL,
    [NumeroFojas]                   NVARCHAR (MAX) NULL,
    [AñoInscripcion]                INT            NULL,
    [DatosCBR]                      NVARCHAR (MAX) NULL,
    [MinistroDeFe]                  NVARCHAR (MAX) NULL,
    [FechaOficio]                   DATETIME       NULL,
    [FechaAsambleaSocios]           DATETIME       NULL,
    [NombreNotaria]                 NVARCHAR (MAX) NULL,
    [DatosNotario]                  NVARCHAR (MAX) NULL,
    [DirectorioId]                  INT            NULL,
    [ComisionLiquidadoraId]         INT            NULL,
    CONSTRAINT [PK_dbo.Disolucion] PRIMARY KEY CLUSTERED ([DisolucionId] ASC),
    CONSTRAINT [FK_dbo.Disolucion_dbo.Organizacion_OrganizacionId] FOREIGN KEY ([OrganizacionId]) REFERENCES [dbo].[Organizacion] ([OrganizacionId]),
    CONSTRAINT [FK_dbo.Disolucion_dbo.TipoOrganizacion_TipoOrganizacionId] FOREIGN KEY ([TipoOrganizacionId]) REFERENCES [dbo].[TipoOrganizacion] ([TipoOrganizacionId]),
    CONSTRAINT [FK_dbo.Disolucion_dbo.TipoNorma_TipoNormaId] FOREIGN KEY ([TipoNormaId]) REFERENCES [dbo].[TipoNorma] ([TipoNormaId]),
    CONSTRAINT [FK_dbo.Disolucion_dbo.Directorio_DirectorioId] FOREIGN KEY ([DirectorioId]) REFERENCES [dbo].[Directorio] ([DirectorioId]),
    CONSTRAINT [FK_dbo.Disolucion_dbo.ComisionLiquidadora_ComisionLiquidadoraId] FOREIGN KEY ([ComisionLiquidadoraId]) REFERENCES [dbo].[ComisionLiquidadora] ([ComisionLiquidadoraId])
);

GO

CREATE TABLE [dbo].[ComisionLiquidadora] (
    [ComisionLiquidadoraId] INT            IDENTITY (1, 1) NOT NULL,
    [DirectorioId]          INT            NULL,
    [DisolucionId]          INT            NULL,
    [NombreCompleto]        NVARCHAR (MAX) NULL,
    [CargoId]               INT            NULL,
    [GeneroId]              INT            NULL,
    [Rut]                   NVARCHAR (MAX) NULL,
    [FechaInicio]           DATETIME       NULL,
    [FechaTermino]          DATETIME       NULL,
    [esMiembro]             BIT            NULL,
    [OrganizacionId]        INT            NULL,
    CONSTRAINT [PK_dbo.ComisionLiquidadora] PRIMARY KEY CLUSTERED ([ComisionLiquidadoraId] ASC),
    CONSTRAINT [FK_dbo.ComisionLiquidadora_dbo.Cargo_CargoId] FOREIGN KEY ([CargoId]) REFERENCES [dbo].[Cargo] ([CargoId]),
    CONSTRAINT [FK_dbo.ComisionLiquidadora_dbo.Genero_GeneroId] FOREIGN KEY ([GeneroId]) REFERENCES [dbo].[Genero] ([GeneroId]),
    CONSTRAINT [FK_dbo.ComisionLiquidadora_dbo.Organizacion_OrganizacionId] FOREIGN KEY ([OrganizacionId]) REFERENCES [dbo].[Organizacion] ([OrganizacionId]),
    CONSTRAINT [FK_dbo.ComisionLiquidadora_dbo.Directorio_DirectorioId] FOREIGN KEY ([DirectorioId]) REFERENCES [dbo].[Directorio] ([DirectorioId]),
    CONSTRAINT [FK_dbo.ComisionLiquidadora_dbo.Disolucion_DisolucionId] FOREIGN KEY ([DisolucionId]) REFERENCES [dbo].[Disolucion] ([DisolucionId])
);

GO
