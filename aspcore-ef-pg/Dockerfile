FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["aspcore-ef-pg/aspcore-ef-pg.csproj", "aspcore-ef-pg/"]
RUN dotnet restore "aspcore-ef-pg/aspcore-ef-pg.csproj"
COPY . .
WORKDIR "/src/aspcore-ef-pg"
RUN dotnet build "aspcore-ef-pg.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "aspcore-ef-pg.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspcore-ef-pg.dll"]
