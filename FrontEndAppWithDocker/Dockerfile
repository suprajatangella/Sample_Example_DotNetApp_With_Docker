#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
#You can specify a working directory on the container to execute all forward
#commands based on a specific directory
WORKDIR /src
COPY ["FrontEndAppWithDocker/FrontEndAppWithDocker.csproj", "FrontEndAppWithDocker/"]
#we need to restore all project dependencies in the image
RUN dotnet restore "./FrontEndAppWithDocker/FrontEndAppWithDocker.csproj"
#we are copying all the content of the current directory in the app directory of the new image
COPY . .
WORKDIR "/src/FrontEndAppWithDocker"

CMD dotnet watch run "./FrontEndAppWithDocker.csproj"  #newly added line
RUN dotnet build "./FrontEndAppWithDocker.csproj" -c $BUILD_CONFIGURATION -o /app/build -t frontend

#Sql Server Database image
FROM mcr.microsoft.com/mssql/server as sqldb
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Password_123
ENV MSSQL_PID=Express

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FrontEndAppWithDocker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrontEndAppWithDocker.dll"]