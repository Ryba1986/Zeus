# TODO: remove "-pre" after .NET 7 release
dotnet outdated -u -t -pre Always

Set-Location "./src/Zeus.Web"
ncu -u
yarn install
Set-Location "../../"
