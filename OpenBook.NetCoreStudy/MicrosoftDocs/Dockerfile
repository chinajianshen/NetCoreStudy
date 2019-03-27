#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["MicrosoftDocs/MicrosoftDocs.csproj", "MicrosoftDocs/"]
RUN dotnet restore "MicrosoftDocs/MicrosoftDocs.csproj"
COPY . .
WORKDIR "/src/MicrosoftDocs"
RUN dotnet build "MicrosoftDocs.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicrosoftDocs.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicrosoftDocs.dll"]