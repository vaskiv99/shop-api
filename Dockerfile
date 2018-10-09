FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ShopApi/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish ./ShopApi/ShopService.Web.csproj -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
 
COPY --from=build-env /app/ShopApi/out .

EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "ShopService.Web.dll"]