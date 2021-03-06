﻿using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0049;

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
            IServiceAction<IBackgroundWorkItemQueue> addBackgroundWorkItemQueue,
            IServiceAction<IExceptionSink> exceptionSinkAction)
        {
            services
                .AddHostedService<BackgroundWorkItemQueueProcessor>()
                .RunServiceAction(addBackgroundWorkItemQueue)
                .Run(exceptionSinkAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueueProcessor"/> as a <see cref="Microsoft.Extensions.Hosting.IHostedService"/> using the <see cref="BackgroundWorkItemQueue"/> implementation of <see cref="IBackgroundWorkItemQueue"/>.
        /// </summary>
        public static IServiceCollection AddBackgroundWorkItemQueueProcessor(this IServiceCollection services,
            IServiceAction<IExceptionSink> exceptionSinkAction)
        {
            var backgroundWorkItemQueueAction = services.AddBackgroundWorkItemQueueAction();

            services.AddBackgroundWorkItemQueueProcessor(
                backgroundWorkItemQueueAction,
                exceptionSinkAction);

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueueProcessor"/> as a <see cref="Microsoft.Extensions.Hosting.IHostedService"/> using the <see cref="BackgroundWorkItemQueue"/> implementation of <see cref="IBackgroundWorkItemQueue"/>.
        /// </summary>
        public static IServiceAction<IBackgroundWorkItemQueue> AddBackgroundWorkItemQueueAndProcessorAction(this IServiceCollection services,
            IServiceAction<IExceptionSink> exceptionSinkAction)
        {
            var serviceAction = new ServiceAction<IBackgroundWorkItemQueue>(() => services.AddBackgroundWorkItemQueueProcessor(
                exceptionSinkAction));

            return serviceAction;
        }
    }
}
