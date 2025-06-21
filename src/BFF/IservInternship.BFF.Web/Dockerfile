# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble-chiseled-extra AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
VOLUME ["/root/.nuget/packages"]
COPY ["BFF/IservInternship.BFF.Web/IservInternship.BFF.Web.csproj", "BFF/IservInternship.BFF.Web/IservInternship.BFF.Web.csproj"]
RUN dotnet restore "BFF/IservInternship.BFF.Web/IservInternship.BFF.Web.csproj"

COPY ["BFF/IservInternship.BFF.Web/", "BFF/IservInternship.BFF.Web/"]

COPY ["Account/IservInternship.Account.Infrastructure/", "Account/IservInternship.Account.Infrastructure/"]
COPY ["Account/IservInternship.Account.Services/", "Account/IservInternship.Account.Services/"]

COPY ["Application/IservInternship.Application.Infrastructure/", "Application/IservInternship.Application.Infrastructure/"]
COPY ["Application/IservInternship.Application.Services/", "Application/IservInternship.Application.Services/"]
COPY ["Shared/", "Shared/"]
RUN dotnet build "BFF/IservInternship.BFF.Web/IservInternship.BFF.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BFF/IservInternship.BFF.Web/IservInternship.BFF.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IservInternship.BFF.Web.dll"]