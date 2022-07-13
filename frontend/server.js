const express = require("express");
const path = require("path");
const http = require('http');
const app = express();
const server = http.createServer(app);
const { PORT } = require("./config");

app.use("/static", express.static(path.resolve(__dirname, "static")));

app.use("/signInBundle.js", express.static("static/js/signInBundle.js"))

app.use("/signUpBundle.js", express.static("static/js/signUpBundle.js"))

app.use('/favicon.ico', express.static('static/images/friender.ico'));

app.use('/config.js', express.static('config.js'));

app.use('/userpool.js', express.static("userpool.js"));

app.use('/cookiebundle.js', express.static("/static/js/cookiebundle.js"))

app.use('/usernameBundle.js', express.static("/static/js/usernameBundle.js"))

app.use('/amazon-cognito-identity.min.js', express.static('amazon-cognito-identity.min.js'));

app.use((req, res, next) => {
  
  let accessTokenFromClient = req.rawHeaders?.[req.rawHeaders.indexOf('Cookie') + 1];

  if (req.rawHeaders.indexOf('Cookie') === -1 || !accessTokenFromClient) {

    if(req.originalUrl === '/login') {

      return res.sendFile(path.resolve(__dirname, "static/templates/login.html"));
    }

    if(req.originalUrl === '/signUp') {

      return res.sendFile(path.resolve(__dirname, "static/templates/signup.html"));
    }

    return res.sendFile(path.resolve(__dirname, "public/index.html"));
  }

  next();
});

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

app.get("/login", (req, res) => {
  res.sendFile(path.resolve(__dirname, "static/templates/login.html"));
});

app.get("/signup", (req, res) => {
  res.sendFile(path.resolve(__dirname, "static/templates/signup.html"));
});

app.get("/mySecrets", (req, res) => {
    res.sendFile(path.resolve(__dirname, "static/templates/mySecrets.html"));
  });
  
  app.get("/newSecret", (req, res) => {
    res.sendFile(path.resolve(__dirname, "static/templates/newSecret.html"));
  });
  


app.use(function (req, res) {
  res.status(404).sendFile(path.resolve(__dirname, "static/templates/404.html"));
});

//run when the client connections

server.listen(PORT || 3001, () => console.log(`Frontend server running on port ${PORT}...`));