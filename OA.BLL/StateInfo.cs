using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using SubSonic;
using System.Data;

namespace CanYou.OA.BLL
{
   public partial class StateInfo
    {
        //��ѯ״̬ID��һ������
        public static int getStateId1()
        {
            Query q = State.Query();
            q.AddWhere(State.Columns.StateName, "һ������");
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return Convert.ToInt32(dt.Columns["StateId"].ToString());
        }
        //��ѯ״̬ID��һ��δ��
        public static int getStateId2()
        {
            Query q = State.Query();
            q.AddWhere(State.Columns.StateName, "һ��δ��");
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return Convert.ToInt32(dt.Columns["StateId"].ToString());
        }
        //��ѯ״̬ID����������
        public static int getStateId3()
        {
            Query q = State.Query();
            q.AddWhere(State.Columns.StateName, "��������");
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return Convert.ToInt32(dt.Columns["StateId"].ToString());
        }
        //��ѯ״̬ID���������
        public static int getStateId4()
        {
            Query q = State.Query();
            q.AddWhere(State.Columns.StateName, "�������");
            DataTable dt = q.ExecuteDataSet().Tables[0];
            return Convert.ToInt32(dt.Columns["StateId"].ToString());
        }
    }
}
