# Fabros ECSLite тестовое

1. На разработку суммарно потрачено около 12 часов. Из них 4 часа - знакомство с ECSLite + поиск и загрузка ассетов.


2. **Для полной симуляции логики нужны:**

_Системы:_

- EcsGameStartup - создать и запустить
- GatesInitSystem
- GateOpenSystem
- GateTriggerSystem
- PlayerInitSystem
- MovableSystem

_Компоненты:_

- GameData - класс с данными для работы
- PlayerTag
- MovableComponent
- ButtonComponent
- GateComponent

Чтобы проект полноценно работал в иной среде, нужно:
1. Написать стартовый класс (аналог Startup), в котором заполнится GameData и запустится EcsGameStartup
2. Написать польозвательский ввод. Здесь он реализован с использованием Camera.


_**Спасибо за просмотр моей работы. Буду рад полезным замечаниям по работе с ECS)**_