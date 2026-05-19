# Relevos Familiares - Front-End hecho en .NET MAUI

Este proyecto se trata de una aplicación móvil desarrollada en **.NET MAUI** para la gestión de reservas de personal para servicios hospitalarios y/o domésticos. Dicha aplicación ha sido realizada como TFG para el Ciclo Formativo de Grado Superior de Desarrollo de Aplicaciones Multiplataforma.

Incluye dos perfiles: **Cliente** y **Empleado**, con navegación independiente.

## Funcionalidades principales

### Cliente

- Registro e inicio de sesión
- Selección de fecha y tipo de servicio
- Selección de horas (diurno, nocturno, acompañamiento)
- Confirmación de reserva
- Gestión de reservas (ver, cancelar)

### Empleado

- Inicio de sesión
- Ver servicios disponibles
- Aceptar servicios
- Ver servicios aceptados
- Cancelar servicios aceptados previamente

## Diseño

- Tema claro/oscuro
- Paleta de colores personalizada
- Estilos globales para botones, labels, entries, borders, checkboxes y shell

## Estructura del proyecto

La base son las páginas (pages), cada una con su lógica (XAML + cs). Todo lo referente al estilo de la aplicación se encuentra en la carpeta Resources/Styles. En referente a navegación, la información se encuentra en AppShell.xaml

## Requisitos

- .NET 10 / .NET MAUI  
- Android SDK  
- Visual Studio 2026 

## Instalación

1. Clonar el repositorio  
2. Abrir en Visual Studio  
3. Seleccionar **Android**  
4. Ejecutar
