FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 5000
COPY ./PublishOutput .
ENTRYPOINT ["dotnet", "Website.App.dll"]
