FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TreeHouse-AdventOfCodeDay8.csproj", "./"]
RUN dotnet restore "TreeHouse-AdventOfCodeDay8.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TreeHouse-AdventOfCodeDay8.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TreeHouse-AdventOfCodeDay8.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TreeHouse-AdventOfCodeDay8.dll"]
