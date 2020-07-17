using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;

using R5T.Marathon.Default;


namespace R5T.Marathon.Standard
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueueProcessor"/> as a <see cref="Microsoft.Extensions.Hosting.IHostedService"/>.
        /// </summary>
        /// <remarks>
        /// No corresponding AddXAction() method for this service since the queue processor will never be injected.
        /// </remarks>
        public static IServiceCollection AddBackgroundWorkItemQueueProcessor(this IServiceCollection services,
            IServiceAction<IBackgroundWorkItemQueue> addBackgroundWorkItemQueue)
        {
            services
                .AddHostedService<BackgroundWorkItemQueueProcessor>()
                .RunServiceAction(addBackgroundWorkItemQueue)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueueProcessor"/> as a <see cref="Microsoft.Extensions.Hosting.IHostedService"/> using the <see cref="BackgroundWorkItemQueue"/> implementation of <see cref="IBackgroundWorkItemQueue"/>.
        /// </summary>
        public static IServiceCollection AddBackgroundWorkItemQueueProcessor(this IServiceCollection services)
        {
            var backgroundWorkItemQueueAction = services.AddBackgroundWorkItemQueueAction();

            services.AddBackgroundWorkItemQueueProcessor(backgroundWorkItemQueueAction);

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueueProcessor"/> as a <see cref="Microsoft.Extensions.Hosting.IHostedService"/> using the <see cref="BackgroundWorkItemQueue"/> implementation of <see cref="IBackgroundWorkItemQueue"/>.
        /// </summary>
        public static IServiceAction<IBackgroundWorkItemQueue> AddBackgroundWorkItemQueueAndProcessorAction(this IServiceCollection services)
        {
            var serviceAction = new ServiceAction<IBackgroundWorkItemQueue>(() => services.AddBackgroundWorkItemQueueProcessor());
            return serviceAction;
        }
    }
}
