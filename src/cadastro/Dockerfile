# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia solução e projetos
COPY *.sln .
COPY cadastro.API/cadastro.API.csproj cadastro.API/
COPY cadastro.Application/cadastro.Application.csproj cadastro.Application/
COPY cadastro.Domain/cadastro.Domain.csproj cadastro.Domain/
COPY cadastro.Infrastructure/cadastro.Infrastructure.csproj cadastro.Infrastructure/

# Restaura dependências
RUN dotnet restore

# Copia tudo
COPY . .

# Build
WORKDIR /src/cadastro.API
RUN dotnet publish -c Release -o /app/publish

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "cadastro.API.dll"]
