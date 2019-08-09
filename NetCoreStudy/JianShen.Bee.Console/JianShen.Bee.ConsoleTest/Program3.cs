using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Bee.ConsoleTest
{
    class Program3
    {
        //.net core DI 实例生命周期
        //static void Main(string[] args)
        //{
        //    var services = new ServiceCollection();

        //    //默认构造
        //    services.AddSingleton<IOperationSingleton, Operation>();
        //    //自定义传入GUID空值
        //    services.AddSingleton<IOperationSingleton>(new Operation(Guid.Empty));
        //    //自定义传入一个New的GUID
        //    services.AddSingleton<IOperationSingleton>(new Operation(Guid.NewGuid()));

        //    var provider = services.BuildServiceProvider();

        //    // 输出singletone1的Guid
        //    var singleton1 = provider.GetService<IOperationSingleton>();
        //    Console.WriteLine($"signletone1: {singleton1.OperationId}");

        //    // 输出singletone2的Guid
        //    var singleton2 = provider.GetService<IOperationSingleton>();
        //    Console.WriteLine($"signletone2: {singleton2.OperationId}");
        //    Console.WriteLine($"singletone1 == singletone2 ? : { singleton1 == singleton2 }");

        //    Console.WriteLine();

        //    var services2 = new ServiceCollection();
        //    services2.AddTransient<IOperationTransient, Operation>();

        //    var provider2 = services2.BuildServiceProvider();
        //    var transient1 = provider2.GetService<IOperationTransient>();
        //    Console.WriteLine($"transient1: {transient1.OperationId}");

        //    var transient2 = provider2.GetService<IOperationTransient>();
        //    Console.WriteLine($"transient2: {transient2.OperationId}");
        //    Console.WriteLine($"transient1 == transient2 ? : { transient1 == transient2 }");
        //}
    }

    public interface IOperation
    {
        Guid OperationId { get; }
    }

    public interface IOperationSingleton : IOperation { }

    public interface IOperationTransient : IOperation { }

    public interface IOperationScoped : IOperation { }

    public class Operation : IOperationSingleton, IOperationTransient, IOperationScoped
    {
        private Guid _guid;
        public Operation()
        {
            _guid = Guid.NewGuid();
        }

        public Operation(Guid guid)
        {
            _guid = guid;
        }

        public Guid OperationId => _guid;
    }
}
