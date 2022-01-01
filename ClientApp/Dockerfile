# Stage 1: Build an Angular Docker Image

FROM node:14.16.0-alpine as node

WORKDIR /app

COPY ClientApp . 
RUN npm install --silent

RUN npm run build --prod

# Stage 2, use the compiled app, ready for production with Nginx

FROM nginx:alpine
COPY --from=node /app/dist /usr/share/nginx/html
COPY ClientApp/nginx/nginx.conf /etc/nginx/conf.d/default.conf
