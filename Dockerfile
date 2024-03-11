# Create an intermediate image using .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
LABEL stage=build-env
WORKDIR /app

# Copy and build in the intermediate image
COPY . /app
RUN dotnet publish /app/PlayEco.Website -c Release -o ./build/release

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app

## Create a volume for App_Data
#VOLUME /app/PlayEco.Website/App_Data

COPY --from=build-env /app/build/release .

ENTRYPOINT ["dotnet", "PlayEco.Website.dll"]
