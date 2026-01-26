FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copiar todos os arquivos de projeto primeiro
# Isso permite que o cache do Docker salve o restore se você não mudar dependências
COPY ["notification.Worker/notification.Worker.csproj", "notification.Worker/"]
COPY ["notification.Infrastructure/notification.Infrastructure.csproj", "notification.Infrastructure/"]
COPY ["notification.Application/notification.Application.csproj", "notification.Application/"]
COPY ["notification.Domain/notification.Domain.csproj", "notification.Domain/"]

# 2. Rodar o restore no projeto principal (ele vai restaurar os outros por referência)
RUN dotnet restore "notification.Worker/notification.Worker.csproj"

# 3. Agora sim copiar o resto dos arquivos
COPY . .

# 4. Build e Publish
WORKDIR "/src/notification.Worker"
RUN dotnet publish "notification.Worker.csproj" -c Release -o /app/publish

# Stage final
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "notification.Worker.dll"]