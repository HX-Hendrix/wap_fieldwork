using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.FieldWork.Share
{
    // <summary>
    /// ����Ȩ����֤ϵͳ������
    /// </summary>
    /// ͳһ��0x1002��ͷ��ʮ��������
    public static class StateCode
    {

        #region ҵ���޹ؼ���״̬��

        /// <summary>
        /// ģ��ת������
        /// </summary>
        public const int CODE_MODEL_CONVERT_ERROR = 0x1000000D;

        /// <summary>
        /// SQLִ���쳣
        /// </summary>
        public const int CODE_SQL_EXECUTE_ERROR = 0x1000000E;

        /// <summary>
        /// ģ�Ͳ�����
        /// </summary>
        public const int CODE_MODEL_NOT_EXIST = 0x1000000F;
        /// <summary>
        /// �����쳣
        /// </summary>
        public static int CODE_CACHE_ERROR = 0x10000010;

        /// <summary>
        /// ����������Ϊ��
        /// </summary>
        public const int CODE_ARGUMENT_NULL = 0x10000011;

        /// <summary>
        /// ��������
        /// </summary>
        public const int CODE_ARGUMENT_LENGTH = 0x10000012;

        /// <summary>
        /// �������ʹ���
        /// </summary>
        public const int CODE_ARGUMENT_TYPE_ERROR = 0x10000013;

        /// <summary>
        /// ��ȡ��Ŵ���
        /// </summary>
        public const int CODE_GET_SEQ_ERROR = 0x10000014;

        /// <summary>
        /// ������Χ����
        /// </summary>
        public const int CODE_ARGUMENT_LIMIT_ERROR = 0x10000015;

        /// <summary>
        /// ������һ��
        /// </summary>
        public const int CODE_ARGUMENT_NOT_EQUAL = 0x10000016;

        /// <summary>
        /// �����ظ�
        /// </summary>
        public const int CODE_ARGUMENT_DATA_REPEAT = 0x10000017;

        /// <summary>
        /// Ӧ�ñ�ʶ�Ѵ���
        /// </summary>
        public const int CODE_APP_EXIST = 0x10000018;

        /// <summary>
        /// �û���Ȩ��
        /// </summary>
        public const int CODE_USER_NO_AUTHORITY = 0x10000019;

        #endregion

        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        /// <param name="code">����״̬��</param>
        /// <returns>��������Ϣ</returns>
        public static string GetMessage(int code)
        {
            string message = "";
            _errorCodeDic.TryGetValue(code, out message);
            return message;
        }

        private static readonly Dictionary<int, string> _errorCodeDic;

        static StateCode()
        {
            _errorCodeDic = new Dictionary<int, string>();

            _errorCodeDic.Add(CODE_MODEL_CONVERT_ERROR, "ģ��ת������");
            _errorCodeDic.Add(CODE_SQL_EXECUTE_ERROR, "SQLִ���쳣");
            _errorCodeDic.Add(CODE_MODEL_NOT_EXIST, "ģ�Ͳ�����");
            _errorCodeDic.Add(CODE_CACHE_ERROR, "�����쳣");
            _errorCodeDic.Add(CODE_ARGUMENT_NULL, "����������Ϊ��");
            _errorCodeDic.Add(CODE_ARGUMENT_LENGTH, "��������");
            _errorCodeDic.Add(CODE_ARGUMENT_TYPE_ERROR, "�������ʹ���");
            _errorCodeDic.Add(CODE_GET_SEQ_ERROR, "��ȡ��Ŵ���");
            _errorCodeDic.Add(CODE_ARGUMENT_LIMIT_ERROR, "������Χ����");
            _errorCodeDic.Add(CODE_ARGUMENT_NOT_EQUAL, "������һ��");
            _errorCodeDic.Add(CODE_ARGUMENT_DATA_REPEAT, "�����ظ�");


            _errorCodeDic.Add(CODE_USER_NO_AUTHORITY, "�û���Ȩ��");

        }

    }
}
