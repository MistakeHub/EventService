EventService - представляет собой сервис, работающий с данными мероприяйтий.

запуск сервиса через docker контейнер:
В командной строке следует перейти в папку EventService/EventService -> docker build -t eventservice . (если у вас докер поддерживайт Linux контейнеры, то переключите на Windows контейнеры) -> docker-compose up.
Т.к данный сервис поддерживает Аутентификацию посредством Identity Server 4, то вам нужно: перйти в папку SC.Internship.Common ->  docker-compose up.