==важный рефакторинг==
- подключать стили с фиксированного адреса без js
* добавление элементов в главное меню
- проверить: что происходит при исключениях во время инициализации и остановки сервиса
- сделать быстрее загрузку страниц

==виджеты==
- запуск сценариев
- будильники
- погода
- проверить виджеты noolite

==дополнительно==
- логирование присутствия
- серая тема оформления
- переключение тем оформления через конфиг
- привести в порядок стили
- плагин управления плейером
- плагин управления торрентами
- сделать обработку ошибок на клиенте
- редактор голосовых команд и их обработчиков
- менеджер пакетов
- документация к плагинам на wiki

==i18n==
- язык выбирается при старте приложения (задается в конфиге) и во время работы приложения не меняется
- в местах использования фраз задается только ключ, остальные параметры задаются в другом месте и не дублируются
- данные в БД хранятся на одном языке
- переводятся константные строки на сервере и клиенте, даты, числа надписи в шаблонах


как делать:
- на стороне сервера создать ресурсный файл, включить кодогенерацию и использовать статические свойства сгенерированного класса
- для клиента вызывать хелпер, который из ResourceManager выгрузит строки в json