using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CanYou.OA.DAL;
using SubSonic;
namespace CanYou.OA.BLL
{
    public partial  class AssetInfo
    {
        //��ȡ�̶��ʲ���Ϣ��
        public static DataTable getAssetListVW()
        {
            Query q = Asset.Query();
            
            return q.ExecuteDataSet().Tables[0];
        }

        public static DataTable getExAssetList()
        {
            Query q = Vw_ExAsset.Query();
            return q.ExecuteDataSet().Tables[0];
        }
        //��ȡ������λUnit��Ϣ��
        public static DataTable getUnitList()
        {
            Query q = Unit.Query();
            return q.ExecuteDataSet().Tables[0];
        }

        //��ȡ��ŵص�Site��Ϣ��
        public static DataTable getSiteList()
        {
            Query q = Site.Query();
            return q.ExecuteDataSet().Tables[0];
        }

        //ɾ��Asset
        public static void DelAsset(int assetid)
        {
            Asset.Delete(assetid);
        }
    }
}
