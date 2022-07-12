const { CognitoUserPool, CognitoUserAttribute} = require('amazon-cognito-identity-js');
const  AmazonCognitoIdentity  = require("amazon-cognito-identity-js");
const AWS = require('aws-sdk');


module.exports.completedSignUp = (email, password) => {

	let cognito = {
        UserPoolId: "eu-west-1_eYPZ6wtks",
        ClientId: "4apfe9mjbkqm6jd1lvbfhipfs8"
      };

	let userPool = new CognitoUserPool(cognito);
	userPool.signUp(email, password, [], null, (err, data) => {
		if (err) {
			console.log(err);
		}
		else
		{
			console.log(data);
		}
	});
};

module.exports.completedSignIn = (email,password)=>{

	let authenticationData = {
		Username: email,
		Password: password,
	};
	let authenticationDetails = new AmazonCognitoIdentity.AuthenticationDetails(
		authenticationData
	);
	let poolData = {
		UserPoolId: 'eu-west-1_eYPZ6wtks', // Your user pool id here
		ClientId: '4apfe9mjbkqm6jd1lvbfhipfs8', // Your client id here
	};
	let userPool = new AmazonCognitoIdentity.CognitoUserPool(poolData);
	let userData = {
		Username: email,
		Pool: userPool,
	};
	let cognitoUser = new AmazonCognitoIdentity.CognitoUser(userData);
	cognitoUser.authenticateUser(authenticationDetails, {
		onSuccess: function(result) {
			let accessToken = result.getAccessToken().getJwtToken();
	
			//POTENTIAL: Region needs to be set if not already set previously elsewhere.
			console.log(AWS);
			AWS.config.region = 'eu-west-1';
	
			AWS.config.credentials = new AWS.CognitoIdentityCredentials({
				IdentityPoolId: 'eu-west-1:c405b538-a9bb-4589-98fb-d0e8dae18e23', // your identity pool id here
				Logins: {
					// Change the key below according to the specific region your user pool is in.
					'cognito-idp.eu-west-1.amazonaws.com/eu-west-1_eYPZ6wtks': result
						.getIdToken()
						.getJwtToken(),
				},
			});
	
			//refreshes credentials using AWS.CognitoIdentity.getCredentialsForIdentity()
			AWS.config.credentials.refresh(error => {
				if (error) {
					console.error(error);
				} else {
					// Instantiate aws sdk service objects now that the credentials have been updated.
					// example: let s3 = new AWS.S3();
					console.log('Successfully logged!');
				}
			});
		},
	
		onFailure: function(err) {
			alert(err.message || JSON.stringify(err));
		},
	});
}