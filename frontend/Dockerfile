FROM node:16.10 as build

WORKDIR /usr/local/app
COPY ./ /usr/local/app/

RUN npm i 
RUN npm run build --prod

FROM nginx:latest
COPY --from=build /usr/local/app/dist/api-training /usr/share/nginx/html
EXPOSE 4200

