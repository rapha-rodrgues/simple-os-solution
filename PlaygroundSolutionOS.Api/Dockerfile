FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PlaygroundSolutionOS/PlaygroundSolutionOS.csproj", "PlaygroundSolutionOS/"]
RUN dotnet restore "PlaygroundSolutionOS/PlaygroundSolutionOS.csproj"
COPY . .
WORKDIR "/src/PlaygroundSolutionOS"
RUN dotnet build "PlaygroundSolutionOS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaygroundSolutionOS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaygroundSolutionOS.dll"]
