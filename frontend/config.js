const dotenv = require('dotenv');
const path = require('path');

dotenv.config({
    path: path.resolve(__dirname, `${process.env.NODE_ENV}.env`)
});

module.exports = {
  NODE_ENV : process.env.NODE_ENV || 'development',
  HOST : process.env.HOST || 'localhost',
  PORT : process.env.PORT || 3003,
  API_BASE_URL: process.env.API_BASE_URL,
  CLIENTID: '4apfe9mjbkqm6jd1lvbfhipfs8',
  USER_POOL_ID: 'eu-west-1_eYPZ6wtks'
}