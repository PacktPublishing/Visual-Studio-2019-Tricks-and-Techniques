using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Builder;
using Unity.Extension;
using Unity.Strategies;

namespace CGHClientServer1.WebApi.DI
{
    internal class LogExtension : UnityContainerExtension
    {
        public LogExtension(ILogger logger)
        {
            _logger = logger;
        }

        #region UnityContainerExtension

        protected override void Initialize()
        {
            Context.Strategies.Add(new LoggingStrategy(_logger), UnityBuildStage.PreCreation);
        }

        #endregion UnityContainerExtension

        #region private

        private readonly ILogger _logger;

        private class LoggingStrategy : BuilderStrategy
        {
            public LoggingStrategy(ILogger logger)
            {
                _logger = logger;
            }

            #region BuilderStrategy

            public override void PreBuildUp(ref BuilderContext context)
            {
                // _logger.Log($"Resolving {context.BuildKey.Type} for {context.OriginalBuildKey.Type}");
                _logger.LogInformation($"Resolving {context.Type} for {context.RegistrationType}");
            }

            #endregion BuilderStrategy

            #region private

            private readonly ILogger _logger;

            #endregion private
        }

        #endregion private
    }
}