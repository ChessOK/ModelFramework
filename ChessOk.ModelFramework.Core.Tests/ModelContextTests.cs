using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class ModelContextTests
    {
        private IContainer _container;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new SampleService()).AsSelf().InstancePerModelContext();
            _container = builder.Build();
        }

        [TestMethod]
        public void ModelContextGetMethodsTests()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new SampleService()).AsSelf();
            builder.Register(c => new SampleImplA()).As<ISampleIntf>();
            builder.Register(c => new SampleImplB()).As<ISampleIntf>();
            var container = builder.Build();

            using (var context = new ModelContext(container))
            {
                Assert.IsInstanceOfType(context.Get<SampleService>(), typeof(SampleService));
                Assert.IsInstanceOfType(context.Get(typeof(SampleService)), typeof(SampleService));

                var collection = context.GetAll<ISampleIntf>();
                Assert.IsNotNull(collection);
                Assert.AreEqual(2, collection.Count());

                collection = context.GetAll(typeof(ISampleIntf)) as IEnumerable<ISampleIntf>;
                Assert.IsNotNull(collection);
                Assert.AreEqual(2, collection.Count());
            }
        }

        [TestMethod]
        public void ModelContextResolvesItselfCorrectly()
        {
            using (var modelContext = new ModelContext(_container))
            {
                Assert.AreSame(modelContext, modelContext.Get<ModelContext>());
            }
        }

        [TestMethod]
        public void MultipleModelContextxResolveItselfCorrectly()
        {
            using (var context1 = new ModelContext(_container))
            using (var context2 = new ModelContext(_container))
            {
                Assert.AreNotSame(context1, context2);
                Assert.AreSame(context1, context1.Get<ModelContext>());
                Assert.AreSame(context2, context2.Get<ModelContext>());
            }
        }

        [TestMethod]
        public void DifferentScopedInstancesResolvingForDifferentModelContexts()
        {
            using (var context1 = new ModelContext(_container))
            using (var context2 = new ModelContext(_container))
            {
                Assert.AreNotSame(context1.Get<SampleService>(), context2.Get<SampleService>());
            }
        }

        [TestMethod]
        public void SameScopedInstancesResolvingInsideModelContext()
        {
            using (var context = new ModelContext(_container))
            {
                Assert.AreSame(context.Get<SampleService>(), context.Get<SampleService>());
            }
        }

        [TestMethod]
        public void InstancePerModelContextObjectsAreDisposedAfterDisposingOfModelContext()
        {
            SampleService service;
            using (var context = new ModelContext(_container))
            {
                service = context.Get<SampleService>();
                Assert.IsFalse(service.Disposed);
            }

            Assert.IsTrue(service.Disposed);
        }

        public class SampleService : IDisposable
        {
            public bool Disposed { get; set; }

            public void Dispose()
            {
                Disposed = true;
            }
        }

        public interface ISampleIntf {}
        public class SampleImplA : ISampleIntf {}
        public class SampleImplB : ISampleIntf {}
    }
}
