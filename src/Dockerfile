FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .
WORKDIR "/src/ExadelMentorship.WebApi"
RUN dotnet publish "ExadelMentorship.WebApi.csproj" -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExadelMentorship.WebApi.dll"]