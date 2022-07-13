const Cookies = require('universal-cookie');

module.exports.getCookie = () => {
	let cookie = new Cookies();
	return cookie.get('token');
}
