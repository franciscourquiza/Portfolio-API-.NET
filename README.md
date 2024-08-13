# Backend Portfolio API + JWT Tokens + SQL Database + EmailService

# Características del sistema.
- Implementación de la arquitectura limpia en 4 capas (WEB, Application, Domain, Infrastructure).
- Patrón de capa Servicio para la lógica de negocio.
- Patrón Repositorio para el manejo de la base de datos.
- Patrón Generics implementado.
- Patrón DTO para crear, listar y actualizar datos (cada modelo tiene sus DTOs).
- AutoMapper para la mapear de manera más eficiente los DTOs a modelos y separar la lógica de mapeo del servicio.
- Inyección de dependencia para servicios, repositorios, mappers, etc.
- Validación de modelos DTOs con Regular Expressions.
- Middleware para gestionar todas las excepciones (deja limpio el controlador).
- Autenticado mediante JWT Tokens.
- Roles de User, Admin y SuperAdmin diferenciados.
- Envío de correos electrónicos para Resetear Contraseñas.
- Model First (creación, migración y versionamiento de la base de datos a partir del modelo).
- Interfaces de Servicio y repositorio.
- Cors activada para petición desde cualquier url.
- SQLite como Base de Datos.

# Auth para autenticación y autorización
El sistema de Auth cuenta de controlador, servicio y repositorio, y permite gestionar permisos y roles de acceso a lo endpoints contando con:
- Login.- Genera Token de Acceso.
- Request Password Reset.- Realiza una petición con el correo para resetear contraseña, se envía un correo de verificación.
- Reset Password.- Endpoint de verificación para el reseteo de password, genera una nueva contraseña aleatoria y la envía por correo.

![](https://i.ibb.co/7vLDNtb/AUTH.png)

# User
El sistema permite una gestión completa de usuarios solicitando permisos de Administrador o SuperAdmin cuando es requerido.
- Obtener usuario por Nombre (Acceso sin roles requeridos).
- Obtener usuario por Email (Acceso sin roles requeridos).
- Obtener un listado de todos los usuarios (Requiere rol de Admin o SuperAdmin).
- Crear cuenta.- Permite crear una cuenta con los siguientes datos. (No requiere ningun rol).
    - Nombre
    - Email(Primary Key)
    - Contraseña
    - Resumen de perfil
    - Edad
    - País
    - Estado
    - Ciudad
    - Dirección
    - Número de teléfono
    - Link de LinkedIn
    - Link de GitHub
    - Rol de Usuario
- Editar cuenta (Los usuarios solamente pueden editar sus propias cuentas, esta validación se realiza tomando el email).
- Eliminar cuenta por Email (Acción que solamente el SuperAdmin puede llevar a cabo).

![](https://i.ibb.co/8YDN5gC/USER.png)

# Admin
Gestión de Admins llevado a cabo únicamente por el SuperAdmin:
- Obtener Admin por Nombre.
- Obtener Admin por Email.
- Obtener todos los Admins.
- Crear un Admin.
- Editar un Admin.
- Eliminar Admin por Email.

![](https://i.ibb.co/tX4srgf/ADMIN-copia.png)

# WorkExperience
Permite cargar experiencias laborales a los usuarios (Se debe estar logeado para poder acceder a los métodos):
- Obtener experiencia laboral por título.
- Obtener experiencia laboral por Id.
- Obtener todas las experiencias laborales.
- Crear una experiencia laboral para la cuenta que está logeada:
    - Id (Primary Key)
    - Título
    - Descripción
    - Fecha de inicio
    - Fecha de finalización
    - UserEmail (Foreign Key a la tabla Users)
- Editar experiencia laboral por título (Tiene que ser propia al usuario logeado).
- Eliminar experiencia laboral por título (Tiene que ser propia al usuario logeado).

![](https://i.ibb.co/h7FvWds/WORK.png)

# Proyect
Permite cargar proyectos personales a los usuarios (Se debe estar logeado para poder acceder a los métodos):
- Obtener proyecto personal por título.
- Obtener proyecto personal por Id.
- Obtener todas los proyectos personales.
- Crear un proyecto personal para la cuenta que está logeada:
    - Id (Primary Key)
    - Título
    - Descripción
    - Fecha de inicio
    - Fecha de finalización
    - UserEmail (Foreign Key a la tabla Users)
- Editar proyecto personal por título (Tiene que ser propia al usuario logeado).
- Eliminar proyecto personal por título (Tiene que ser propia al usuario logeado).

![](https://i.ibb.co/NNZDdLt/PROYECT.png)

# Education
Permite cargar educaciones a los usuarios (Se debe estar logeado para poder acceder a los métodos):
- Obtener educación por título.
- Obtener educación por Id.
- Obtener todas las educaciones.
- Crear una educación para la cuenta que está logeada:
    - Id (Primary Key)
    - Título
    - Descripción
    - Fecha de inicio
    - Fecha de finalización
    - UserEmail (Foreign Key a la tabla Users)
- Editar educación por título (Tiene que ser propia al usuario logeado).
- Eliminar educación por título (Tiene que ser propia al usuario logeado).

![](https://i.ibb.co/zVnNKRX/EDUCATION.png)

# DTO para la administración de entrada y salida de datos

![](https://i.ibb.co/THByn4n/DTOS.png)

# Dependencias
- Entity Framework
- Entity Framwork SQLite
- Entity Framwork Core
- Entity Framwork Tools
- AutoMapper
- FluentValidation
- MailKit
- Jwt Bearer
- Tokens
- Newtonsoft Json


Desarrollado por Francisco Urquiza.
