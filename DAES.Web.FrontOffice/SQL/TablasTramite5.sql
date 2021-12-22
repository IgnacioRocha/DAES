USE [AESLocal]
GO

/****** Object: Table [dbo].[RepresentanteLegal] Script Date: 22-12-2021 11:12:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TipoPersonaJuridica] (
    [TipoPersonaJuridicaId] INT            IDENTITY (1, 1) NOT NULL,
    [NombrePersonaJuridica] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.TipoPersonaJuridica] PRIMARY KEY CLUSTERED ([TipoPersonaJuridicaId] ASC)
);
go

CREATE TABLE [dbo].[SupervisorAuxiliarTemporal] (
    [SupervisorAuxiliarTempId] INT             IDENTITY (1, 1) NOT NULL,
    [Rut]                      NVARCHAR (MAX)  NULL,
    [DomicilioLegal]           NVARCHAR (MAX)  NULL,
    [Telefono]                 NVARCHAR (MAX)  NULL,
    [CorreoElectronico]        NVARCHAR (MAX)  NULL,
    [TipoPersonaJuridicaId]    INT             NULL,
    [RazonSocial]              NVARCHAR (MAX)  NULL,
    [DocumentoAdjunto]         VARBINARY (MAX) NULL,
    CONSTRAINT [PK_dbo.SupervisorAuxiliarTemporal] PRIMARY KEY CLUSTERED ([SupervisorAuxiliarTempId] ASC),
    CONSTRAINT [FK_dbo.SupervisorAuxiliarTemporal_dbo.TipoPersonaJuridica_TipoPersonaJuridicaId] FOREIGN KEY ([TipoPersonaJuridicaId]) REFERENCES [dbo].[TipoPersonaJuridica] ([TipoPersonaJuridicaId])
);
go

CREATE TABLE [dbo].[SupervisorAuxiliar] (
    [SupervisorAuxiliarId]  INT             IDENTITY (1, 1) NOT NULL,
    [Rut]                   NVARCHAR (MAX)  NULL,
    [DomicilioLegal]        NVARCHAR (MAX)  NULL,
    [Telefono]              NVARCHAR (MAX)  NULL,
    [CorreoElectronico]     NVARCHAR (MAX)  NULL,
    [TipoPersonaJuridicaId] INT             NULL,
    [RazonSocial]           NVARCHAR (MAX)  NULL,
    [DocumentoAdjunto]      VARBINARY (MAX) NULL,
    [Aprobado]              BIT             NULL,
    CONSTRAINT [PK_dbo.SupervisorAuxiliar] PRIMARY KEY CLUSTERED ([SupervisorAuxiliarId] ASC),
    CONSTRAINT [FK_dbo.SupervisorAuxiliar_dbo.TipoPersonaJuridica_TipoPersonaJuridicaId] FOREIGN KEY ([TipoPersonaJuridicaId]) REFERENCES [dbo].[TipoPersonaJuridica] ([TipoPersonaJuridicaId])
);
go

CREATE TABLE [dbo].[RepresentanteLegal] (
    [RepresentanteLegalId] INT            IDENTITY (1, 1) NOT NULL,
    [NombreCompeto]        NVARCHAR (MAX) NULL,
    [Run]                  NVARCHAR (MAX) NULL,
    [Profesion]            NVARCHAR (MAX) NULL,
    [Domicilio]            NVARCHAR (MAX) NULL,
    [Nacionalidad]         NVARCHAR (MAX) NULL,
    [SupervisorAuxiliarId] INT            NOT NULL
);
go
CREATE TABLE [dbo].[RepresentanteLegal] (
    [RepresentanteLegalId] INT            IDENTITY (1, 1) NOT NULL,
    [NombreCompeto]        NVARCHAR (MAX) NULL,
    [Run]                  NVARCHAR (MAX) NULL,
    [Profesion]            NVARCHAR (MAX) NULL,
    [Domicilio]            NVARCHAR (MAX) NULL,
    [Nacionalidad]         NVARCHAR (MAX) NULL,
    [SupervisorAuxiliarId] INT            NOT NULL
);
go
CREATE TABLE [dbo].[EscrituraConstitucion] (
    [EscrituraConstitucionId] INT            IDENTITY (1, 1) NOT NULL,
    [Notaria]                 NVARCHAR (MAX) NULL,
    [Fecha]                   DATETIME       NULL,
    [NumeroRepertorio]        INT            NULL,
    [SupervisorAuxiliarId]    INT            NULL,
    CONSTRAINT [PK_dbo.EscrituraConstitucion] PRIMARY KEY CLUSTERED ([EscrituraConstitucionId] ASC),
    CONSTRAINT [FK_dbo.EscrituraConstitucion_dbo.SupervisorAuxiliar_SupervisorAuxiliarId] FOREIGN KEY ([SupervisorAuxiliarId]) REFERENCES [dbo].[SupervisorAuxiliar] ([SupervisorAuxiliarId])
);
go
