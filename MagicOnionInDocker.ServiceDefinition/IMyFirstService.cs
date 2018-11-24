using System;
using System.Collections.Generic;
using System.Text;
using MagicOnion;

namespace MagicOnionInDocker.ServiceDefinition
{
    // define interface as Server/Client IDL.
    // implements T : IService<T>.
    public interface IMyFirstService : IService<IMyFirstService>
    {
        UnaryResult<int> SumAsync(int x, int y);
    }
}
