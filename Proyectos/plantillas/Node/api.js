const http = require("http");
const fs = require("fs");
const path = require("path");

// CAMBIAR → aquí va el nombre del archivo JSON que usará la API como “base de datos”
const filePath = path.join(__dirname, "CAMBIAR.json");

// CAMBIAR → nombre del recurso principal que aparecerá en la URL
// Ejemplo: "tracks", "users", "products"
const RESOURCE = "CAMBIAR";

//Cambiar el puerto
const PORT= 3000;

const server = http.createServer((req, res) => {
  res.setHeader("Content-Type", "application/json");

  const { method, url } = req;

  switch (method) {
    // ========================= GET =========================
    case "GET":
      // CAMBIAR → endpoint principal del recurso
      if (url === `/${RESOURCE}`) {
        try {
          const data = fs.readFileSync(filePath, "utf8");
          res.statusCode = 200;
          res.end(data);
        } catch (err) {
          res.statusCode = 500;
          res.end(
            JSON.stringify({
              // CAMBIAR → mensaje de error acorde al recurso
              error: "CAMBIAR error al leer el archivo",
            })
          );
        }
      } else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
      }
      break;

    // ========================= POST =========================
    case "POST":
      // CAMBIAR → endpoint donde se añadirán nuevos elementos
      if (url === `/${RESOURCE}`) {
        let body = "";

        req.on("data", (chunk) => {
          body += chunk.toString();
        });

        req.on("end", () => {
          try {
            // CAMBIAR → estructura del objeto que recibe la API
            // (campos, nombres, validaciones, etc.)
            const newItem = JSON.parse(body);

            const data = fs.readFileSync(filePath, "utf8");
            const items = JSON.parse(data);

            items.push(newItem);

            fs.writeFileSync(
              filePath,
              JSON.stringify(items, null, 4),
              "utf8"
            );

            res.statusCode = 201;
            res.end(
              JSON.stringify({
                // CAMBIAR → mensaje de confirmación según el recurso
                message: "CAMBIAR añadido correctamente",
                id: newItem.id,
              })
            );
          } catch (err) {
            res.statusCode = 400;
            res.end(
              JSON.stringify({
                // CAMBIAR → mensaje cuando el body es incorrecto
                error: "CAMBIAR error en el cuerpo de la petición",
              })
            );
          }
        });
      } else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
      }
      break;

    // ========================= DELETE =========================
    case "DELETE":
      // CAMBIAR → Cambiar lo de arriba "RESOURCE" para cambiar todo el archivo
      if (url.startsWith(`/${RESOURCE}/`)) {
        try {
          const id = parseInt(url.split("/")[2]);

          if (isNaN(id)) {
            res.statusCode = 400;
            return res.end(
              JSON.stringify({
                // CAMBIAR → mensaje si el id no es válido
                error: "CAMBIAR ID no válido",
              })
            );
          }

          const data = fs.readFileSync(filePath, "utf8");
          let items = JSON.parse(data);

          const initialLength = items.length;
          items = items.filter((i) => i.id !== id);

          if (items.length === initialLength) {
            res.statusCode = 404;
            return res.end(
              JSON.stringify({
                // CAMBIAR → mensaje si no se encuentra el elemento
                error: "CAMBIAR no encontrado",
              })
            );
          }

          fs.writeFileSync(
            filePath,
            JSON.stringify(items, null, 4),
            "utf8"
          );

          res.statusCode = 200;
          res.end(
            JSON.stringify({
              // CAMBIAR → mensaje de borrado correcto
              message: "CAMBIAR eliminado correctamente",
              id,
            })
          );
        } catch (err) {
          res.statusCode = 500;
          res.end(
            JSON.stringify({
              // CAMBIAR → mensaje de error interno
              error: "CAMBIAR error al borrar",
            })
          );
        }
      } else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
      }
      break;

    // ========================= DEFAULT =========================
    default:
      res.statusCode = 405;
      res.end(JSON.stringify({ error: "Método no permitido" }));
      break;
  }
});

// CAMBIAR → puerto donde escuchará el servidor. CAMBIAR ARRIBA EN PORT

server.listen(PORT, () => {
  // CAMBIAR → mensaje informativo al arrancar la API
  console.log(`API corriendo en http://localhost${PORT}/${RESOURCE}`);
});
