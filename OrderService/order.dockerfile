FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as sdk
WORKDIR /app

COPY OrderService ./OrderService
COPY OrderService.Contracts ./OrderService.Contracts

EXPOSE 7271

WORKDIR /app/OrderService

CMD dotnet run