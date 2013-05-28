using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using System.Data;
using SubSonic;

namespace CanYou.OA.BLL
{
   public partial class OverTimeInfo
    {
       public static DataTable getOtGroup(int otid)
       {
           Query q = OtGroup.Query();
           q.AddWhere(OtGroup.Columns.OverTimeId, otid);
           return q.ExecuteDataSet().Tables[0];
       }

       public static void DelQtGroup(int otid)
       {
           OtGroup.Delete(otid);
       }

       public static void DelGroupOfQt(int otid)
       {
           Query q = OtGroup.Query();
           q.AddWhere(OtGroup.Columns.OverTimeId, otid);
           q.QueryType = QueryType.Delete;
           q.Execute();
       }

       public static void DelOverTime(int otid)
       {
           OverTime.Delete(otid);

       }

       public static DataTable  getOverTime(string emName)
       {
           Query q = OverTime.Query();
           q.AddWhere(OverTime.Columns.ApplyEmName, emName);
           return q.ExecuteDataSet().Tables[0];
       }

       public static DataTable getAllApproveOvertime()
       {
           Query q = OverTime.Query();
           q.AddWhere(OverTime.Columns.IsSubmit, "����");
           q.OR(OverTime.Columns.IsSubmit, "ͨ��");
           q.OR(OverTime.Columns.IsSubmit, "δͨ��");
           return q.ExecuteDataSet().Tables[0];
       }

       //��ѯͨ���������������뵥
       public static DataTable getAllOtPass()
       {
           Query q = OverTime.Query();
           q.AddWhere(OverTime.Columns.IsSubmit, "ͨ��");
           return q.ExecuteDataSet().Tables[0];
       }
    }
}
