# Gunakan SDK (Software Development Kit) untuk membangun aplikasi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SimpleAPI.csproj", "SimpleAPI/"]
RUN dotnet restore "SimpleAPI/SimpleAPI.csproj"

# Salin semua kode proyek
COPY . .
WORKDIR "/src/SimpleAPI"
RUN dotnet build "SimpleAPI.csproj" -c Release -o /app/build

# Lakukan publish/rilis
FROM build AS publish
RUN dotnet publish "SimpleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Gunakan Runtime untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Tentukan port yang akan digunakan (default Kestrel)
ENV ASPNETCORE_URLS=http://+:8080 
EXPOSE 8080

# Tentukan command saat container dijalankan
ENTRYPOINT ["dotnet", "SimpleAPI.dll"]