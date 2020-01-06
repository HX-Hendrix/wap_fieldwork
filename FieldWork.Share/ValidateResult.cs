using SH3H.SDK.Definition.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.FieldWork.Share
{
    /// <summary>
    /// ��֤�����
    /// </summary>
    public class ValidateResult
    {
        /// <summary>
        /// �Ƿ���֤ͨ��
        /// </summary>
        public bool IsValid { get; private set; }
        /// <summary>
        /// ����
        /// </summary>
        public IList<ErrorState> Errors { get; private set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        public ValidateResult()
        {
            IsValid = true;
            Errors = new List<ErrorState>();
        }
        /// <summary>
        /// ��Ӵ���
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public void AddError(int code, string msg)
        {
            IsValid = false;

            Errors.Add(new ErrorState() { Code = code, Message = msg });
        }
        /// <summary>
        /// ����쳣
        /// </summary>
        /// <returns></returns>
        public WapException BuildException()
        {
            if (IsValid || Errors.Count() == 0)
                return null;

            var e = Errors.First();

            return new WapException(e.Code, e.Message);
        }
    }
}
