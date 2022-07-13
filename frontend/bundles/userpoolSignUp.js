const { CognitoUserPool} = require('amazon-cognito-identity-js');
const {CLIENTID, USER_POOL_ID} = require('../config')


module.exports.completedSignUp = async (email, password) => {

	let cognito = {
        UserPoolId: USER_POOL_ID,
        ClientId: CLIENTID
      };

	let userPool = new CognitoUserPool(cognito);
	
	return new Promise((res, rej) => {
		userPool.signUp(email, password, [], null, (err, data) => {

		var messageResult = [];
	
			if (err) {
				messageResult.push({signUp: 'failed', message: err.message});
				res(messageResult);
			}
			else
			{
				messageResult.push({signUp: 'passed', message: data});
				res(messageResult);
			}
		});
	})
};
