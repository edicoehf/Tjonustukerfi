let envUrl = null;
let envPort = null;

if(window._env_) {
    envUrl = window._env_.API_URL;
    envPort = window._env_.API_PORT;
}

const url = envUrl || "http:localhost";
const port = envPort || 5000;
const endpoint = `${url}:${port}`;

module.exports = endpoint;
