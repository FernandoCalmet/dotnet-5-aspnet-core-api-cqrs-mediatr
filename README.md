# 馃 C# ASP.NET CORE 5 WEB API CQRS MEDIATR

[![Github][github-shield]][github-url]
[![Kofi][kofi-shield]][kofi-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
[![Khanakat][khanakat-shield]][khanakat-url]

## TABLA DE CONTENIDO

* [Acerca del proyecto](#acerca-del-proyecto)
* [Caracter铆sticas](#caracter铆sticas)
* [Instalaci贸n](#instalaci贸n)
* [Resumen te贸rico](#resumen-te贸rico)
* [Dependencias](#dependencias)
* [Licencia](#licencia)

## 馃敟 ACERCA DEL PROYECTO

Este proyecto es una muestra de una aplicaci贸n web API implementando el patron de dise帽o CQRS (Command Query Responsibility Segregation) y MediatR, este proyecto ayuda a dise帽ar una soluci贸n que se adapta a arquitecturas cebolla (Onion). Se utilizo ``ASP.NET Core 5 Web API`` con C# + Entity Framework Core + SQLServer.

## 鉁旓笍 CARACTER脥STICAS

- [x] Database migrations
- [x] CQRS Pattern
- [x] Mediador Pattern
- [x] Register and Validation Pipeline Behaviour

## 鈿欙笍 INSTALACI脫N

Clonar el repositorio.

```bash
gh repo clone FernandoCalmet/dotnet-5-aspnet-core-api-cqrs-mediatr
```

Crear la migraci贸n de base de datos

```bash
update-database
```

Ejecutar aplicaci贸n.

```bash
dotnet run
```

## 馃摀 RESUMEN TE脫RICO

### 驴Qu茅 Es CQRS?

CQRS, Command Query Responsibility Segregation es un patr贸n de dise帽o que separa las operaciones de lectura y escritura de una fuente de datos. Aqu铆, el comando se refiere a un comando de base de datos, que puede ser una operaci贸n Insertar/Actualizar o Eliminar, mientras que Consulta significa Consultar datos de una fuente. B谩sicamente, separa las preocupaciones en t茅rminos de lectura y escritura, lo que tiene mucho sentido. Este patr贸n se origin贸 a partir del principio de separaci贸n de comandos y consultas ideado por Bertrand Meyer . Se define en Wikipedia de la siguiente manera.

> Establece que cada m茅todo debe ser un comando que realiza una acci贸n o una consulta que devuelve datos a la persona que llama, pero no ambos. En otras palabras, hacer una pregunta no deber铆a cambiar la respuesta. M谩s formalmente, los m茅todos deben devolver un valor solo si son referencialmente transparentes y, por lo tanto, no tienen efectos secundarios. (Wikipedia)

El problema con los patrones arquitect贸nicos tradicionales es que se utiliza el mismo modelo de datos o DTO para consultar y actualizar una fuente de datos. Este puede ser el enfoque a seguir cuando su aplicaci贸n est谩 relacionada solo con operaciones CRUD y nada m谩s. Pero cuando sus requisitos de repente comienzan a volverse complejos, este enfoque b谩sico puede resultar un desastre.

En aplicaciones pr谩cticas, siempre hay una discrepancia entre las formas de lectura y escritura de datos, como las propiedades adicionales que puede necesitar actualizar. Las operaciones paralelas pueden incluso provocar la p茅rdida de datos en el peor de los casos. Eso significa que se quedar谩 atascado con un solo objeto de transferencia de datos durante toda la vida 煤til de la aplicaci贸n, a menos que elija introducir otro DTO, lo que a su vez puede romper la arquitectura de su aplicaci贸n.

La idea con CQRS es permitir que una aplicaci贸n funcione con diferentes modelos. En pocas palabras, tiene un modelo que tiene los datos necesarios para actualizar un registro, otro modelo para insertar un registro y otro para consultar un registro. Esto le brinda flexibilidad con escenarios variados y complejos. No tiene que depender de un solo DTO para todas las operaciones CRUD mediante la implementaci贸n de CQRS.

![CQRS](.img/cqrs.png)

### Ventajas De CQRS

Hay muchas ventajas al utilizar el patr贸n CQRS para su aplicaci贸n. Algunos de ellos son los siguientes.

#### Objetos de transferencia de datos optimizados

Gracias al enfoque segregado de este patr贸n, ya no necesitaremos esas clases de modelo complejas dentro de nuestra aplicaci贸n. M谩s bien, tenemos un modelo por operaci贸n de datos que nos brinda toda la flexibilidad del mundo.

#### Altamente escalable

Tener control sobre los modelos de acuerdo con el tipo de operaciones de datos hace que su aplicaci贸n sea altamente escalable a largo plazo.

#### Desempe帽o mejorado

Pr谩cticamente hablando, siempre hay 10 veces m谩s operaciones de lectura en comparaci贸n con la operaci贸n de escritura. Con este patr贸n, podr铆a acelerar el rendimiento de sus operaciones de lectura al introducir un cach茅 o NOSQL Db como Redis o Mongo. El patr贸n CQRS admitir谩 este uso desde el primer momento, no tendr铆a que romperse la cabeza tratando de implementar dicho mecanismo de cach茅.

#### Operaciones paralelas seguras

Dado que tenemos modelos dedicados por operaci贸n, no hay posibilidad de p茅rdida de datos al realizar operaciones paralelas.

### Contras De CQRS

#### Complejidad agregada y m谩s c贸digo

Lo 煤nico que puede preocupar a algunos programadores es que se trata de un patr贸n que exige c贸digo. En otras palabras, terminar谩 con al menos 3 o 4 veces m谩s l铆neas de c贸digo de lo que normalmente tendr铆a. Pero todo tiene un precio. Esto, en mi opini贸n, es un peque帽o precio a pagar mientras se obtienen las incre铆bles funciones y posibilidades con el patr贸n.

### Pipelines : Descripci贸n General

驴Qu茅 sucede internamente cuando env铆as una solicitud a cualquier aplicaci贸n? Idealmente devuelve la respuesta. Pero hay una cosa de la que quiz谩s ya est茅 enterado: Pipelines. Ahora, estas solicitudes y respuestas viajan hacia adelante y hacia atr谩s a trav茅s de Pipelines en ASP.NET Core. Entonces, cuando env铆a una solicitud, el mensaje de solicitud pasa del usuario a trav茅s de una canalizaci贸n hacia la aplicaci贸n, donde realiza la operaci贸n solicitada con el mensaje de solicitud. Una vez hecho esto, la aplicaci贸n devuelve el mensaje como respuesta a trav茅s de la canalizaci贸n hacia el usuario. 驴Cons铆guelo? Por lo tanto, estas canalizaciones son completamente conscientes de cu谩l es la solicitud o la respuesta. Este tambi茅n es un concepto muy importante al aprender sobre Middlewares en ASP.NET Core.

Digamos que quiero validar el objeto de solicitud. 驴Como lo harias? B谩sicamente, escribir铆a las l贸gicas de validaci贸n que se ejecutan despu茅s de que la solicitud haya llegado al final de la canalizaci贸n hacia la aplicaci贸n. Eso significa que est谩 validando la solicitud solo despu茅s de que haya llegado al interior de la aplicaci贸n. Aunque este es un buen enfoque, pens茅moslo. 驴Por qu茅 necesita adjuntar las l贸gicas de validaci贸n a la aplicaci贸n, cuando ya puede validar las solicitudes entrantes incluso antes de que llegue a cualquiera de las l贸gicas de la aplicaci贸n? 驴Tiene sentido?

Un mejor enfoque ser铆a conectar de alguna manera sus l贸gicas de validaci贸n dentro de la canalizaci贸n, de modo que el flujo se convierta en como el usuario env铆a una solicitud a trav茅s de la canalizaci贸n (l贸gicas de validaci贸n aqu铆), si la solicitud es v谩lida, presione las l贸gicas de la aplicaci贸n, de lo contrario lanza una excepci贸n de validaci贸n. Esto tiene mucho sentido en t茅rminos de eficiencia, 驴verdad? 驴Por qu茅 atacar la aplicaci贸n con datos no v谩lidos, cuando antes pod铆a filtrarlos?

Esto no solo es aplicable para validaciones, sino para otras operaciones como registro, seguimiento de rendimiento y mucho m谩s. Puedes ser realmente creativo al respecto.

### Comportamiento De La Tuber铆a De MediatR

Volviendo a MediatR , se necesita un enfoque m谩s de canalizaci贸n en el que sus consultas, comandos y respuestas fluyen a trav茅s de una configuraci贸n de canalizaci贸n de MediatR.

Perm铆tanme presentarles los comportamientos de MediatR. MediatR Pipeline Behavior se puso a disposici贸n de la versi贸n 3 de esta incre铆ble biblioteca.

Sabemos que estas solicitudes o comandos de MediatR son como el primer contacto dentro de nuestra aplicaci贸n, as铆 que 驴por qu茅 no adjuntar algunos servicios en su Pipleline?

Al hacer esto, podremos ejecutar servicios / l贸gicas como validaciones incluso antes de que los Manejadores de Comandos o Consultas lo sepan. De esta manera, enviaremos solo las solicitudes v谩lidas necesarias a la Implementaci贸n de CQRS. El registro y la validaci贸n mediante este comportamiento de canalizaci贸n de MediatR son algunas de las implementaciones comunes.

## 馃摜 DEPENDENCIAS

- [FluentValidation](https://www.nuget.org/packages/FluentValidation/) : FluentValidation es una biblioteca de validaci贸n para .NET que utiliza una interfaz fluida y expresiones lambda para crear reglas de validaci贸n fuertemente tipadas.
- [FluentValidation.DependencyInjectionExtensions](https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions/) : Extensiones de inyecci贸n de dependencia para FluentValidation.
- [MediatR](https://www.nuget.org/packages/MediatR/) : Implementaci贸n de mediador simple y poco ambiciosa en .NET.
- [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection/) : Extensiones de MediatR para ASP.NET Core.
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/) : Entity Framework Core es un mapeador de bases de datos de objetos moderno para .NET. Admite consultas LINQ, seguimiento de cambios, actualizaciones y migraciones de esquemas. EF Core funciona con SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL y otras bases de datos a trav茅s de una API de complemento de proveedor.
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/) : Componentes compartidos en tiempo de dise帽o para las herramientas de Entity Framework Core.
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) : Proveedor de base de datos de Microsoft SQL Server para Entity Framework Core.
- [Microsoft.VisualStudio.Web.CodeGeneration.Design](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/) : Herramienta de generaci贸n de c贸digo para ASP.NET Core. Contiene el comando dotnet-aspnet-codegenerator que se usa para generar controladores y vistas.
- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/) : Herramientas Swagger para documentar API creadas en ASP.NET Core.
- [Swashbuckle.AspNetCore.Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/) : Middleware para exponer los puntos finales Swagger JSON de las API creadas en ASP.NET Core.

## 馃搫 LICENCIA

Este proyecto est谩 bajo la Licencia (Licencia MIT) - mire el archivo [LICENSE](LICENSE) para m谩s detalles.

## 猸愶笍 DAME UNA ESTRELLA

Si esta Implementaci贸n le result贸 煤til o la utiliz贸 en sus Proyectos, d茅le una estrella. 隆Gracias! O, si te sientes realmente generoso, [隆Apoye el proyecto con una peque帽a contribuci贸n!](https://ko-fi.com/fernandocalmet).

<!--- reference style links --->
[github-shield]: https://img.shields.io/badge/-@fernandocalmet-%23181717?style=flat-square&logo=github
[github-url]: https://github.com/fernandocalmet
[kofi-shield]: https://img.shields.io/badge/-@fernandocalmet-%231DA1F2?style=flat-square&logo=kofi&logoColor=ff5f5f
[kofi-url]: https://ko-fi.com/fernandocalmet
[linkedin-shield]: https://img.shields.io/badge/-fernandocalmet-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/fernandocalmet
[linkedin-url]: https://www.linkedin.com/in/fernandocalmet
[khanakat-shield]: https://img.shields.io/badge/khanakat.com-brightgreen?style=flat-square
[khanakat-url]: https://khanakat.com
