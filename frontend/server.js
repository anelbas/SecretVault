const express = require("express");
const path = require("path");
const http = require('http');
const app = express();
const server = http.createServer(app);
const { PORT } = require("./config");
const CognitoExpress =require('cognito-express');
const exp = require("constants");

const authorisedRoute = express.Router();

const cognitoExpress = new CognitoExpress({
  region: "eu-west-1",
  cognitoUserPoolId: "eu-west-1_eYPZ6wtks",
  tokenUse: "access",
  tokenExpiration: 3600000
});

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

  res.setHeader(
    'Access-Control-Allow-Methods',
    'GET, POST, PUT, DELETE, OPTIONS'
  );
  res.header('Access-Control-Allow-Origin', '*');
  res.header(
    'Access-Control-Allow-Headers',
    'Origin, X-Requested-With, Content-Type, Accept, Authorization'
  );
  res.header('Access-Control-Allow-Credentials', "true");
  res.header('Cache-Control', 'private, no-cache, no-store, must-revalidate');

  if (req.method !== 'OPTIONS') {
    let accessTokenFromClient = req.headers['authorization'];
    if (!accessTokenFromClient) {

      if(req.originalUrl === '/login') {

        return res.sendFile(path.resolve(__dirname, "static/templates/login.html"));
      }

      if(req.originalUrl === '/signup') {

        return res.sendFile(path.resolve(__dirname, "static/templates/signup.html"));
      }

      return res.redirect('/login');
    }

    cognitoExpress.validate(accessTokenFromClient, function (err, response) {
      if (err) return res.status(401).send(err);
      else next();
    });
  }

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