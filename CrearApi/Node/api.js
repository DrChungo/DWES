const http = require("http");
const fs = require("fs");
const path = require("path");

/**
 * Ruta absoluta al archivo JSON que actúa como “base de datos” local.
 *
 * @return {string} Ruta del archivo Techno.json
 */
const filePath = path.join(__dirname, "Techno.json");

/**
 * Nombre del recurso base de la API (para construir endpoints).
 *
 * @return {string} Nombre del recurso
 */


const RESOURCE = "tracks";

/**
 * Servidor HTTP que expone una API sencilla para gestionar canciones.
 * Soporta:
 * - GET /tracks -> devuelve todas las canciones
 * - POST /tracks -> añade una canción
 * - DELETE /tracks/:id -> elimina una canción por ID
 *
 * @param {IncomingMessage} req Petición HTTP entrante
 * @param {ServerResponse} res Respuesta HTTP a enviar
 * @return {void} No devuelve nada (responde por HTTP)
 */



const server = http.createServer((req, res) => {
  // Indicamos que la API responde con JSON
  res.setHeader("Content-Type", "application/json");

  // Extraemos método y URL de la petición
  const { method, url } = req;

  switch (method) {
    // ========================= GET =========================
    case "GET":
      /**
       * Devuelve el JSON completo con todas las canciones.
       *
       * @param {string} url Debe ser "/tracks"
       * @return {void} Responde con el contenido de Techno.json
       */
      if (url === "/tracks") {
        try {
          const data = fs.readFileSync(filePath, "utf8");
          res.statusCode = 200;
          res.end(data);
        } catch (err) {
          console.error(err);
          res.statusCode = 500;
          res.end(JSON.stringify({ error: "No se pudo leer Techno.json" }));
        }
      } else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
      }
      break;

    // POST 
    case "POST":
      /**
       * Añade una nueva canción al JSON.
       * Espera un body en formato JSON con el objeto track.
       *
       * @param {string} url Debe ser "/tracks"
       * @return {void} Responde con éxito o error
       */
      if (url === `/${RESOURCE}`) {
        let body = "";

       
        req.on("data", (chunk) => {
          body += chunk.toString();
        });

        
        req.on("end", () => {
          try {
            const newTrack = JSON.parse(body);

           
            const data = fs.readFileSync(filePath, "utf8");
            const tracks = JSON.parse(data);

          
            tracks.push(newTrack);

          
            fs.writeFileSync(filePath, JSON.stringify(tracks, null, 4), "utf8");

            res.statusCode = 201;
            res.end(
              JSON.stringify({
                message: "Track añadido correctamente",
                id: newTrack.id,
              })
            );
          } catch (err) {
            console.error(err);
            res.statusCode = 400;
            res.end(
              JSON.stringify({
                error: "Cuerpo de la petición no válido o error al guardar",
              })
            );
          }
        });
      } else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
      }
      break;



    //  DELETE 
    case "DELETE":
      /**
       * Elimina una canción por ID.
       * Endpoint esperado: /tracks/:id
       *
       * @param {string} url Debe empezar por "/tracks/"
       * @return {void} Responde con éxito o error
       */


      if (url.startsWith(`/${RESOURCE}/`)) {
        try {
          // Extraemos el ID de la URL (/tracks/123 -> 123)
          const id = parseInt(url.split("/")[2]);

          if (isNaN(id)) {
            res.statusCode = 400;
            return res.end(JSON.stringify({ error: "ID no válido" }));
          }

         
          const data = fs.readFileSync(filePath, "utf8");
          let tracks = JSON.parse(data);

      
          const initialLength = tracks.length;
          tracks = tracks.filter((t) => t.id !== id);

         
          if (tracks.length === initialLength) {
            res.statusCode = 404;
            return res.end(JSON.stringify({ error: "Canción no encontrada" }));
          }

        
          fs.writeFileSync(filePath, JSON.stringify(tracks, null, 4), "utf8");

          res.statusCode = 200;
          res.end(JSON.stringify({ message: "Canción eliminada", id }));
        } catch (err) {
          console.error(err);
          res.statusCode = 500;
          res.end(JSON.stringify({ error: "Error al borrar la canción" }));
        }
      } else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
      }
      break;

    // DEFAULT
    default:
      /**
       * Si llega un método no soportado (PUT, PATCH, etc.),
       * devolvemos 405 Method Not Allowed.
       *
       * @param {string} method Método HTTP recibido
       * @return {void} Respuesta de error 405
       */
      res.statusCode = 405;
      res.end(JSON.stringify({ error: "Método no permitido" }));
      break;
  }
});

/**
 * Arranca el servidor en el puerto 3000.
 *
 * @return {void} Muestra un mensaje por consola al iniciar
 */
server.listen(3000, () => {
  console.log("API corriendo en http://localhost:3000/tracks");
});
