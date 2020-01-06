using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace SH3H.WAP.Share.Log
{
    /// <summary>
    /// ��־��ǩ
    /// </summary>
    public class LogHandlerAttribute : HandlerAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LoggerCallHandler();
        }

    }

}
