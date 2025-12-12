# Instalacion
descargar el progrma, descomprimir el ZIP. una vez descomprimido abre la carpeta y haz doble click en run.bat ,eso ejecurata el
programa. Recuerda que tienes que tenes descargado .NET.

# Descripción

Este proyecto consiste en crear una API sencilla en Node.js que gestiona canciones de música techno almacenadas en un archivo JSON.
Desde una aplicación de consola en C#, se consume esta API para trabajar con las canciones.

El objetivo es simular el uso de una API externa real y practicar el manejo de datos, JSON y peticiones HTTP.


# Funcionamiento

La aplicación en C# se conecta a la API y permite:

Jugar a un mini juego de entrenamiento auditivo

Añadir nuevas canciones

Eliminar canciones existentes

Listar todas las canciones disponibles

Todas las acciones se realizan desde un menú por consola.

# API (Node.js)

La API trabaja sobre un archivo Techno.json y ofrece estos endpoints:

GET /tracks → devuelve todas las canciones

POST /tracks → añade una canción

DELETE /tracks/:id → elimina una canción por ID


# Tecnologías usadas

Node.js (API)

C# (.NET)

HttpClient

JSON


Organización del código por capas
