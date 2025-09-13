# Build da aplicação Angular
FROM node:16.14.2 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Servir com Nginx
FROM nginx:alpine
COPY --from=build /app/dist/lit-web /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
