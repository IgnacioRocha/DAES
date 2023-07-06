# Todos los cambios realizados al proyecto se deben documentar en este archivo

Este archivo esta basado en el formato Keep a Changelog(https://keepachangelog.com/es-ES/1.0.0/)

## [2.5.5 Tramites Digitales - 06-07-2023]

### Changed

- Se modificaron los tags de Google Analytics, cambiandolos por la version GA4.

## [2.5.4 tramites digitales, backOffice- 14-03-2023]

### Changed

- se modifica modulo de administración de módulos
- se modifica recaptcha
- se modifica busqueda de usuario al momento de restablecer la contraseña

## [2.5.3 tramites digitales, backOffice- 27-02-2023]

### Changed

- Se agrega tipo de organización a certificados emitidos por trámites digitales

## ADD

- Se agrega módulo de administración de perfiles a backoffice.

## [2.5.2 Tramites Digitales - 23-02-2023]

### Changed

- Se modifico en GPDocumentoVerificacion y GPHSA los controladores agregando protocolo de seguridad.
- Se trajeron los cambios de libreria jquery-unobstrosive-

### Added

- Se agrego en web.config, tokens de ReCAPTCHA para testing de Tramites Digitales.

### Deleted

- Se eliminaron, 7zip DAES12012023 y la carpeta DAES12012023.

## [2.5.1 Tramites Digitales - 22-02-2023]

### Added

- Se agrego Protocolo de seguridad al momento de validar documento con FEA en Tramites Digitales debido al cambio de URL de GP.

## [2.5.0 DAES Completo - 17-01-2023]

## //TODO AGREGAR CAMBIOS GIO

## [2.4.1 DAES FrontOffice - 28-12-2022]

### Added

- Se agregaron en los tramites: Articulo 90, Registro Supervisor, Estudio Ahorro Credito y Cooperativa Abierta, Tags de GA para el paso 1.

## [2.4.1 DAES FrontOffice - 23-12-2022]

### Changed

- Se realizo cambio en el metodo Post de Emitir certificado para que no cargue los tramites que no corresponden.

## [2.4.1 DAES FrontOffice - 21-12-2022]

### Changed

- Se modifico webconfig actualizandolo con el más actualizado en el TFS.
- Se agregaron observaciones segun lo indicado por Control de Gestión algunos tramites digitales.

## [2.4 DAES Completo 09-12-2022]

### Changed

- Se unifica proyecto bajo Azure, haciendo pull de los cambios en TFS.
- Se cambia forma de consultar Tipo Documento al momento de generar un PDF.

## [2.3.2 Front Office] - 06-12-2022

### Changed

- Se modificaron las referencias de Google Analytics.
- Se modifico en Supervisor Auxiliar las referencias de inicio de tramite de Analytics
- Se agregaron en algunos metodos, el bypass de clave unica.

### Added

- Se agregaron, en algunos tramites, el Fin de la solicitud para la metrica de Analytics.
- Se crearon 2 vistas nuevas para ambos incisos del articulo nuevo, las cuales corresponden a la finalización del tramite.

## [2.3 - backoffice y front office] - 08-08-2022

### Change

- Se cambio completamente el método de firma electronica.
- Se modificarón las grillas de las tablas para incorporarle un filtro
- Se modifico los certificados automaticos
- Se cambio de firma electrónica a firma electrónica avanzada (FEA)
- Se modificaron permisos de usuarios
- Botón d eenviar tarea se dejó flotante
- Se utilizará la capa infraestructure para agregar interfaces

### Added

- Se agregaron nuevos datos a la grilla de tareas y documentos
- Se agrego api para consultar HSM
- se agregaron clases para poder preguntar a sigper
- Se agrego miniaturas de los documentos PDF
- Se agregaron clases para consultar a turismo
- Se agregó interface para Email
- Se agregó interface para los documentos
- Se agregó interface para Folio
- Se agregó interface para sigper
- Se agregó clase para crear folio
- Se agregó clase para pregunrtar usuarios a HSM
- Se agregó API FirmaEloc para ser consumido

### Deleted

- Se quito antiguo método de firma

## [2.2.0 - FrontOffice] - 05-07-2022

### Added

- Se agregaron funciones de Google Analytics en varias vistas en relacion a la grilla Listado de tramites Analytics.xlsx
- Se agrego al proyecto las vistas Index en Articulo 90 Primer Inciso y Segundo Inciso.

### Changed

- Se modifico la ubicación de las funciones de Google Analytics donde solicitan clave unica, se paso desde el Index al Search.

## [2.2.0 - BackOffice y FrontOffice] - 05-07-2022

### Changed

- Se modificaron las versiones de Jquery-UI y JQuery a las versiones 1.13.1 y 3.6.0 respectivamente.

## [2.2.0 - BackOffice] - 20-05-2022

### Changed

- Se modificaron todos los botones pertenecientes a la edición de Organizaciones, tanto en Reformas, Disolución y Directorio.

## [2.2.0 - BackOffice] - 20-05-2022

### Changed

- Se modificaron los botones Eliminar de Directorio y Modificaciones para que estos tuvieran la funcionalidad que faltaba y se les agrego estilo para unificarlos con los demas de la pagina.

## [2.2.0 - BackOffice] - 20-05-2022

### Added

- Se agregaron las vistas \_ComisionEdit, \_CoopAnterior y \_CoopPosterior en Task para controlar el error presentado en el ticket 36854.

### Changed

- Se modifico el controlador de Task para agregar funcionalidades a los botones faltantes respecto a la sección Disolucion de EditarOrganizacion.
- Se modifico la vista de la sección Disolución al momento de editar una organizacion en Actualizar Antecedentes.

## [2.2.0 - DAES Digital - BackOffice] - 17-05-2022

### Changed

- Se modifica Custom.cs, con la finalidad de que no arroje error si el Parrafo2 de la tabla ConfiguracionCertificado este NULL, lo deje como un string.empty.
- Se modifica el orden de los datos FechaPublicacionDiarioOficial y FechaEscrituraPublica en la parte de AG/AC en \_OrganizacionDisolucion.cshtml.

## Cambios realizados en BackOffice

Se crearon nuevos archivos en la carpeta Organización en Views, ya que considere que lo el tramite solicitado era aprte del flujo de las organizaciones y por ende era mejor usar esa vista para seguir usando el mismo modelo.
De la misma forma solo se crearon nuevas clases en relacion a esto, Disolucion, Comision Liquidadora y TipoNorma con sus respectivas tablas en la base de datos.

Se crearon nuevas funciones para cumplir con el flujo de las nuevas clases en el controlador de Organización y su posterior udpate en la base de datos en la clase custom y lo cambios para poder generar los certificados de cada tipo de organizacion segun el caso requerido en la clase TaskController.

## Cambios realizados en Base de Datos

Se crearon, para este tramite, 3 nuevas tablas Disolucion, ComisionLiquidadora y TipoNorma, cuyos scripts se encuentran tambien en esta branch que cree, feature-disolucion, en el archivos NuevasTablas.sql.

## Cambios Grandes

Por un error personal durante el desarrollo, se modifico una variable en TODO el projecto y en la base de datos. Originalmente la variable era FechaPubliccionDiarioOficial y yo la modifique a FechaPublicacionDiarioOficial por lo cual si llegara a haber un error en relacion a dicha Fecha, en tablas o durante la prueba de este proyecto puede deberse a este cambio que puede generar un gran impacto.
