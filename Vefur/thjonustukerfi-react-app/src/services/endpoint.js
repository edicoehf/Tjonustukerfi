const url = process.env.REACT_APP_API_ENDPOINT || "http://localhost";
const port = process.env.REACT_APP_API_PORT || "5000";
console.log(process.env)
const endpoint = `${url}:${port}`;

module.exports = endpoint;