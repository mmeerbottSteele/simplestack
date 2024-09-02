FROM mysql:8.0

COPY ./init-db.sql /docker-entrypoint-initdb.d

ENV MYSQL_ROOT_PASSWORD Password123

EXPOSE 3306