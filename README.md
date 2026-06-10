# Sistema de Gestión de Proveedores - TEKUS S.A.S.

Aplicación web empresarial construida con arquitectura **Clean Architecture (DDD)** usando en el backend C# (.NET 8) y un diseño moderno con **Angular 21** en el frontend.

## 🚀 Funcionalidades Principales

1. **Dashboard Analítico**
   - Visualización general de los Proveedores Totales, Servicios Totales y total de Países aliados.
   - Tabla estadística de Servicios prestados agrupados por País.
   - Tabla estadística de Proveedores agrupados por País.

2. **Gestión de Proveedores**
   - Listado paginado de proveedores.
   - Búsqueda en tiempo real por Nombre o NIT.
   - Ordenamiento ascendente/descendente por Nombre, NIT o País.
   - Creación de nuevos proveedores.
   - Eliminación de proveedores.
   - Visualización del número de servicios asociados a cada proveedor.

3. **Gestión de Servicios**
   - Listado paginado de servicios.
   - Búsqueda por nombre del servicio.
   - Ordenamiento por Nombre o Tarifa por Hora (Hourly Rate).
   - Creación de nuevos servicios.
   - Eliminación de servicios.
   - Visualización del número de proveedores que ofrecen cada servicio.

4. **Autenticación y Seguridad**
   - Sistema de Login seguro basado en **JSON Web Tokens (JWT)**.
   - Cifrado de contraseñas utilizando **BCrypt**.
   - Protección de rutas en el frontend (AuthGuard) y en el backend (`[Authorize]`).

## 🔄 Flujo del Programa

1. **Ingreso:** El usuario accede a la plataforma y se presenta la pantalla de Login.
2. **Autenticación:** El frontend envía las credenciales al endpoint `POST /api/auth/login`. El backend verifica el email y el hash BCrypt, generando un token JWT válido por 60 minutos si son correctos.
3. **Navegación:** Una vez autenticado, el usuario es redirigido al **Dashboard**, donde se consultan múltiples endpoints (`GET /api/dashboard`) inyectando el token JWT en las cabeceras HTTP mediante un Interceptor de Angular.
4. **Operaciones CRUD:** Desde el menú lateral, el usuario navega hacia las vistas de Proveedores o Servicios. Al realizar acciones (crear, buscar, eliminar), el frontend se comunica con los Controladores de la API (ej. `ProvidersController`), los cuales utilizan el patrón **CQRS con MediatR** para delegar la lógica a los Casos de Uso (Commands/Queries) en la capa de Aplicación, manteniendo los controladores limpios.
5. **Cierre de sesión:** El usuario puede cerrar sesión en cualquier momento, lo que destruye el token en el cliente y lo redirige al Login.

## 🔗 Asociación de Servicios y Proveedores

La asociación entre Proveedores y Servicios se maneja de forma segura bajo los principios de **Domain-Driven Design (DDD)**. 
- **En la API (Backend):** Existen los endpoints `POST /api/providers/{id}/services` para asociar un servicio a un proveedor (con la posibilidad de definir una tarifa personalizada) y `DELETE /api/providers/{id}/services/{serviceId}` para remover la asociación.
- **En la Interfaz (Frontend):** Actualmente, la interfaz permite ver la *cantidad* de relaciones que tiene cada entidad. La vista avanzada para gestionar las asociaciones directamente desde el UI (ej. un modal con checkboxes) es el siguiente paso evolutivo natural del producto, sin embargo la infraestructura backend ya soporta la lógica al 100%.

## 🌱 Semilla de Creación de Base de Datos (Seeder)

El proyecto está diseñado para ser portátil y *Plug & Play*. Utiliza **EF Core InMemoryDatabase**.
Cada vez que el backend se inicializa vacío, se ejecuta el archivo `DatabaseSeeder.cs`, el cual realiza las siguientes acciones de forma secuencial:
1. **Administrador:** Crea el usuario por defecto (`admin@tekus.com` / `Admin123!`).
2. **Servicios:** Inserta 8 servicios predeterminados (Ej. "Desarrollo Web Frontend", "Descarga espacial de contenidos").
3. **Proveedores:** Inserta 10 proveedores ficticios distribuidos en varios países (Colombia, USA, España, México, Argentina).
4. **Relaciones:** Asocia programáticamente los servicios a los proveedores (utilizando el método de dominio `provider.AddService(...)`) y rastrea explícitamente estas entidades de unión para que la base de datos en memoria las detecte y devuelva correctamente en el Dashboard.

---

## ⚙️ Guía de Configuración (Desde otra PC)

Asumiendo que la computadora destino tiene instalados **.NET 8 SDK** y **Node.js (npm)**, estos son los pasos exactos para levantar el proyecto:

### 1. Levantar el Backend (.NET 8)
El backend no requiere instalación de SQL Server ni bases de datos externas, ya que utiliza una base de datos en memoria preconfigurada.

1. Abre una terminal.
2. Navega a la carpeta del backend:
   ```bash
   cd PTTekus/backend
   ```
3. Restaura los paquetes, compila y ejecuta el servidor:
   ```bash
   dotnet run --project src/Tekus.API/Tekus.API.csproj
   ```
4. Verás en la terminal que la aplicación está escuchando en el puerto local (usualmente `http://localhost:5062`). La API y Swagger estarán activos.

### 2. Levantar el Frontend (Angular 21)
El frontend se comunicará automáticamente con la URL de la API especificada en sus variables de entorno.

1. Abre una **nueva** terminal (sin cerrar la del backend).
2. Navega a la carpeta del frontend:
   ```bash
   cd PTTekus/frontend
   ```
3. Instala todas las dependencias necesarias:
   ```bash
   npm install
   ```
4. Inicia el servidor de desarrollo de Angular:
   ```bash
   npm start
   ```
5. Abre tu navegador web y visita: `http://localhost:4200`
6. Inicia sesión con las credenciales de la semilla:
   - **Email:** admin@tekus.com
   - **Password:** Admin123!

# By: Daniel Ramirez Agudelo
# Contact: 3332835153