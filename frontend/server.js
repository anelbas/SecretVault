const express = require("express");
const path = require("path");
const http = require('http');
const app = express();
const server = http.createServer(app);
const { PORT } = require("./config")

app.use("/static", express.static(path.resolve(__dirname, "static")));

app.use('/favicon.ico', express.static('static/images/friender.ico'));

app.use('/config.js', express.static('config.js'));

app.get("/", (req, res) => {
    res.sendFile(path.resolve(__dirname, "public/index.html"));
});

app.get("/publicSecrets", (req, res) => {
    res.sendFile(path.resolve(__dirname, "static/templates/publicSecrets.html"));
});

app.get("/mySecrets", (req, res) => {
    res.sendFile(path.resolve(__dirname, "static/templates/mySecrets.html"));
});

app.get("/newSecret", (req, res) => {
    res.sendFile(path.resolve(__dirname, "static/templates/newSecret.html"));
});


app.use(function(req,res){
    res.status(404).sendFile(path.resolve(__dirname, "static/templates/404.html"));
});

//run when the client connections

server.listen(PORT || 3001, () => console.log(`Frontend server running on port ${PORT}...`));