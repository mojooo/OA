using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using SubSonic;
using System.Data;

namespace CanYou.OA.BLL
{
    public partial  class ContactInfo
    {
        //��ȡͨѶ¼��ͼ ��GVW
        public static DataTable getContactListVW()
        {
            Query q = Vw_ContactList.CreateQuery();
            return q.ExecuteDataSet().Tables[0];
        }

        //ɾ������ͨѶ¼ 
        public static void DelContact(int contactid)
        {
            Contact.Delete(contactid);
        }

        //��ѯ����
        public static DataTable EmployeeIdOfName(string employeeName)
        {
            DataTable dt = SPs.EmployeeIdOfName_Sp(employeeName).GetDataSet().Tables[0];
            return dt;

        }

        //���Ų�ѯ
        public static DataTable ContactOfDepart(int departid)
        {
            DataTable dt = SPs.ContactOfDepart_Sp(departid).GetDataSet().Tables[0];
            return dt;
        }

        //��ѯ��������
        public static DataTable ContactOfBoth(int departid, string employeename)
        {
            return SPs.ContactOfBoth_Sp(departid, employeename).GetDataSet().Tables[0];
        }
    }
}
