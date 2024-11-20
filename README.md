# Documentación del Proyecto

## Introducción
Este documento describe la solución implementada para el challenge de Metafar. El objetivo es proporcionar una visión general de la arquitectura y explicar el funcionamiento de cada una de las capas del proyecto.

## ¿Que contiene el proyecto?
* Web API
* Web application
* Diagrama Entidad Relacion
  
## Tecnologías Utilizadas

### Backend
- Net 8
- Rest API
- Entity framework
- Docker
- Sql server

### Frontend
- Blazor
- JavaScrip
- Html

## Instrucciones para configurar el proyecto localmente

### Pre-requisitos
- Docker
- Docker Compose
- Net 8
- Visual Studio / Visual code

## Configuración

1. Clona el repositorio:

    ```sh
    git clone https://github.com/tu-usuario/metafar-challenge.git
    cd metafar-challenge
    ```

2. Asegúrate de tener Docker y Docker Compose instalados en tu sistema.

   
## Variables de Entorno
Por practicidad y para evitar inconvenientes en la ejecucion de la aplicacion las variables ya se encuentran configuradas dentro del repositorio.

Las siguientes variables de entorno se utilizan en el archivo `src/docker-compose.yml`:

### Base de datos
- `SA_PASSWORD`: Contraseña para el usuario `sa` de SQL Server.
- `ACCEPT_EULA`: Aceptación de la EULA de SQL Server.

 ### Web API
- `ASPNETCORE_ENVIRONMENT`: Entorno de ASP.NET Core (por defecto `Development`).
- `DB`: Cadena de conexión a la base de datos.
- `JWT_SECRET`: Secreto para la generación de tokens JWT.
- `JWT_ISSUER`: Emisor de los tokens JWT.
- `JWT_AUDIENCE`: Audiencia de los tokens JWT.
- `JWT_EXPIRATION_MINUTES`: Minutos de expiración para los tokens JWT.

### WEB APP
- `METAFAR_URL_BASE`: URL base para la aplicación web.

## Endpoints

### API
Swagger url http://localhost:5000/swagger/index.html

- `GET /v1/accounts/{cardNumber}`: Obtiene la información de la cuenta por número de tarjeta.
- `GET /v1/accounts/{cardNumber}`: Obtiene la información de la cuenta por número de tarjeta.
- `POST /v1/accounts/balance/withdraw`: Realiza una extración de la cuenta.

## Ejecución

1. Construye y levanta los contenedores con Docker Compose:

    ```sh
    docker-compose up --build
    ```

2. La API estará disponible en `http://localhost:5000` y la aplicación web en `http://localhost:5001`.
3. Por defecto el proyecto crea la BBDD mediante una migracion
4. Una vez los servicios esten en ejecution dentro de docker conectarse a la BBDD y ejecutar el script de SQL (/src/Metafar.Challenge.Db/dummy-data-db.sql) el cual contiene datos dummy.


## Notas

- Asegúrate de que los puertos `1433`, `5000` y `5001` estén disponibles en tu máquina.
- Puedes modificar las variables de entorno en el archivo `docker-compose.yml` según tus necesidades.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT.
  
