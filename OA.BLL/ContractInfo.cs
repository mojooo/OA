using System;
using System.Collections.Generic;
using System.Text;
using CanYou.OA.DAL;
using System.Data;
using SubSonic;

namespace CanYou.OA.BLL
{
    public partial class ContractInfo
    {
        //��ȡ���к�ͬ�б�
        public static DataTable getContractList()
        {
            Query q = Contract.Query();
            return q.ExecuteDataSet().Tables[0];
        }
        //ɾ����ͬ
        public static void DelContract(int contractid)
        {
            Contract.Delete(contractid);
        }
        //��ѯ��ͬ����
        public static DataTable getContractTypeList()
        {
            Query q = ContractType.Query();
            return q.ExecuteDataSet().Tables[0];
        }
       
    }
}
