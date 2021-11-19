#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Hahn.ApplicatonProcess.July2021.Web/Hahn.ApplicationProcess.July2021.Web.csproj", "Hahn.ApplicatonProcess.July2021.Web/"]
COPY ["Hahn.ApplicatonProcess.July2021.Data.Sql.Queries/Hahn.ApplicationProcess.July2021.Data.Sql.Queries.csproj", "Hahn.ApplicatonProcess.July2021.Data.Sql.Queries/"]
COPY ["Hahn.ApplicatonProcess.July2021.Domain/Hahn.ApplicationProcess.July2021.Domain.csproj", "Hahn.ApplicatonProcess.July2021.Domain/"]
COPY ["Hahn.ApplicatonProcess.July2021.Data/Hahn.ApplicationProcess.July2021.Data.csproj", "Hahn.ApplicatonProcess.July2021.Data/"]
COPY ["Hahn.ApplicatonProcess.July2021.Data.Sql.Commands/Hahn.ApplicationProcess.July2021.Data.Sql.Commands.csproj", "Hahn.ApplicatonProcess.July2021.Data.Sql.Commands/"]
COPY ["Hahn.ApplicationProcess.July2021.Web.Integration/Hahn.ApplicationProcess.July2021.Web.Integration.csproj", "Hahn.ApplicationProcess.July2021.Web.Integration/"]
RUN dotnet restore "Hahn.ApplicatonProcess.July2021.Web/Hahn.ApplicationProcess.July2021.Web.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicatonProcess.July2021.Web"
RUN dotnet build "Hahn.ApplicationProcess.July2021.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicationProcess.July2021.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.ApplicationProcess.July2021.Web.dll"]