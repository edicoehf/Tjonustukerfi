const url = window._env_.API_URL || "http://localhost";
const port = window._env_.API_PORT || "5000";
const endpoint = `${url}:${port}`;

module.exports = endpoint;
