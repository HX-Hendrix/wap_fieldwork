using SDKConsts = SH3H.SDK.Share.Consts;

namespace SH3H.WAP.FieldWork.Share
{
    /// <summary>
    /// ��������ƽ̨��������ϵͳ����
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// ����RESTful����WAP��ַǰ׺
        /// </summary>
        public const string URL_PREFIX_WAP = SDKConsts.APP_NAME + "/fw/v1";


        #region �������

        /// <summary>
        /// Ӧ�û���key
        /// </summary>
        public const string URL_PREFIX_WAP_APP = "urn:wap:app";
        

        #endregion

        #region ��־���
        /// <summary>
        /// ����¼��־�������б�
        /// </summary>
        public const string LOG_METHOD_NAME_IGNORE = "GetUserByToken,AddAudit,TokenValid,Ping";

        #endregion
    }
}
