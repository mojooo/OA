using System;
using System.Collections.Generic;
using System.Text;

namespace KingControls
{
    /// <summary>
    /// Author: ��ҹսӥ����רע��DotNet��������ChengKing(ZhengJian)��
    /// ��ñ���ĸ����½�:��http://blog.csdn.net/ChengKing/archive/2008/08/18/2792440.aspx��
    /// ����: ��������Ϊ����Asp.net������һЩ���¡���ת��ʱ�뱣��������Դ��
    /// </summary>
    public delegate void DataEventHandler(object sender, DataEventArgs e);
    public class DataEventArgs : EventArgs
    {
        public DataEventArgs()
        {
        }

        private string strDataValue;
        public string DataValue
        {
            get { return strDataValue; }
            set { strDataValue = value; }
        }
    }
}
