const { CognitoUserPool, CognitoUserAttribute, AmazonCognitoIdentity} = require("amazon-cognito-identity-js");
// const { AmazonCognitoIdentity } = require("./amazon-cognito-identity.min");


module.exports.completedSignUp = (email, password) => {

	let cognito = {
        UserPoolId: "eu-west-1_eYPZ6wtks",
        ClientId: "4apfe9mjbkqm6jd1lvbfhipfs8"
      }

	let userPool = new CognitoUserPool(cognito);
	userPool.signUp(email, password, [], null, (err, data) => {
		if (err) {
			console.log(err);
		}
		else
		{
			console.log(data);
		}
	})
}