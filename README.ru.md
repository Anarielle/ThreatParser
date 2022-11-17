# Парсер угроз
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6b7b630c12074adf81f9df4bce536b60)](https://www.codacy.com/gh/Anarielle/ThreatParser/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Anarielle/ThreatParser&amp;utm_campaign=Badge_Grade)

Приложение автоматически создает локальную базу данных угроз информации, путем загрузки и последущего парсинга информации из официального банка данных угроз ФСТЭК России. 

## Возможности
### Просмотр общего перечня угроз
Выводит краткую информацию об угрозах в виде идентификатора угрозы и наименования.

<div align="center">

![Alt text](./Resources/Parser_short_info.png) 

</div>

### Просмотр всех сведений о каждой угрозе
Каждая запись об угрозе безопасности информации включает в себя следущие сведения:

-   идентификатор
-   наименование
-   описание
-   источник
-   объект воздействия
-   нарушение конфиденциальности (да/нет)
-   нарушение целостности (да/нет)
-   нарушение доступности (да/нет)

<div align="center">

![Alt text](./Resources/Parser_detailed_info.png) 

</div>

### Обновление сведений
Автоматическое обновление сведений (по запросу пользователя) в результате которого программа выводит отчет об обновленных записях.

<div align="center">

![Alt text](./Resources/Parser_update_info.png) 

</div>

## Стек технологий
-   .NET Fraemwork 4.7.2
-   WPF
-   C#
-   Библиотека EPPlus версия 6.0