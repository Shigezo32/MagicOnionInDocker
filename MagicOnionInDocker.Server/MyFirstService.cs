using System;
using System.Collections.Generic;
using System.Text;
using MagicOnion;
using MagicOnion.Server;
using MagicOnionInDocker.ServiceDefinition;

namespace MagicOnionInDocker.Server
{
    // implement RPC service.
    // inehrit ServiceBase<interface>, interface
    public class MyFirstService : ServiceBase<IMyFirstService>, IMyFirstService
    {
        public async UnaryResult<int> SumAsync(int x, int y)
        {
            Logger.Debug($"Received:{x}, {y}");

            return x + y;
        }
    }
}
