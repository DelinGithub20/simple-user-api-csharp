# Gunakan SDK (Software Development Kit) untuk membangun aplikasi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Salin file proyek dan lakukan restore dependency
COPY ["SimpleAPI.csproj", "."]
RUN dotnet restore "SimpleAPI.csproj"

# Salin semua kode proyek dan lakukan publish
COPY . .
RUN dotnet publish "SimpleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Gunakan Runtime untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Tentukan port yang akan digunakan (default Kestrel)
ENV ASPNETCORE_URLS=http://+:8080 
EXPOSE 8080

# Tentukan command saat container dijalankan
ENTRYPOINT ["dotnet", "SimpleAPI.dll"]
