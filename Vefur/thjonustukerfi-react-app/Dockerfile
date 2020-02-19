FROM node:latest
WORKDIR /app

LABEL maintainer = "Edico ehf."

COPY package.json package.json
RUN npm install

COPY . /app

ENTRYPOINT ["npm", "run", "build"]
ENTRYPOINT ["npm", "run", "serve", "-s", "build"]