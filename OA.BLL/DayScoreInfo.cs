using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using SubSonic;
using System.Data;

namespace CanYou.OA.BLL
{
    public partial class DayScoreInfo
    {
         public static DataTable getAddDayScore(int wkid)
         {
             //Query q = DayScore.Query();
             //q.AddWhere(DayScore.Columns.WeekScoreId, wkid);
             //return q.ExecuteDataSet().Tables[0];

             return SPs.DayScore_Sp(wkid).GetDataSet().Tables[0];
         }

         public static void DelDayScore(int daid)
         {
             DayScore.Delete(daid);
         }
         public static void  DelDaysOfWk(int wkid)
         {
             Query q = DayScore.Query();
             q.AddWhere(DayScore.Columns.WeekScoreId, wkid);
             q.QueryType = QueryType.Delete;
             q.Execute();

             Query s = WeekScore.Query();
             s.AddWhere(WeekScore.Columns.WeekScoreId, wkid);
             s.QueryType = QueryType.Delete;
             s.Execute();
         }

         public static DataTable getDayRole()
         {
             Query q = DayRole.Query();
             return q.ExecuteDataSet().Tables[0];
         }

        //��ѯ�ض��˵��ܱ�
        public static DataTable getWeekScore(int emid)
        {
            Query q = WeekScore.Query();
            q.AddWhere(WeekScore.Columns.EmployeeId, emid);
            return q.ExecuteDataSet().Tables[0];
        }

        //��ѯָ��Id���ܱ�
        public static DataTable getWk(int wkid)
        {
            Query q = WeekScore.Query();
            q.AddWhere(WeekScore.Columns.WeekScoreId, wkid);
            return q.ExecuteDataSet().Tables[0];
        }

        //ɾ��ָ��Id���ܱ�
        public static void DelWeekScore(int wkid)
        {
            WeekScore.Delete(wkid);
        }

        //��ѯ���������������ܱ�
        public static DataTable getWksOfTm()
        {
            Query q = Vw_WksofTech.Query();
            return q.ExecuteDataSet().Tables[0];
        }
        
        public static String  getSumScore(int emid,String date1,String date2)
        {
            DataTable dt =  SPs.ScoreByNameAndDate_Sp(emid, date1, date2).GetDataSet().Tables[0];
            return dt.Rows[0]["SumScore"].ToString(); 

        }

        public static DataTable getScoreByCondition(int emid, String date1, String date2)
        {
            DataTable dt = SPs.ScoreGv_Sp(emid, date1, date2).GetDataSet().Tables[0];
            return dt;
        }
    
  
    }
}
