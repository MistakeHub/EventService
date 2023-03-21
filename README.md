EventService - представляет собой сервис, работающий с данными мероприяйтий.

запуск сервиса через docker контейнер:
В командной строке следует перейти в папку EventService/EventService -> docker build -t eventservice . -> docker-compose up
Т.к данный сервис поддерживает Аутентификацию посредством Identity Server 4, то вам нужно: перейти в папку SC.Internship.Common -> docker-compose up
