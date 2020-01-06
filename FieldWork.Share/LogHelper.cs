using Newtonsoft.Json;
using SH3H.SDK.Infrastructure.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace SH3H.WAP.FieldWork.Share
{
    public class ServiceLogHelper
    {

        private static string input = "ServiceName: {0}\r\nActionName: {1}\r\nParams: {2}";
        private static string output = "ServiceName: {0}\r\nActionName: {1}\r\nResult: {2}";

        /// <summary>
        /// ��¼������־
        /// </summary>
        /// <param name="paraArry"></param>
        public static void LogServiceBegin(params object[] paraArry)
        {
            LogServiceInfo(input, true, paraArry);
        }

        /// <summary>
        /// ��¼������־
        /// </summary>
        /// <param name="paraArry"></param>
        public static void LogServiceEnd(params object[] paraArry)
        {
            LogServiceInfo(output, false, paraArry);
        }

        /// <summary>
        /// ��¼��־
        /// </summary>
        /// <param name="temple"></param>
        /// <param name="isIn"></param>
        /// <param name="paraArry"></param>
        public static void LogServiceInfo(string temple, bool isIn, params object[] paraArry)
        {
            string serviceName = "";
            string actionName = "";

            StackTrace stackTrace = new StackTrace();
            if (stackTrace.FrameCount == 0)
            {
                return;
            }

            //��ȡ����������������
            StackFrame lastStackFrame = null;
            MethodBase method = null;
            Type type = null;

            //��ȡ���ж�ջ
            StackFrame[] frames = stackTrace.GetFrames();

            if (frames == null)
            {
                return;
            }

            //���ն�ջ����(������ǰ)������ȡ�����ü�¼�����ķ��� ����������
            foreach (var item in frames)
            {
                method = item.GetMethod();
                type = method.ReflectedType;
                if (type == typeof(ServiceLogHelper))
                {
                    continue;
                }
                lastStackFrame = item;
                break;
            }
            if (lastStackFrame == null)
            {
                //δ���ҵ����Լ�¼����
                return;
            }

            //ƴ����Ҫ��¼�Ĳ���
            ParameterInfo[] paramInfos = method.GetParameters();
            StringBuilder sb = new StringBuilder();

            actionName = method.Name;
            serviceName = type.Name;
            int pcount = paramInfos.Length;

            if (isIn)
            {
                for (int i = 0; i < pcount; i++)
                {
                    var paramInfo = paramInfos[i];

                    sb.Append(paramInfo.Name).Append("=");

                    if (paraArry != null && paraArry.Length > i)
                    {
                        sb.Append(GetValue(paraArry[i]));
                    }
                    else
                    {
                        sb.Append("null");
                    }

                    sb.Append(',');
                }
            }
            else if (paraArry.Length > 0)
            {
                foreach (var item in paraArry)
                {
                    if (item == null)
                    {
                        sb.Append("null").Append(",");
                    }
                    else
                    {
                        sb.Append(GetValue(item)).Append(",");
                    }
                }
            }

            if (sb.Length > 0 && sb[sb.Length - 1] == ',')
            {
                sb.Length = sb.Length - 1;
            }

            //��װ��Ϣ
            string info = string.Format(temple, serviceName, actionName, sb.ToString());

            //��¼��Ϣ
            LogManager.Get().Trace(info);
        }

        /// <summary>
        /// ��ȡ���Ӷ��������ʽ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static string GetValue(object p)
        {
            if (p == null)
            {
                return null;
            }

            var type = p.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return p.ToString();
            }

            return JsonConvert.SerializeObject(p);
        }
    }
}
