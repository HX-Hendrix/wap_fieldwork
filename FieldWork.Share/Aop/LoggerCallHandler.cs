using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SH3H.SDK.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SH3H.WAP.FieldWork.Share.Aop
{
    /// <summary>
    /// ��־��¼�ܵ�
    /// </summary>
    public class LoggerCallHandler : ICallHandler
    {

        private static List<string> _ignoreList;

        private static string _inputTemple = "ServiceName: {0}\r\nActionName: {1}\r\nParams: {2}";
        private static string _outputTemple = "ServiceName: {0}\r\nActionName: {1}\r\nResult: {2}";

        private static JsonSerializerSettings _setting ;


        static LoggerCallHandler()
        {
            InitSetting();
            InitIgnore();
        }


        /// <summary>
        /// �ܵ����ش���
        /// </summary>
        /// <param name="input">�������</param>
        /// <param name="getNext">ִ�е���һ���ܵ�</param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {


            LogBegin(input);

            IMethodReturn result = getNext()(input, getNext);

            LogEnd(result, input);

            return result;
        }

        /// <summary>
        /// ��¼���ÿ�ʼ
        /// </summary>
        /// <param name="input"></param>
        private void LogBegin(IMethodInvocation input)
        {
            MethodBase method = input.MethodBase;
            Type type = method.ReflectedType;
            var args = input.Arguments;

            if (IsIgnore(method, type))
            {
                return;
            }

            string serviceName = "";
            string actionName = "";

            //ƴ����Ҫ��¼�Ĳ���
            ParameterInfo[] paramInfos = method.GetParameters();
            StringBuilder sb = new StringBuilder();

            actionName = method.Name;
            serviceName = type.Name;
            int pcount = paramInfos.Length;

            for (int i = 0; i < pcount; i++)
            {
                var paramInfo = paramInfos[i];

                sb.Append(paramInfo.Name).Append("=");

                if (input.Inputs != null && input.Inputs.Count > i)
                {
                    sb.Append(GetValue(input.Inputs[i]));
                }
                else
                {
                    sb.Append("null");
                }

                sb.Append(',');
            }


            if (sb.Length > 0 && sb[sb.Length - 1] == ',')
            {
                sb.Length = sb.Length - 1;
            }

            //��װ��Ϣ
            string info = string.Format(_inputTemple, serviceName, actionName, sb.ToString());

            //��¼��Ϣ
            LogManager.Get().Trace(info);
        }

        /// <summary>
        /// ��¼���ý��
        /// </summary>
        /// <param name="result"></param>
        /// <param name="input"></param>
        private void LogEnd(IMethodReturn result, IMethodInvocation input)
        {
            if (result.Exception != null)
            {
                return;
            }

            MethodBase method = input.MethodBase;
            Type type = method.ReflectedType;
            var args = input.Arguments;

            if (IsIgnore(method, type))
            {
                return;
            }

            string serviceName = "";
            string actionName = "";

            //ƴ����Ҫ��¼�Ĳ���
            ParameterInfo[] paramInfos = method.GetParameters();
            StringBuilder sb = new StringBuilder();

            actionName = method.Name;
            serviceName = type.Name;
            int pcount = paramInfos.Length;


            if (result.ReturnValue == null)
            {
                sb.Append("null");
            }
            else
            {
                sb.Append(GetValue(result.ReturnValue));
            }

            //��װ��Ϣ
            string info = string.Format(_outputTemple, serviceName, actionName, sb.ToString());

            //��¼��Ϣ
            LogManager.Get().Trace(info);
        }

        public int Order { get; set; }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsIgnore(MethodBase method, Type type)
        {
            return method != null && _ignoreList.Contains(method.Name);
        }

        private static void InitIgnore()
        {
            _ignoreList = new List<string>();

            string ignoreStr = Consts.LOG_METHOD_NAME_IGNORE;

            if (!string.IsNullOrWhiteSpace(ignoreStr))
            {
                _ignoreList.AddRange(ignoreStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private static void InitSetting()
        {
            _setting = new JsonSerializerSettings();
            _setting.Error = delegate(object sender, ErrorEventArgs args)
            {
                args.ErrorContext.Handled = true;
            };
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

            return JsonConvert.SerializeObject(p, _setting);
        }
    }
}
