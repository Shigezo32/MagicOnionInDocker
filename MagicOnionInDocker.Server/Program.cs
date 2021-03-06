﻿using System;
using Grpc.Core;
using MagicOnion.Server;

namespace MagicOnionInDocker.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort("0.0.0.0", 12345, ServerCredentials.Insecure) }
            };

            // launch gRPC Server.
            server.Start();

            Console.ReadLine();
        }
    }
}
