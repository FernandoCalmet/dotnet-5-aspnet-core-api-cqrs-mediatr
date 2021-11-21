# 🦄 C# ASP.NET CORE 5 WEB API CQRS MEDIATR

[![Github][github-shield]][github-url]
[![Kofi][kofi-shield]][kofi-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
[![Khanakat][khanakat-shield]][khanakat-url]

## TABLA DE CONTENIDO

* [Acerca del proyecto](#acerca-del-proyecto)
* [Características](#características)
* [Instalación](#instalación)
* [Resumen teórico](#resumen-teórico)
* [Dependencias](#dependencias)
* [Licencia](#licencia)

## 🔥 ACERCA DEL PROYECTO

Este proyecto es una muestra de una aplicación web API implementando el patron de diseño CQRS (Command Query Responsibility Segregation) y MediatR, este proyecto ayuda a diseñar una solución que se adapta a arquitecturas cebolla (Onion). Se utilizo ``ASP.NET Core 5 Web API`` con C# + Entity Framework Core + SQLServer.

## ✔️ CARACTERÍSTICAS

- [x] Database migrations
- [x] CQRS Pattern
- [x] Mediador Pattern

## ⚙️ INSTALACIÓN

Clonar el repositorio.

```bash
gh repo clone FernandoCalmet/CS-ASPNET-Core-API-CQRS-MediatR
```

Crear la migración de base de datos

```bash
update-database
```

Ejecutar aplicación.

```bash
dotnet run
```

## 📓 RESUMEN TEÓRICO

### ¿Qué Es CQRS?

CQRS, Command Query Responsibility Segregation es un patrón de diseño que separa las operaciones de lectura y escritura de una fuente de datos. Aquí, el comando se refiere a un comando de base de datos, que puede ser una operación Insertar/Actualizar o Eliminar, mientras que Consulta significa Consultar datos de una fuente. Básicamente, separa las preocupaciones en términos de lectura y escritura, lo que tiene mucho sentido. Este patrón se originó a partir del principio de separación de comandos y consultas ideado por Bertrand Meyer . Se define en Wikipedia de la siguiente manera.

> Establece que cada método debe ser un comando que realiza una acción o una consulta que devuelve datos a la persona que llama, pero no ambos. En otras palabras, hacer una pregunta no debería cambiar la respuesta. Más formalmente, los métodos deben devolver un valor solo si son referencialmente transparentes y, por lo tanto, no tienen efectos secundarios. (Wikipedia)

El problema con los patrones arquitectónicos tradicionales es que se utiliza el mismo modelo de datos o DTO para consultar y actualizar una fuente de datos. Este puede ser el enfoque a seguir cuando su aplicación está relacionada solo con operaciones CRUD y nada más. Pero cuando sus requisitos de repente comienzan a volverse complejos, este enfoque básico puede resultar un desastre.

En aplicaciones prácticas, siempre hay una discrepancia entre las formas de lectura y escritura de datos, como las propiedades adicionales que puede necesitar actualizar. Las operaciones paralelas pueden incluso provocar la pérdida de datos en el peor de los casos. Eso significa que se quedará atascado con un solo objeto de transferencia de datos durante toda la vida útil de la aplicación, a menos que elija introducir otro DTO, lo que a su vez puede romper la arquitectura de su aplicación.

La idea con CQRS es permitir que una aplicación funcione con diferentes modelos. En pocas palabras, tiene un modelo que tiene los datos necesarios para actualizar un registro, otro modelo para insertar un registro y otro para consultar un registro. Esto le brinda flexibilidad con escenarios variados y complejos. No tiene que depender de un solo DTO para todas las operaciones CRUD mediante la implementación de CQRS.

![CQRS](.img/cqrs.png)

### Ventajas De CQRS

Hay muchas ventajas al utilizar el patrón CQRS para su aplicación. Algunos de ellos son los siguientes.

#### Objetos de transferencia de datos optimizados

Gracias al enfoque segregado de este patrón, ya no necesitaremos esas clases de modelo complejas dentro de nuestra aplicación. Más bien, tenemos un modelo por operación de datos que nos brinda toda la flexibilidad del mundo.

#### Altamente escalable

Tener control sobre los modelos de acuerdo con el tipo de operaciones de datos hace que su aplicación sea altamente escalable a largo plazo.

#### Desempeño mejorado

Prácticamente hablando, siempre hay 10 veces más operaciones de lectura en comparación con la operación de escritura. Con este patrón, podría acelerar el rendimiento de sus operaciones de lectura al introducir un caché o NOSQL Db como Redis o Mongo. El patrón CQRS admitirá este uso desde el primer momento, no tendría que romperse la cabeza tratando de implementar dicho mecanismo de caché.

#### Operaciones paralelas seguras

Dado que tenemos modelos dedicados por operación, no hay posibilidad de pérdida de datos al realizar operaciones paralelas.

### Contras De CQRS

#### Complejidad agregada y más código

Lo único que puede preocupar a algunos programadores es que se trata de un patrón que exige código. En otras palabras, terminará con al menos 3 o 4 veces más líneas de código de lo que normalmente tendría. Pero todo tiene un precio. Esto, en mi opinión, es un pequeño precio a pagar mientras se obtienen las increíbles funciones y posibilidades con el patrón.

## 📥 DEPENDENCIAS

- [FluentValidation](https://www.nuget.org/packages/FluentValidation/) : FluentValidation es una biblioteca de validación para .NET que utiliza una interfaz fluida y expresiones lambda para crear reglas de validación fuertemente tipadas.
- [FluentValidation.DependencyInjectionExtensions](https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions/) : Extensiones de inyección de dependencia para FluentValidation.
- [MediatR](https://www.nuget.org/packages/MediatR/) : Implementación de mediador simple y poco ambiciosa en .NET.
- [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection/) : Extensiones de MediatR para ASP.NET Core.
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/) : Entity Framework Core es un mapeador de bases de datos de objetos moderno para .NET. Admite consultas LINQ, seguimiento de cambios, actualizaciones y migraciones de esquemas. EF Core funciona con SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL y otras bases de datos a través de una API de complemento de proveedor.
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/) : Componentes compartidos en tiempo de diseño para las herramientas de Entity Framework Core.
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) : Proveedor de base de datos de Microsoft SQL Server para Entity Framework Core.
- [Microsoft.VisualStudio.Web.CodeGeneration.Design](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/) : Herramienta de generación de código para ASP.NET Core. Contiene el comando dotnet-aspnet-codegenerator que se usa para generar controladores y vistas.
- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/) : Herramientas Swagger para documentar API creadas en ASP.NET Core.
- [Swashbuckle.AspNetCore.Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/) : Middleware para exponer los puntos finales Swagger JSON de las API creadas en ASP.NET Core.

## 📄 LICENCIA

Este proyecto está bajo la Licencia (Licencia MIT) - mire el archivo [LICENSE](LICENSE) para más detalles.

## ⭐️ DAME UNA ESTRELLA

Si esta Implementación le resultó útil o la utilizó en sus Proyectos, déle una estrella. ¡Gracias! O, si te sientes realmente generoso, [¡Apoye el proyecto con una pequeña contribución!](https://ko-fi.com/fernandocalmet).

<!--- reference style links --->
[github-shield]: https://img.shields.io/badge/-@fernandocalmet-%23181717?style=flat-square&logo=github
[github-url]: https://github.com/fernandocalmet
[kofi-shield]: https://img.shields.io/badge/-@fernandocalmet-%231DA1F2?style=flat-square&logo=kofi&logoColor=ff5f5f
[kofi-url]: https://ko-fi.com/fernandocalmet
[linkedin-shield]: https://img.shields.io/badge/-fernandocalmet-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/fernandocalmet
[linkedin-url]: https://www.linkedin.com/in/fernandocalmet
[khanakat-shield]: https://img.shields.io/badge/khanakat.com-brightgreen?style=flat-square
[khanakat-url]: https://khanakat.com