using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace castle_dynamic_proxy_test
{
    public class QuickIntro
    {
        /// <summary>
        /// https://stackoverflow.com/questions/6633914/castle-dynamic-proxy-not-intercepting-method-calls-when-invoked-from-within-the/
        /// 
        /// We can only intercept the call to the proxy, not the actual object itself.
        /// Interceptor is the way to define what to do before and after the actual method call.
        /// 
        /// </summary>
        public class Interceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                Console.WriteLine($"Intercepted call to: {invocation.Method.Name}");
                Console.WriteLine("Do something before");

                if (invocation.Method.Name.Equals("Method1"))
                {
                    Console.WriteLine("Method1");
                }
                else if (invocation.Method.Name.Equals("Method2"))
                {
                    Console.WriteLine("Method2");
                }
                invocation.Proceed();
                Console.WriteLine("Do something after");

            }
        }

        public class InterceptedClass
        {
            public virtual void Method1()
            {
                Console.WriteLine("Called Method 1");
                Method2();
            }
            public virtual void Method2()
            {
                Console.WriteLine("Called Method 2");
            }
        }

        private void Test1()
        {
            var cp = new ProxyGenerator().CreateClassProxy<InterceptedClass>(new Interceptor());
            cp.Method1();
            cp.Method2();
        }

        public void Run()
        {
            Test1();
        }
    }
}
