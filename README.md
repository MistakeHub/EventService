EventService - представляет собой сервис, работающий с данными мероприяйтий.

запуск сервиса через docker контейнер: 
В командной строке следует перейти в папку EventService/EventService -> docker build . -f SpaceService/Dockerfile -t space-> docker build . -f ImageService/Dockerfile -t image -> docker build . -f PaymentService/Dockerfile -t payment ->docker build -t eventservice . -> docker-compose up

(Сервисы Space и Image могут не запуститься с первого раза, возможно вам придётся запускать вручную )
