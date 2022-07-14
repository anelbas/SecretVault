const  AmazonCognitoIdentity  = require("amazon-cognito-identity-js");
const AWS = require('aws-sdk');
const Cookies = require('universal-cookie');
const {CLIENTID, USER_POOL_ID} = require('../config')

module.exports.completedSignIn = (email,password)=>{

	let authenticationData = {
		Username: email,
		Password: password,
	};
	let authenticationDetails = new AmazonCognitoIdentity.AuthenticationDetails(
		authenticationData
	);
	let poolData = {
		UserPoolId: USER_POOL_ID, // Your user pool id here
		ClientId: CLIENTID, // Your client id here
	};
	let userPool = new AmazonCognitoIdentity.CognitoUserPool(poolData);
	let userData = {
		Username: email,
		Pool: userPool,
	};
	let cognitoUser = new AmazonCognitoIdentity.CognitoUser(userData);
	return new Promise((res, rej) => {
        cognitoUser.authenticateUser(authenticationDetails, {
            onSuccess: function(result) {
                let accessToken = result.getIdToken().getJwtToken();
        
                //POTENTIAL: Region needs to be set if not already set previously elsewhere.
                // console.log(AWS);
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
                        res({status: 500, message: 'failed'});
                    } else {
                        console.log(result);
                        const cookie = new Cookies();
                        cookie.set('token', accessToken, {expires: new Date(new Date().getTime() + 1*60*60*1000), HttpOnly: true, path:"/"})
                        cookie.set('username', result.getIdToken().payload['cognito:username'], {expires: new Date(new Date().getTime() + 1*60*60*1000), HttpOnly: true, path:"/"});
                        
                        res({status: 200, message: 'success'});
                    }
                });
            },
            onFailure: function(err) {
                alert(err.message || JSON.stringify(err));
            },
        });
    });
}