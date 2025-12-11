const http = require("http");
const fs = require("fs");
const path = require("path");

const filePath = path.join(__dirname, "Techno.json");

const server = http.createServer((req, res) => {
    res.setHeader("Content-Type", "application/json");

    // GET /tracks -> devuelve el JSON completo
    if (req.url === "/tracks" && req.method === "GET") {
        try {
            const data = fs.readFileSync(filePath, "utf8");
            res.statusCode = 200;
            res.end(data);
        } catch (err) {
            console.error(err);
            res.statusCode = 500;
            res.end(JSON.stringify({ error: "No se pudo leer Techno.json" }));
        }
    }

    // POST /tracks -> añade una nueva canción al JSON
    else if (req.url === "/tracks" && req.method === "POST") {
        let body = "";

        req.on("data", chunk => {
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
                res.end(JSON.stringify({ message: "Track añadido correctamente", id: newTrack.id }));
            } catch (err) {
                console.error(err);
                res.statusCode = 400;
                res.end(JSON.stringify({ error: "Cuerpo de la petición no válido o error al guardar" }));
            }
        });
    }

    // Otros endpoints
    else {
        res.statusCode = 404;
        res.end(JSON.stringify({ error: "Endpoint no encontrado" }));
    }
});

server.listen(3000, () => {
    console.log("API corriendo en http://localhost:3000/tracks");
});
