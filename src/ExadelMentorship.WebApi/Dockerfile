#Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ExadelMentorship.WebApi.csproj .
RUN dotnet restore "ExadelMentorship.WebApi.csproj"
COPY . .
RUN dotnet publish "ExadelMentorship.WebApi.csproj" -c Release -o /publish

#Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /publish .

EXPOSE 7066
ENTRYPOINT ["dotnet", "ExadelMentorship.WebApi.dll"]