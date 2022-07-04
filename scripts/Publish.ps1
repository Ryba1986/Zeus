dotnet publish "./src/Zeus.Api.Client/Zeus.Api.Client.csproj" -o "./dist/Zeus.Api.Client" -c Release
dotnet publish "./src/Zeus.Api.Web/Zeus.Api.Web.csproj" -o "./dist/Zeus.Api.Web" -c Release
dotnet publish "./src/Zeus.Client/Zeus.Client.csproj" -o "./dist/Zeus.Client" -c Release

Set-Location "./src/Zeus.Web"
yarn install
yarn build
Set-Location "../../"
