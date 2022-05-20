# Todos los cambios realizados al proyecto se deben documentar en este archivo

Este archivo esta basado en el formato Keep a Changelog(https://keepachangelog.com/es-ES/1.0.0/)

Cambios realizados en la solución, tanto adherencias, como modificaciones y eliminaciones hechas durante el proceso de desarrollo del tramite N°2 Certificado de Disolucion

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
