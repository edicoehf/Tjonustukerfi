FROM mhart/alpine-node:11 AS builder
WORKDIR /app

LABEL maintainer="Edico ehf"

COPY . .
RUN npm install react-scripts -g --silent
RUN npm install
RUN npm run build

FROM mhart/alpine-node
RUN npm install -g serve
WORKDIR /app
COPY --from=builder /app/build .
CMD ["serve", "-p", "3000", "-s", "."]doc