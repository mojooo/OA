using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using System.Data;
using SubSonic;
namespace CanYou.OA.BLL
{
   public partial class RoleInfo
    {
       public static DataTable getRoleList()
       {
           Query q = Role.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       public static DataTable getRightList()
       {
           Query q = Action.Query();
           return q.ExecuteDataSet().Tables[0];
       }
       
       //��ɫ�Ƿ����
       public static bool isRoleMaster(int masterid)
       {
           Query q = RoleMaster.Query();
           q.AddWhere(RoleMaster.Columns.MasterId, masterid);
           DataTable dt = q.ExecuteDataSet().Tables[0];
           if (dt.Rows.Count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       //��ɫ�Ƿ����
       public static bool IsRoleMasters(int masterid, int roleid)
       {
           Query q = RoleMaster.Query();
           q.AddWhere(RoleMaster.Columns.RoleId, roleid);
           q.AddWhere(RoleMaster.Columns.MasterId, masterid);
           DataTable dt = q.ExecuteDataSet().Tables[0];
          
           if (dt.Rows.Count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       //����Ȩ���Ƿ����
       public static bool IsOperationRole(int roleid, int processid, int operationid)
       {

           Query q = OpProRole.Query();
           q.AddWhere(OpProRole.Columns.RoleId, roleid);
           q.AddWhere(OpProRole.Columns.ProcessId, processid);
           q.AddWhere(OpProRole.Columns.OperationId, operationid);
           DataTable dt = q.ExecuteDataSet().Tables[0];

           if (dt.Rows.Count>0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       //ProcessRole�����Ƿ����
       public static bool IsProcessRole(int processid, int roleid)
       {
           Query q = OpProRole.Query();
           q.AddWhere(OpProRole.Columns.RoleId, roleid);
           q.AddWhere(OpProRole.Columns.ProcessId, processid);
           DataTable dt = q.ExecuteDataSet().Tables[0];

           if (dt.Rows.Count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }

       }

       ////���¸�ֵ
       //public static void UpdateOperation(int operationid,int roleid,int processid)
       //{
       //    Query q = OpProRole.Query();
       //    q.AddWhere(OpProRole.Columns.RoleId, roleid);
       //    q.AddWhere(OpProRole.Columns.ProcessId, processid);
       //    q.AddUpdateSetting(OpProRole.Columns.OperationId, operationid);
       //    q.QueryType = QueryType.Update;
       //    q.Execute();
       //}

       //���
       //public static void UpdateOperation(int processid, int roleid)
       //{
       //    Query q = OpProRole.Query();
       //    q.AddWhere(OpProRole.Columns.RoleId, roleid);
       //    q.AddWhere(OpProRole.Columns.ProcessId, processid);
       //    q.AddUpdateSetting(OpProRole.Columns.OperationId, null);
       //    q.QueryType = QueryType.Update;
       //    q.Execute();

       //}

       //ɾ��ָ������Ȩ��
       public static void DelOperation(int processid, int roleid, int operationid)
       {
           Query q = OpProRole.Query();
           q.AddWhere(OpProRole.Columns.OperationId, operationid);
           q.AddWhere(OpProRole.Columns.RoleId, roleid);
           q.AddWhere(OpProRole.Columns.ProcessId, processid);
           q.QueryType = QueryType.Delete;
           q.Execute();
       }

       //��ѯָ�����̡���ɫ�Ĳ���
       public static DataTable getOperation(int processid, int roleid)
       {
           Query q = OpProRole.Query();
           q.AddWhere(OpProRole.Columns.RoleId, roleid);
           q.AddWhere(OpProRole.Columns.ProcessId, processid);
           return q.ExecuteDataSet().Tables[0];
       }
       //Ȩ���Ƿ����
       public static bool isActionMaster(int masterid)
       {
           Query q = ActionMaster.Query();
           q.AddWhere(ActionMaster.Columns.MasterId, masterid);
           DataTable dt = q.ExecuteDataSet().Tables[0];
           if (dt.Rows.Count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       //���½�ɫ
       public static void UpdateRoleMaster(int masterid,int roleid)
       {
           Query q = RoleMaster.Query();
           q.AddWhere(RoleMaster.Columns.MasterId, masterid);
           q.AddUpdateSetting(RoleMaster.Columns.RoleId, roleid);
           q.QueryType = QueryType.Update;
           q.Execute();
       }

    

       public static void UpdateRoleMasters(int masterid)
       {
           Query q = RoleMaster.Query();
           q.AddWhere(RoleMaster.Columns.MasterId, masterid);
           q.AddUpdateSetting(RoleMaster.Columns.IsPass, 1);
           q.QueryType = QueryType.Update;
           q.Execute();
       }

      //ɾ��Ȩ��
       public static void DelActionMaster(int masterid)
       {
           SPs.DelActionMaster_Sp(masterid).Execute();
       
       }

       //ɾ����ɫ
      public static void DelRoles(int masterid,int roleid)
      {
          Query q = RoleMaster.Query();
          q.AddWhere(RoleMaster.Columns.MasterId, masterid);
          q.AddWhere(RoleMaster.Columns.RoleId, roleid);
          q.QueryType = QueryType.Delete;
          q.Execute();
          
      }


       //��ȡ��ɫ
       public static DataTable GetRoleFromMaster(int masterid)
       {
           Query q = RoleMaster.Query();
           q.AddWhere(RoleMaster.Columns.MasterId, masterid);
           return q.ExecuteDataSet().Tables[0];
       }

       //��ȡȨ��
       public static DataTable getActionFromMaster(int masterid)
       {
           Query q = ActionMaster.Query();
           q.AddWhere(ActionMaster.Columns.MasterId, masterid);
           return q.ExecuteDataSet().Tables[0];
       }

       //��ѯ��ɫ�б�
       public static DataTable getRole()
       {
           Query q = Role.Query();
           return q.ExecuteDataSet().Tables[0];
       }
       //ɾ����ɫ����Ӧ�Ĳ������û�
       public static void DelRole(int id)
       {
           Query q = RoleMaster.Query();
           q.AddWhere(RoleMaster.Columns.RoleId, id);
           q.QueryType = QueryType.Delete;
           q.Execute();
           Query s = OpProRole.Query();
           s.AddWhere(OpProRole.Columns.RoleId, id);
           s.QueryType = QueryType.Delete;
           s.Execute();
           Role.Delete(id);
       }

       //��ѯ��ɫ���û��б�
       public static DataTable getUserRoleList()
       {
           Query q = Vw_UserRoleList.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ѯ���̱���ɫ����Ϣ
       public static DataTable getProcessRoleList()
       {
           Query q = Vw_ProcessRole.Query();
           return q.ExecuteDataSet().Tables[0];
       }

       //��ѯ����
       public static DataTable getOperationList()
       {
           Query q = Operation.Query();
           return q.ExecuteDataSet().Tables[0];
       }

  
      
 
    }
}
