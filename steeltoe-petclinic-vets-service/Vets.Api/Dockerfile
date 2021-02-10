FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster as builder
WORKDIR /application
COPY ["Steeltoe.Petclinic.Vets.Api.csproj", ""]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /application

ARG EXPOSED_PORT=5000
ENV PORT ${EXPOSED_PORT}
EXPOSE ${EXPOSED_PORT}

ENV ASPNETCORE_ENVIRONMENT docker
ENV ENVIRONMENT docker

COPY --from=builder application/publish .
ENTRYPOINT ["dotnet", "vets-service.dll"]
