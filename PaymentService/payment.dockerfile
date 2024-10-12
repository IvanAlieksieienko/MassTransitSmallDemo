FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as sdk
WORKDIR /app

COPY PaymentService ./PaymentService
COPY OrderService.Contracts ./OrderService.Contracts

EXPOSE 7271

WORKDIR /app/PaymentService
CMD dotnet run