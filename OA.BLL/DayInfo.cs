using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using System.Data;
using SubSonic;

namespace CanYou.OA.BLL
{
    public partial  class DayInfo
    {
        

        public static void DelDay(int dayid)
        {
            Day.Delete(dayid);
        }

        public static DataTable getWeekList()
        {
            Query q = Week.Query();
            return q.ExecuteDataSet().Tables[0];
        }

        //����׼�б�
        public static DataTable getWeekSumOfApprove(int RecvEmployeeId)
        {
            return SPs.WeekSumListOfApprove_Sp(RecvEmployeeId).GetDataSet().Tables[0];
        }

        //��ȡ��½�û���RoleName 
        public static string getRoleNameOfMaster(string masterName)
        {
            return SPs.RoleNameOfMaster_Sp(masterName).ExecuteScalar().ToString();
        }

        public static bool IsRole(string mastername)
        {
            DataTable dt = SPs.RoleNameOfMaster_Sp(mastername).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //ͨ��EmployeeId��ȡDepartId
        public static string GetDepartIdOfEmployee(int employeeid)
        {
            return SPs.DepartIdOfEmployee_Sp(employeeid).ExecuteScalar().ToString();
        }

        //ͨ��DepatId��ȡ���ܽ����������ߵ�EmployeeId
        public static string getRecvEmployeeId(int DepartId)
        {
            return SPs.EmployeeOfWeekSum_Sp(DepartId).ExecuteScalar().ToString();
        }

       
        //��ȡ�����ܽ�gw����ͼ
        public static DataTable getWeekSumListOfEmployee(int emid)
        {
            return SPs.WeekSumListOfEmployee_Sp(emid).GetDataSet().Tables[0];
        }


        //ɾ�����ܽ�
        public static void DelWeekSum(int weeksumid)
        {
            WeekSum.Delete(weeksumid);
        }

        //ɾ��������־
        public static void DelDaysOfWeekSum(int weeksumId)
        {
            Query q = Day.Query();
            q.AddWhere(Day.Columns.WeekSumId, weeksumId);
            q.QueryType = QueryType.Delete;
            q.Execute();
           
        }

        //��ȡ��Day����ͼ
        public static DataTable getDayList(int wksumId)
        {
            return SPs.DayListOfWK_Sp(wksumId).GetDataSet().Tables[0];
        }


        //����DLL
        public static DataTable getWeek()
        {
            Query q = Week.Query();
            return q.ExecuteDataSet().Tables[0];
        }
    }
}
