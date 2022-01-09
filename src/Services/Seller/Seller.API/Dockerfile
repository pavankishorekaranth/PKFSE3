#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Seller/Seller.API/Seller.API.csproj", "Services/Seller/Seller.API/"]
COPY ["Services/Seller/Seller.Infrastructure/Seller.Infrastructure.csproj", "Services/Seller/Seller.Infrastructure/"]
COPY ["Services/Seller/Seller.Application/Seller.Application.csproj", "Services/Seller/Seller.Application/"]
COPY ["Services/Seller/Seller.Domain/Seller.Domain.csproj", "Services/Seller/Seller.Domain/"]
COPY ["CommonUtility/EventBus.Message/EventBus.Message.csproj", "CommonUtility/EventBus.Message/"]
RUN dotnet restore "Services/Seller/Seller.API/Seller.API.csproj"
COPY . .
WORKDIR "/src/Services/Seller/Seller.API"
RUN dotnet build "Seller.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Seller.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seller.API.dll"]