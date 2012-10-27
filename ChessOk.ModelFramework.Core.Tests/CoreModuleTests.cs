﻿using Autofac;

using ChessOk.ModelFramework.Commands;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class CoreModuleTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            _container = builder.Build();
        }

        [TestMethod]
        public void ApplicationBusItemsShouldBeRegistered()
        {
            var applicationBus = new ApplicationBus(new ModelContext(_container));
            Assert.AreSame(applicationBus, applicationBus.Model.Get<IApplicationBus>());
        }
    }
}
