# Stage 1: Build an Angular Docker Image

FROM node:latest as node

RUN mkdir -p /app
WORKDIR /app
COPY  ./ClientApp ./app

RUN npm install
RUN npm run build --prod

# Stage 2, use the compiled app, ready for production with Nginx

FROM nginx:alpine
COPY --from=node /app/dist /usr/share/nginx/html
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
