using System;

using Autofac;
using Autofac.Builder;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework
{
    public static class RegistrationExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle>
            InstancePerModelContext<TLimit, TActivatorData, TStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            return registration.InstancePerMatchingLifetimeScope(ContextHierarchy.ModelContext);
        }

        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle>
            InstancePerApplicationBus<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            return registration.InstancePerMatchingLifetimeScope(ContextHierarchy.ApplicationBus);
        }

        public static IRegistrationBuilder<TLimit, TActivationData, TStyle>
            InstancePerValidationContext<TLimit, TActivationData, TStyle>(
                this IRegistrationBuilder<TLimit, TActivationData, TStyle> registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            return registration.InstancePerMatchingLifetimeScope(ContextHierarchy.ValidationContext);
        }

        public static void RegisterEventHandler<T>(this ContainerBuilder builder,
            Func<IComponentContext, T> @delegate)
        {
            builder.Register(@delegate).As<IApplicationBusMessageHandler>().InstancePerDependency();
        }
    }
}
