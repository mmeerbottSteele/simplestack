FROM public.ecr.aws/docker/library/node:20 AS base-frontend

# We are hosting the frontend and backend on the same server in this dockerfile

# Accept an argument, default to dev 
# provided by AWS Console, forwarded by buildspec. Uses short, lowercase naming.

RUN mkdir ng-app
WORKDIR /ng-app

COPY frontend/package.json /ng-app/package.json
RUN npm install
RUN npm install -g @angular/cli@18.0.4

COPY ./frontend /ng-app
# Frontend env is trivial, just use prod unless we want to expose debug symbols
RUN npm run prod

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./backend/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./backend ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=base-frontend /ng-app/dist/frontend/browser/ ./wwwroot
EXPOSE 80
EXPOSE 8080
ARG ENVIRONMENT=Development
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
RUN echo $ENVIRONMENT
ENTRYPOINT ["dotnet", "backend.dll"]

