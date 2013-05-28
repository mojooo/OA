using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using SubSonic;
using System.Data;

namespace CanYou.OA.BLL
{
    public class Common
    {


        public static string getEmIdOfLeadDepart(string RoleName, int DepartId)
        {
            DataTable dt = SPs.EmIdOfLeadDepart_Sp(RoleName, DepartId).GetDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        //��ȡ�г�������Id
        public static string getEmployeeIdOfMarketManager()
        {
            Query q = Vw_EmployeeIdOfMarket.Query();
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        //��ȡ����������Id
        public static string getEmployeeIdOfTechManager()
        {
            Query q = Vw_EmployeeIdOfTechManager.Query();
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        //��ȡ����������Id

        public static string getEmployeeIdOfFinaceManager()
        {
            Query q = Vw_EmployeeIdOfFinaceManager.Query();
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        //��ȡ�ܾ���Id
        public static string getEmployeeIdOfManager()
        {
            Query q = Vw_EmployeeIdOfManager.Query();
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        //��ȡ�ۺϲ�������б�
        public static string getEmployeeIdOfAdmin()
        {
            Query q = Vw_EmployeeIdOfAdmin.Query();
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        public static string getEmployeeIdOfDriver()
        {
            Query q = Vw_EmployeeIdOfDriver.Query();
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return dt.Rows[0]["EmployeeId"].ToString();
        }

        ////��ȡEmploy 
        //public static DataTable getEmployeeOf(string DepartName, string RoleName)
        //{
        //    return SPs.EmployeeOfX_Sp(DepartName, RoleName).GetDataSet().Tables[0];
        //}

        //�ж�ָ��Ա��Ȩ���Ƿ����
        public static bool IsActionNameOfMaster(int emid, string action)
        {
            DataTable dt = SPs.IsActionNameOfMaster_Sp(emid, action).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

    }
}
