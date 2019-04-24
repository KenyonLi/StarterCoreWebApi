using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DIExtension
{
    public interface IRepositoryFactory
    {
        object CreateInstance(Type interfaceType);
    }
}
