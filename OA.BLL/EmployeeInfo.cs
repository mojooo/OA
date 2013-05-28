using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SubSonic;
using CanYou.OA.DAL;

namespace CanYou.OA.BLL
{
   public partial class EmployeeInfo
    {
       public static DataTable GetEmployeeList()
       {
           Query q = Employee.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡgridviewԱ�������ڲ�����ͼ
       public static DataTable EmployeeList()
       {
           Query q = Vw_EmployeeInfo.CreateQuery();
           return q.ExecuteDataSet().Tables[0];
       }

       
      //��ȡԱ����Ϣ
       public static DataTable getEmployeeList(int employeeid)
       {
           return SPs.EmployeeList_Sp(employeeid).GetDataSet().Tables[0];
           
       }



       //��ȡ���Ű�����Ա����Ϣ
       public static DataTable getEmployeeOfDepart(int departid)
       {
           return SPs.EmployeeOfDepart_Sp(departid).GetDataSet().Tables[0];
       }

       //��ȡ��½�û�����Ա��Id
       public static string   getEmployeeOfMaster(string MasterName, string MasterPsd)
       {
           Query q = Master.Query();
           q.AddWhere(Master.Columns.MasterName, MasterName);
           q.AND(Master.Columns.MasterPsd, MasterPsd);
           DataTable dt = q.ExecuteDataSet().Tables[0];
           return dt.Rows[0]["EmployeeId"].ToString();

       }
       //ɾ��Ա����Ϣ
       public static void EmployeeDel(int EmployeeId)
       {
           Employee.Delete(EmployeeId);
       }

       //��ȡѧ����Ϣ��
       public static DataTable getEducationList()
       {
           Query q = Education.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡ������Ϣ��
       public static DataTable getLanguageList()
       {
           Query q = Language.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡ������ò��Ϣ��
       public static DataTable getPoliticsList()
       {
           Query q = Politics.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡְλ��Ϣ��
       public static DataTable getPositionList()
       {
           Query q = Position.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡ������Ϣ��
       public static DataTable getDepartList()
       {
           Query q = Depart.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡ�ļ�������Ϣ��
       public static DataTable getFileType()
       {
           Query q = FileType.Query();
           return q.ExecuteDataSet().Tables[0];
           
       }

       //��ѯ������Ϣ
       public static DataTable getEmDang()
       {
           Query q = EmDang.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ѯ������Ϣ
       public static DataTable getEmLevel()
       {
           Query q = EmLevel.Query();
           return q.ExecuteDataSet().Tables[0];
       }

    }
}
