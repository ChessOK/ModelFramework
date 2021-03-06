﻿using System.Collections.Generic;

using Autofac;

using ChessOk.ModelFramework.AsyncCommands;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Logging;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Регистрирует основные классы и модули ModelFramework. 
    /// Регистрирует <see cref="ApplicationBus"/>, загружает
    /// модули <see cref="CommandsModule"/>, <see cref="AsyncCommandsModule"/>,
    /// <see cref="ValidationModule"/>.
    /// </summary>
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationBus(
                x.Resolve<IModelContext>(), 
                x.Resolve<IEnumerable<IApplicationBusMessageHandler>>())
            ).As<IApplicationBus>();

            builder.Register(x => new NullLogger()).As<ILogger>().SingleInstance();
            
            builder.RegisterModule(new CommandsModule());
            builder.RegisterModule(new AsyncCommandsModule());
            builder.RegisterModule(new ValidationModule());
        }
    }
}
