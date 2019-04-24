using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DIExtension
{
    public class RepositoryFactory : IRepositoryFactory
    {

        private readonly IDictionary<Type, object> _cachedRepository = new Dictionary<Type, object>();

        //private readonly IRepositoryBuilder _repositoryBuilder;
        private readonly ILogger _logger;

        public RepositoryFactory(ILogger logger)
        {
            _logger = logger;
        }

        public object CreateInstance(Type interfaceType)
        {
            if (!_cachedRepository.ContainsKey(interfaceType))
            {
                lock (this)
                {
                    if (!_cachedRepository.ContainsKey(interfaceType))
                    {
                        if (_logger.IsEnabled(LogLevel.Debug))
                        {
                            _logger.LogDebug($"RepositoryFactory.CreateInstance :InterfaceType.FullName:[{interfaceType.FullName}] Start");
                        }
                        //var implType = _repositoryBuilder.Build(interfaceType, sqlMapper.SmartSqlConfig, scope);

                        //var obj = sqlMapper.SmartSqlConfig.ObjectFactoryBuilder
                        //    .GetObjectFactory(implType, new Type[] { ISqlMapperType.Type })(new object[] { sqlMapper });
                        //_cachedRepository.Add(interfaceType, obj);
                        //if (_logger.IsEnabled(LogLevel.Debug))
                        //{
                        //    _logger.LogDebug($"RepositoryFactory.CreateInstance :InterfaceType.FullName:[{interfaceType.FullName}],ImplType.FullName:[{implType.FullName}] End");
                        //}
                    }
                }
            }
            return _cachedRepository[interfaceType];
        }

    }
}
