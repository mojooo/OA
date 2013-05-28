using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using SubSonic;
using System.Data;

namespace CanYou.OA.BLL
{
    public partial class ContractApplyInfo
    {
       

        //�����¼
        public static DataTable getContractApplyOfEm(String ApplyName)
        {
            return SPs.ContractApplyOfEm(ApplyName).GetDataSet().Tables[0];
        }
        //����������¼
        public static DataTable getContractApplyOfDepart(String departName)
        {
            return SPs.ContractOfDepart(departName).GetDataSet().Tables[0];
        }
        //�ܾ���������¼
        public static DataTable getContractApplyOfManager()
        {
            Query q = Vw_ContractApplyManager.Query();
            return q.ExecuteDataSet().Tables[0];
        }
        //ɾ����¼
        public static void DelContractApply(int cid)
        {
            ContractApply.Delete(cid);
        }

        //�Ƿ������ͬ�ļ���
        public static bool IsContractFileSame(string FileName)
        {
            DataTable dt = SPs.ConctractFileSame_Sp(FileName).GetDataSet().Tables[0];
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
