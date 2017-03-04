# .netcore-rc4-nancy-api

Use Postman or similar to call the api, does not work on the browser.

Azure Commands

azure appserviceplan create --name myapplinuxsp --location "West Europe" --resource-group Default-Web-NorthEurope  --sku F1 --islinux true

azure appserviceplan list --resource-group Default-Web-NorthEurope

azure webapp create --name myapplinux --location "West Europe" --resource-group Default-Web-NorthEurope  --plan myapplinuxsp

azure webapp list --resource-group Default-Web-NorthEurope

azure webapp config set --name myapplinux --resource-group Default-Web-NorthEurope --netframeworkversion v1.0 --appcommandline myapi.dll

az appservice web source-control config--name myapplinux --resource-group Default-Web-NorthEurope --repo-url https://github.com/hjgraca/.netcore-rc4-nancy-api.git --branch master

# Docker

docker build -f Dockerfile -t netcore-rc4-nancy-api:latest .

docker run -it -p 5000:5000 --rm netcore-rc4-nancy-api:latest