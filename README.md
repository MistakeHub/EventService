EventService - представляет собой сервис, работающий с данными мероприяйтий.

запуск сервиса через docker контейнер: В командной строке следует перейти в папку EventService/EventService -> docker network create eventidentity_net(нужно для того, чтобы два контейнера могли между собой общаться)-> docker build -t eventservice . -> docker-compose up

Т.к данный сервис поддерживает Аутентификацию посредством Identity Server 4, то вам нужно: перейти в папку SC.Internship.Common -> 
добавить в docker-compose.yaml:
networks:
    default:
     external: true
     name: eventidentity_net 
	 
-> затем в командной строке прописать docker-compose up
