

<<<<<<< HEAD
EventService
=======
EventService - представляет собой сервис, работающий с данными мероприяйтий.
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

