# 🚀 Prueba Técnica para Grupo COS - API Backend

¡Gracias por la oportunidad de presentar esta prueba técnica para Grupo COS! 🙏  
Este proyecto demuestra una API RESTful desarrollada con .NET Core, Entity Framework y arquitectura limpia.

## 📋 Requisitos Previos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Recomendado) o VS Code
- [SQLite](https://download.sqlitebrowser.org/DB.Browser.for.SQLite-v3.13.1-win64.msi) (LocalDB)
- [Postman](https://www.postman.com/downloads/) (Para pruebas de API)

## 🛠️ Configuración del Proyecto

### 1. Clonar el Repositorio
```bash
git clone https://github.com/Sergio9905/PruebaTecnicaGrupoCOS.git
cd PruebaTecnicaGrupoCOS
```

### 2. Restauración de Paquetes NuGet

Opción 1: Desde Visual Studio
```bash
Click derecho en Solution > Restore NuGet Packages
```

Opción 2: CLI
```bash
dotnet restore ./PruebaTecnicaGrupoCOS.sln
```

## 🗃️ Configuración de Base de Datos

### 3. Migraciones de Base de Datos

1. Instalar Herramientas de EF Core (si no las tienes)
```bash
dotnet tool install --global dotnet-ef
```

2. Crear una Nueva Migración
Desde la raíz del proyecto (donde está el .csproj de Infrastructure):

```bash
dotnet ef migrations add InitialCreate
```

3. Aplicar Migraciones a la Base de Datos
```bash
dotnet ef database update
```

## 🔌 Colección Postman

### 4. Postman

Importar el archivo ./PostmanCollection/PruebaTecnicaGrupoCOS.postman_collection.json

Configurar variables de entorno: (Los valores colocados mas abajo son solo de referencia)

base_url
```bash
https://localhost:44363/api
```

email
```bash
sergioo@gmail.com
```

username
```bash
Sergiooo
```

password
```bash
Sergio123
```

token
```bash
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwiZW1haWwiOiJzZXJnaW9AZ21haWwuY29tIiwibmFtZSI6IlNlcmdpbyBFc3Blam8iLCJ1bmlxdWVfbmFtZSI6IlNlcmdpb29vIiwiZXhwIjoxNzUxNjQ4NTkwLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjI1MDg3IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDoyNTA4NyJ9.RqYeqhQK9xcmiIqrfIARi-Z4B9KoymyUmnTx4aoLGek
```

refreshtoken
```bash
BVvfvEM284AmkJA7IyZOR2oxWuGrPFqVjqzJCQ7d+TQ=
```

🏃 Ejecución del Proyecto

### 5. Hora de ejecutar el proyecto

Configurar la ejecución del proyecto como IIS Express

<img width="149" alt="image" src="https://github.com/user-attachments/assets/988a1ab9-bf4d-4e51-96f0-53634be19907" />


