const http = require("http");
const fs = require("fs");
const path = require("path");

const filePath = path.join(__dirname, "Techno.json");
const RESOURCE = "tracks";

const server = http.createServer((req, res) => {
  res.setHeader("Content-Type", "application/json");

  const { method, url } = req;

  switch (method) {
    // ========================= GET =========================
    case "GET":
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

    // ========================= POST =========================

    case "POST":
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

            fs.writeFileSync(
              filePath,
              JSON.stringify(tracks, null, 4),
              "utf8"
            );

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
                error:
                  "Cuerpo de la petición no válido o error al guardar",
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

      if (url.startsWith(`/${RESOURCE}/`)) {
        try {
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
            return res.end(
              JSON.stringify({ error: "Canción no encontrada" })
            );
          }

          fs.writeFileSync(
            filePath,
            JSON.stringify(tracks, null, 4),
            "utf8"
          );

          res.statusCode = 200;
          res.end(JSON.stringify({ message: "Canción eliminada", id }));
        } catch (err) {
          console.error(err);
          res.statusCode = 500;
          res.end(
            JSON.stringify({ error: "Error al borrar la canción" })
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

server.listen(3000, () => {
  console.log("API corriendo en http://localhost:3000/tracks");
});
