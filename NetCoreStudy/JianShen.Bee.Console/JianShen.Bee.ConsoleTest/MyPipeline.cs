using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Bee.ConsoleTest
{
    class MyPipeline
    {
        public static List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();
        //模拟ApplicationBuilder中的app.Use方法
        public static void Use(Func<RequestDelegate,RequestDelegate> middleware)
        {
            _list.Add(middleware);
        }
        static void Main(string[] args)
        {
            //第一步
            Use(next => {
                return context =>
                {
                    Console.WriteLine("1");
                    return next.Invoke(context);
                    //return Task.CompletedTask;
                };
            });

            //第二步
            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("2");
                    return next.Invoke(context);
                };
            });

            RequestDelegate end = (context) =>
            {
                Console.WriteLine("end..");
                return Task.CompletedTask;
            };

            _list.Reverse();

            foreach (var middleware in _list)
            {
                end = middleware.Invoke(end);
            }

            //这时候我们没有Context,所以直接new一个Context给它
            end.Invoke(new Context());

            Console.ReadLine();
        }
    }

    public class Context
    {

    }

    public delegate Task RequestDelegate(Context context);


}
