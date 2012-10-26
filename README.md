ModelFramework
==============

ModelFramework создан для того, чтобы решить некоторые [характерные для ASP.NET MVC](https://github.com/ChessOK/ModelFramework.Core/wiki) (да и для многих реализаций шаблона [Model 2 MVC](http://en.wikipedia.org/wiki/Model_2) в веб-приложениях) проблемы, связанные с довольно мутным определением самой модели и тем, что должно там находиться.

Если быть более точным, то он предоставляет компоненты для реализации [операционного уровня](http://www.k-press.ru/cs/2010/4/ddd/ddd.asp) приложения (если делить уровни приложений по [Эрику Эвансу](http://domaindrivendesign.org/about)).

Он состоит из следующих компонентов:
* Команды — инкапсулируют операции пользователей: валидируют параметры этих операций и координируют работу служб приложения. Под операцией я подразумеваю любой код, который меняет состояние системы, либо её окружения. В качестве пользователя может выступать как живой человек, так и другая система.
* Шина сообщений — предоставляет единый интерфейс для инициирования команд и других сообщений, предоставляя возможность другим модулям приложения вносить изменения в выполнение тех или иных операций.
* Валидационный контекст — встроенный механизм для шины сообщений (и, в частности, для команд), который предоставляет средства для валидации параметров, пришедших от пользователя.
* Многоуровневый контейнер для резолвинга зависимостей, в качестве которого выступает [Autofac](http://code.google.com/p/autofac/).

Облегчайте контроллеры
----------------------
```csharp
[HttpPost]
public ActionResult Register(RegisterModel model)
{
    var command = new RegisterCommand { Data = model };
    if (Bus.TrySend(command))
    {
        return RedirectToAction("Index", "Home");
    }

    return View(model);
}
```

Расширяйте приложение без изменения модели
------------------------------------------
```csharp
public class SendMailAfterRegistrationHandler : CommandInvokedHandler<RegistrationCommand>
{
    protected void Handle(RegisterCommand command)
    {
        EmailApi.Send(command.Email, EmailApi.RegistrationMessage(command.UserName));
    }
}
```

*Продолжение следует*