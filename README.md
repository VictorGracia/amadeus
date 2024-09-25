# Para iniciar la api se hace desde Api y con dotnet run
# Para correr las pruebas unitarias, se hace desde Api y con dotnet test
# En el front se está llamando la api desde http://localhost:5007 
# El front se corre con ng serve

# Proyecto API

Este proyecto es una API desarrollada utilizando .NET 8.0. porque es la versión que tengo disponible en mi maquina. La API se conecta a una base de datos SQL Server alojada en Azure.

## Base de Datos

Se ha creado una base de datos en Azure SQL Server. A continuación se detallan las credenciales necesarias para acceder a ella:

- **Servidor:** `amadeussqldatabase.database.windows.net`
- **Base de Datos:** `amadeus_db`
- **Usuario:** `user_root`
- **Contraseña:** `Amadeus_DB_key*!`

### 1. Controladores

La **capa de controladores** maneja las solicitudes HTTP y las respuestas que se envían al cliente. Cada controlador se encarga de una entidad o funcionalidad específica y se basa en los servicios de la capa de negocio para procesar las solicitudes.

### 2. Modelos

La **capa de modelos** define las entidades del dominio que representan los datos en la base de datos. Cada modelo es una representación de una tabla en la base de datos y contiene las propiedades correspondientes.

### 3. Consultas (Queries)

La **capa de consultas** utiliza Entity Framework para realizar operaciones de acceso a datos. Aquí se pueden implementar métodos específicos que encapsulan las consultas complejas necesarias para interactuar con la base de datos de manera eficiente.

## Uso de Entity Framework

Entity Framework se utiliza en este proyecto para facilitar la interacción con la base de datos. Proporciona una forma sencilla y eficiente de realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en los datos y permite la utilización de LINQ para consultas más complejas, simplificando así el acceso a los datos.

## Se agrega el archivo queries para poner ahí las creaciones importantes


## Frontend

El frontend está desarrollado en Angular, proporcionando una interfaz de usuario interactiva que consume la API. Se asegura una buena comunicación entre el frontend y el backend a través de solicitudes HTTP, utilizando el endpoint de la API.
