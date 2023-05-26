USE AES
GO



CREATE TABLE [dbo].[Aprobacion](
[AprobacionId] [int] IDENTITY(1,1) NOT NULL,
[Nombre] [nvarchar](max) NULL,
CONSTRAINT [PK_Aprobacion] PRIMARY KEY CLUSTERED
(
[AprobacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ExistenciaLegal](
	[ExistenciaId] [int] IDENTITY(1,1) NOT NULL,
	[TipoNormaId] [int] NULL,
	[NumeroNorma] [int] NULL,
	[FechaPublicacionn] [datetime] NULL,
	[AutorizadoPor] [nvarchar](max) NULL,
	[OrganizacionId] [int] NULL,
	[FechaNorma] [datetime] NULL,
	[FechaConstitutivaSocios] [datetime] NULL,
	[FechaEscrituraPublica] [datetime] NULL,
	[DatosGeneralesNotario] [nvarchar](max) NULL,
	[Fojas] [int] NULL,
	[FechaInscripcion] [datetime] NULL,
	[DatosCBR] [nvarchar](max) NULL,
	[NumeroOficio] [int] NULL,
	[FechaOficio] [datetime] NULL,
	[FechaAsambleaConstitutiva] [datetime] NULL,
	[AprobacionId] [int] NULL,
 CONSTRAINT [PK_ExistenciaLegal] PRIMARY KEY CLUSTERED 
(
	[ExistenciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Reforma](
	[IdReforma] [int] IDENTITY(1,1) NOT NULL,
	[FechaReforma] [datetime] NULL,
	[UltimaReforma] [bit] NULL,
	[NumeroNormaa] [int] NULL,
	[FechaNormaa] [datetime] NULL,
	[FechaPublicacionDiario] [datetime] NULL,
	[DatosGeneralNotario] [nvarchar](max) NULL,
	[OrganizacionId] [int] NULL,
	[TipoNormaId] [int] NULL,
	[FechaJuntaGeneral] [datetime] NULL,
	[FechaPublicacion] [datetime] NULL,
	[AnoInscripcion] [datetime] NULL,
	[DatosCBR] [nvarchar](max) NULL,
	[FechaEscrituraPublica] [datetime] NULL,
	[Fojas] [int] NULL,
	[AsambleaDepId] [int] NULL,
	[FechaAsambleaDep] [datetime] NULL,
	[FechaOficio] [datetime] NULL,
	[NumeroOficio] [int] NULL,
	[AprobacionId] [int] NULL,
 CONSTRAINT [PK_Reforma] PRIMARY KEY CLUSTERED 
(
	[IdReforma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Saneamiento](
	[IdSaneamiento] [int] IDENTITY(1,1) NOT NULL,
	[FechaEscrituraPublicaa] [datetime] NULL,
	[FechaaPublicacionDiario] [datetime] NULL,
	[DatoGeneralesNotario] [nvarchar](max) NULL,
	[Fojass] [int] NULL,
	[FechaaInscripcion] [datetime] NULL,
	[DatossCBR] [nvarchar](max) NULL,
	[OrganizacionId] [int] NULL,
 CONSTRAINT [PK_Saneamiento] PRIMARY KEY CLUSTERED 
(
	[IdSaneamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ConfiguracionCertificado](
	[ConfiguracionCertificadoId] [int] IDENTITY(1,1) NOT NULL,
	[TipoOrganizacionId] [int] NOT NULL,
	[Parrafo1] [nvarchar](max) NOT NULL,
	[Parrafo2] [nvarchar](max) NULL,
	[Parrafo3] [nvarchar](max) NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[Ciudad] [nvarchar](max) NULL,
	[UnidadOrganizacional] [nvarchar](max) NULL,
	[XML] [nvarchar](max) NULL,
	[IsActivo] [bit] NOT NULL,
	[TieneDirectorio] [bit] NOT NULL,
	[TieneEstatuto] [bit] NOT NULL,
	[TipoDocumentoId] [int] NULL,
 CONSTRAINT [PK_dbo.ConfiguracionCertificado] PRIMARY KEY CLUSTERED 
(
	[ConfiguracionCertificadoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TipoNorma](
	[TipoNormaId] [int] NOT NULL,
	[Nombre] [nvarchar](max) NULL,
 CONSTRAINT [PK_TipoNorma] PRIMARY KEY CLUSTERED 
(
	[TipoNormaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ConfiguracionCertificado] ADD [Parrafo4] [nvarchar](max)
GO

ALTER TABLE [dbo].[ExistenciaLegal]  WITH CHECK ADD  CONSTRAINT [FK_ExistenciaLegal_Aprobacion] FOREIGN KEY([AprobacionId])
REFERENCES [dbo].[Aprobacion] ([AprobacionId])
GO

ALTER TABLE [dbo].[ExistenciaLegal] CHECK CONSTRAINT [FK_ExistenciaLegal_Aprobacion]
GO

ALTER TABLE [dbo].[ExistenciaLegal]  WITH CHECK ADD  CONSTRAINT [FK_ExistenciaLegal_Organizacion] FOREIGN KEY([OrganizacionId])
REFERENCES [dbo].[Organizacion] ([OrganizacionId])
GO

ALTER TABLE [dbo].[ExistenciaLegal] CHECK CONSTRAINT [FK_ExistenciaLegal_Organizacion]
GO

ALTER TABLE [dbo].[ExistenciaLegal]  WITH CHECK ADD  CONSTRAINT [FK_ExistenciaLegal_TipoNorma] FOREIGN KEY([TipoNormaId])
REFERENCES [dbo].[TipoNorma] ([TipoNormaId])
GO

ALTER TABLE [dbo].[ExistenciaLegal] CHECK CONSTRAINT [FK_ExistenciaLegal_TipoNorma]
GO




ALTER TABLE [dbo].[Reforma]  WITH CHECK ADD  CONSTRAINT [FK_Reforma_Aprobacion] FOREIGN KEY([AprobacionId])
REFERENCES [dbo].[Aprobacion] ([AprobacionId])
GO

ALTER TABLE [dbo].[Reforma] CHECK CONSTRAINT [FK_Reforma_Aprobacion]
GO

ALTER TABLE [dbo].[Reforma]  WITH CHECK ADD  CONSTRAINT [FK_Reforma_Organizacion] FOREIGN KEY([OrganizacionId])
REFERENCES [dbo].[Organizacion] ([OrganizacionId])
GO

ALTER TABLE [dbo].[Reforma] CHECK CONSTRAINT [FK_Reforma_Organizacion]
GO

ALTER TABLE [dbo].[Reforma]  WITH CHECK ADD  CONSTRAINT [FK_Reforma_TipoNorma] FOREIGN KEY([TipoNormaId])
REFERENCES [dbo].[TipoNorma] ([TipoNormaId])
GO

ALTER TABLE [dbo].[Reforma] CHECK CONSTRAINT [FK_Reforma_TipoNorma]
GO






ALTER TABLE [dbo].[Saneamiento]  WITH CHECK ADD  CONSTRAINT [FK_Saneamiento_Organizacion] FOREIGN KEY([OrganizacionId])
REFERENCES [dbo].[Organizacion] ([OrganizacionId])
GO

ALTER TABLE [dbo].[Saneamiento] CHECK CONSTRAINT [FK_Saneamiento_Organizacion]
GO



SET IDENTITY_INSERT [dbo].[ConfiguracionCertificado] ON 

INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (1, 1, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresa de Menor tamaño certifica que, la entidad denominada [RAZONSOCIAL], se encuentra inscrita en el Registro de Cooperativas con el Rol [ROL] y que su personalidad jurídica se encuentra vigente.', N' ', N'*DECRETO TRA N° 119247/1/2021, del 01 de febrero de 2021.', N'C E R T I F I C A D O
 ', N'SANTIAGO', NULL, NULL, 1, 0, 0, 2, NULL)
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (2, 2, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresa de Menor tamaño certifica que, la entidad denominada [RAZONSOCIAL], se encuentra inscrita en el Registro de Asociaciones Gremiales con el Número [ROL] y que su personalidad jurídica se encuentra vigente.', N'El último Directorio informado por la entidad es:', N'Conforme a lo dispuesto por el artículo 1° letras d) y g) de la Ley N°21.239, publicada en el Diario Oficial el 23 de junio de 2020, el mandato de los integrantes del directorio cuya vigencia hubiere vencido durante el tiempo transcurrido desde que se decretó el estado de excepción constitucional de catástrofe, por calamidad pública, declarado por decreto supremo Nº104, de 2020, del Ministerio del Interior y Seguridad Pública, o en el tiempo en que éste fuere prorrogado, si es el caso; o que hayan cumplido su vigencia en los tres meses anteriores a su declaración, se entenderá prorrogado hasta los tres meses siguientes de que el estado de catástrofe referido, o su prórroga, haya finalizado.

*DECRETO TRA N° 119247/1/2021, del 01 de febrero de 2021.', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 1, 0, 1, NULL)
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (3, 3, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresa de Menor tamaño certifica que, la entidad denominada [RAZONSOCIAL], se encuentra inscrita en el Registro de Asociaciones de Consumidores con el Número [ROL] y que su personalidad jurídica se encuentra vigente.', N'El último Directorio informado por la entidad es:', N'Conforme a lo dispuesto por el artículo 1° letras d) y g) de la Ley N°21.239, publicada en el Diario Oficial el 23 de junio de 2020, el mandato de los integrantes del directorio cuya vigencia hubiere vencido durante el tiempo transcurrido desde que se decretó el estado de excepción constitucional de catástrofe, por calamidad pública, declarado por decreto supremo Nº104, de 2020, del Ministerio del Interior y Seguridad Pública, o en el tiempo en que éste fuere prorrogado, si es el caso; o que hayan cumplido su vigencia en los tres meses anteriores a su declaración, se entenderá prorrogado hasta los tres meses siguientes de que el estado de catástrofe referido, o su prórroga, haya finalizado.

*DECRETO TRA N° 119247/1/2021, del 01 de febrero de 2021.', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 1, 0, 1, NULL)
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (4, 3, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresa de Menor tamaño certifica que, la entidad denominada [RAZONSOCIAL], se encuentra inscrita en el Registro de Asociaciones de Consumidores con el Número [ROL] y que su personalidad jurídica se encuentra vigente.', N' ', N'*DECRETO TRA N° 119247/1/2021, del 01 de febrero de 2021.
', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 0, 0, 2, NULL)
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (5, 1, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresa de Menor tamaño certifica que, la entidad denominada [RAZONSOCIAL], se encuentra inscrita en el Registro de Cooperativas con el Rol [ROL] y que su personalidad jurídica se encuentra vigente.', N'El último Consejo de Administración informado por la entidad, electo con fecha [FECHACELEBRACION], está compuesto por las siguientes personas:', N'Conforme a lo dispuesto por el artículo 1° letra f) de la Ley N°21.239, publicada en el Diario Oficial el 23 de junio de 2020, el mandato de los integrantes del consejo de administración cuya vigencia hubiere vencido durante el tiempo transcurrido desde que se decretó el estado de excepción constitucional de catástrofe, por calamidad pública, declarado por decreto supremo Nº104, de 2020, del Ministerio del Interior y Seguridad Pública, o en el tiempo en que éste fuere prorrogado, si es el caso; o que hayan cumplido su vigencia en los tres meses anteriores a su declaración, se entenderá prorrogado hasta los tres meses siguientes de que el estado de catástrofe referido, o su prórroga, haya finalizado.

*DECRETO TRA N° 119247/1/2021, del 01 de febrero de 2021.
', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 1, 0, 1, NULL)
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (6, 2, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresa de Menor tamaño certifica que, la entidad denominada [RAZONSOCIAL], se encuentra inscrita en el Registro de Asociaciones Gremiales con el Número [ROL] y que su personalidad jurídica se encuentra vigente.', N' ', N'*DECRETO TRA N° 119247/1/2021, del 01 de febrero de 2021.', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 0, 0, 2, NULL)
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (7, 1, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresas de Menor Tamaño certifica que, la  [RAZONSOCIAL] ha sido inscrita en el Registro de Cooperativas bajo el rol N° [ROL].', N'Autorizacion de existencia legal: [TIPONORMA][NUMERONORMA][FECHANORMA][FECHAPUBLICACIONN][AUTORIZADOPOR][FECHAESCRITURAPUBLICA][DATOSGENERALNOTARIO][FOJAS][FECHAINSCRIPCION][DATOSCBR]', N'Saneamiento: [FECHAESCRITURAPUBLICA][FECHAPUBLICACIONDIARIO][DATOSGENERALESNOTARIO][FOJAS][FECHAINSCRIPCION][DATOSCBR] ', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 0, 1, 103, N'Reforma/as: [FECHAREFORMA][FECHAREFORMAA][NUMERONORMARREF][FECHANORMAREF][FECHAPUBLICACIONDIARIOREF][DATOGENERALNOTARIOREF][TIPONORMAREF][FECHAJUNTAGENERAL][FECHAPUBLICACIONREF][anoinscripcion][DATOSCBRREF][FECHAESCRITURAPUBLICAREF][FECHAOFICIOREF][NUMEROOFICIOREF][APROBACION][ULTIMA][ULTIMAA] ')
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) VALUES (11, 3, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresas de Menor Tamaño certifica que, la  [RAZONSOCIAL] ha sido inscrita en el Registro de Asociación gremial bajo el rol N° [ROL] y su personalidad juridica se encuenta [VIGENTE].', N'Autorizacion de existencia legal:  [FECHACONSTITUTIVASOCIOS] [NUMEROOFICIO] [FECHAOFICIO] [APROBACION].', N' ', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 0, 1, 103, N'Reformas: [ASAMBLEA] [FECHAASAMBLEA] [NUMEROOFICIO] [FECHAOFICIO] [APROBACION][ULTIMA].')
INSERT [dbo].[ConfiguracionCertificado] ([ConfiguracionCertificadoId], [TipoOrganizacionId], [Parrafo1], [Parrafo2], [Parrafo3], [Titulo], [Ciudad], [UnidadOrganizacional], [XML], [IsActivo], [TieneDirectorio], [TieneEstatuto], [TipoDocumentoId], [Parrafo4]) 
VALUES (15, 2, N'La División de Asociatividad y Cooperativas de la Subsecretaría de Economía y Empresas de Menor Tamaño certifica que, la  [RAZONSOCIAL] ha sido inscrita en el Registro de Asociación de consumidores bajo el rol N° [ROL] y su personalidad juridica se encuenta [VIGENTE].', N'Autorizacion de existencia legal:  [FECHACONSTITUTIVASOCIOS] [NUMEROOFICIO] [FECHAOFICIO] [APROBACION].', N'', N'C E R T I F I C A D O', N'SANTIAGO', NULL, NULL, 1, 0, 1, 103, N' Reformas: [ASAMBLEA] [FECHAASAMBLEA] [NUMEROOFICIO] [FECHAOFICIO] [APROBACION][ULTIMA] ')
SET IDENTITY_INSERT [dbo].[ConfiguracionCertificado] OFF
GO
INSERT [dbo].[TipoNorma] ([TipoNormaId], [Nombre]) VALUES (1, N'Decreto')
INSERT [dbo].[TipoNorma] ([TipoNormaId], [Nombre]) VALUES (2, N'Decreto Supremo')
INSERT [dbo].[TipoNorma] ([TipoNormaId], [Nombre]) VALUES (3, N'Resolucion')
GO



SET IDENTITY_INSERT [dbo].[Aprobacion] ON
INSERT [dbo].[Aprobacion] ([AprobacionId], [Nombre]) VALUES (1, N'Aprueba')
INSERT [dbo].[Aprobacion] ([AprobacionId], [Nombre]) VALUES (2, N'Objeta')
SET IDENTITY_INSERT [dbo].[Aprobacion] OFF
GO




