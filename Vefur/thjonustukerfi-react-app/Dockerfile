FROM node:11-alpine AS builder
WORKDIR /app

LABEL maintainer="Edico ehf"

COPY . .
RUN npm install --silent
RUN npm run build

FROM node:11-alpine
RUN npm install -g serve
RUN apk add --no-cache bash
WORKDIR /app
RUN chmod +x env.sh
RUN ./env.sh
COPY --from=builder /app/build .
CMD ["serve", "-p", "3000", "-s", "."]doc