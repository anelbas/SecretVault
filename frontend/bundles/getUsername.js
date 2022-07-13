const Cookies = require('universal-cookie');

module.exports.getUsername = () => {
	let cookie = new Cookies();
	return cookie.get('username');
}
