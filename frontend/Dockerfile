FROM node:16.10 as node

WORKDIR /app
COPY . .

RUN npm install -g @angular/cli 
RUN npm i 
RUN npm run build --prod

FROM nginx:latest
COPY --from=node /app/dist/api-training /usr/share/nginx/html
EXPOSE 4200

