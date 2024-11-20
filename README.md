# 📘 Documentación del Proyecto

## Introducción

Este documento describe la solución implementada para el *challenge de Metafar*. El objetivo es proporcionar una visión general de la arquitectura y explicar como poner en marcha el proyecto localmente.
## ¿Qué contiene el proyecto?

- **Web API**: API RESTful que gestiona la lógica de negocio.
- **Aplicación Web**: Interfaz de usuario para interactuar con la API.
- **Diagrama Entidad-Relación**: Representación gráfica de la base de datos.
- **Data dummy**: Script de BBDD con datos dummy para realizar pruebas.

## Funcionalidades

El proyecto implementa las siguientes funcionalidades clave:

1. **Login de usuario mediante número de tarjeta y PIN**
2. **Bloqueo de usuario después de 4 intentos fallidos**
3. **Vista de datos del usuario y cuenta**
4. **Vista de operaciones con paginación**
5. **Extracción de la cuenta**

## Endpoints

La API está documentada con Swagger y puede accederse en la siguiente URL (Solo acceso loca):  
[Swagger UI](http://localhost:5000/swagger/index.html)

### Rutas Disponibles

- **`GET /v1/security/{cardNumber}/{pin}`**: Genera un token de autorización.
- **`GET /v1/accounts/{cardNumber}`**: Obtiene la información de la cuenta asociada al número de tarjeta.
- **`GET /v1/operations/{cardNumber}`**: Obtienes las operaciones realizadas en la cuenta.
- **`POST /v1/accounts/balance/withdraw`**: Realiza una extracción de saldo desde la cuenta.

## 🏗️ Arquitectura y patrones implementados
1. La arquitectura de la solucion esta basada en servicios.
2. Implementa el patron mediator para la comunicacion entre la capa de presentacion y la logica de negocio.
3. CQRS solo a nivel de clases y objetos(NO BBDD).
4. Patron Repository para acceso a datos.

## 🛠️ Tecnologías Utilizadas

### Backend

- **.NET 8**: Framework principal para la creación de la API y el backend.
- **REST API**: Arquitectura de la API para la comunicación cliente-servidor.
- **Entity Framework**: ORM para interactuar con la base de datos.
- **Docker**: Contenedores para la gestión de la infraestructura.
- **SQL Server**: Base de datos relacional utilizada.

### Frontend

- **Blazor**: Framework para la construcción de la aplicación web interactiva.
- **JavaScript**: Lenguaje de programación para la lógica del frontend.
- **HTML**: Lenguaje de marcado para estructurar la interfaz de usuario.

## 🚀 Instrucciones para configurar el proyecto localmente

### Pre-requisitos

Antes de comenzar, asegúrate de tener instalados los siguientes componentes en tu máquina:

- Docker
- Docker Compose
- .NET 8
- Visual Studio o Visual Studio Code
- SQL Server Management Studio o herramienta similar

### Configuración local para iniciar aplicación y servicios

1. Clona el repositorio:

    ```bash
    git clone https://github.com/tu-usuario/metafar-challenge.git
    cd metafar-challenge
    ```

2. Verifica que Docker y Docker Compose estén correctamente instalados.

### Variables de Entorno

Las variables de entorno necesarias para la ejecución del proyecto ya están configuradas dentro del repositorio. A continuación, se detallan las variables utilizadas en el archivo `src/docker-compose.yml`:

#### Base de datos

- **`SA_PASSWORD`**: Contraseña para el usuario `sa` de SQL Server.
- **`ACCEPT_EULA`**: Aceptación de la EULA de SQL Server.

#### Web API

- **`ASPNETCORE_ENVIRONMENT`**: Entorno de ASP.NET Core (por defecto, `Development`).
- **`DB`**: Cadena de conexión a la base de datos.
- **`JWT_SECRET`**: Secreto para la generación de tokens JWT.
- **`JWT_ISSUER`**: Emisor de los tokens JWT.
- **`JWT_AUDIENCE`**: Audiencia de los tokens JWT.
- **`JWT_EXPIRATION_MINUTES`**: Tiempo de expiración de los tokens JWT en minutos.

#### Web App

- **`METAFAR_URL_BASE`**: URL base para la aplicación web.

## Ejecución del Proyecto

1. Construye y levanta los contenedores utilizando Docker Compose:
    ```bash
    docker-compose up --build
    ```

2. La API estará disponible en: `http://localhost:5000`.
3. La aplicación web estará disponible en: `http://localhost:5001`.
4. El proyecto crea automáticamente la base de datos mediante migraciones de Entity Framework.
5. Una vez los servicios estén en ejecución, conéctate a la base de datos y ejecuta el siguiente script SQL para insertar datos de prueba:

    ```bash
    /src/Metafar.Challenge.Db/dummy-data-db.sql
    ```

    **Credenciales de base de datos**:
    - **Usuario**: `sa`
    - **Contraseña**: `Password12345`
  
## 🧪 Casos de prueba
### Caso 1: Usuario con tarjeta bloqueda.
    - **NumeroTarjeta**: `34567890`
    - **Pin**: `9012`
### Caso 2: Usuario con acceso a cuenta.
    - **NumeroTarjeta**: `12345678`
    - **Pin**: `1234`
### Caso 3: Usuario con 2 intentos fallidos en el pin, con dos intentos erroneos mas se bloqueara la tarjeta.
    - **NumeroTarjeta**: `23456789`
    - **Pin**: `5678`

## 📝 Notas

- Asegúrate de que los puertos **1433**, **5000** y **5001** estén libres en tu máquina local.
- Puedes modificar las variables de entorno en el archivo `docker-compose.yml` según tus necesidades.

## 📜 Licencia

Este proyecto está licenciado bajo la Licencia MIT.

