const http = require("http");
const fs = require("fs");

const server = http.createServer((req, res) => {
    res.setHeader("Content-Type", "application/json");

    if (req.url === "/tracks") {
        const data = fs.readFileSync("Techno.json", "utf8");
        res.write(data);
        res.end();
    } else {
        res.write(JSON.stringify({ error: "Endpoint no encontrado" }));
        res.end();
    }
});

server.listen(3000, () => {
    console.log("API corriendo en http://localhost:3000/tracks");
});