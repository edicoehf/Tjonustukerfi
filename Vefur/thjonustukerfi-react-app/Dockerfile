FROM node:11-alpine AS builder
WORKDIR /app

LABEL maintainer="Edico ehf"

ARG API_URL
ARG API_PORT
ENV API_URL=$API_URL
ENV API_PORT=$API_PORT

COPY . .
RUN npm install --silent
RUN apk add --no-cache bash
RUN chmod +x env.sh
RUN ./env.sh
RUN npm run build

FROM node:11-alpine
RUN npm install -g serve
WORKDIR /app
COPY --from=builder /app/build .
CMD ["serve", "-p", "3000", "-s", "."]doc