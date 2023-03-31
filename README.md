EventService - представляет собой сервис, работающий с данными мероприяйтий.

<<<<<<< HEAD
запуск сервиса через docker контейнер: В командной строке следует перейти в папку EventService/EventService -> docker network create eventidentity_net(нужно для того, чтобы два контейнера могли между собой общаться)-> docker build -t eventservice . -> docker-compose up
=======
запуск сервиса через docker контейнер: 
1. В командной строке, вам нужно перейти  вс дерикторию EventService
2. Для запуска, вам требуется в командной строке ввести docker-compose up

Swagger EventService: http://localhost:8081/swagger/index.html
Swagger SpaceService: http://localhost:8082/swagger/index.html
Swagger SpaceService: http://localhost:8083/swagger/index.html
Swagger PaymentService: http://localhost:8084/swagger/index.html

Данные свервисы поддерживают авторизацию при помощи JWT токенов. Получить их вы можете по эндпоинту http://localhost:5000/connect/token с телом запроса:
https://imgur.com/a/WXlFDTO

token(access_token):
https://imgur.com/a/9lxSlxy

затем в Swagger вы можете ввести данный токен:
https://imgur.com/a/FwtY0zj
>>>>>>> Task4

Т.к данный сервис поддерживает Аутентификацию посредством Identity Server 4, то вам нужно: перейти в папку SC.Internship.Common -> 
добавить в docker-compose.yaml:
networks:
    default:
     external: true
     name: eventidentity_net 
	 
-> затем в командной строке прописать docker-compose up
