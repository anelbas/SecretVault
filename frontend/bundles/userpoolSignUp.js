const { CognitoUserPool} = require('amazon-cognito-identity-js');
const {CLIENTID, USER_POOL_ID} = require('../config')


module.exports.completedSignUp = (email, password) => {

	let cognito = {
        UserPoolId: USER_POOL_ID,
        ClientId: CLIENTID
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
