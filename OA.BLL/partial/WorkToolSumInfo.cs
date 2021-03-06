﻿// ======================================================================================================================================
// 类说明
//=======================================================================================================================================
// 创建者：sk @Copy Right 残友软件有限公司OA项目 
// 文件名：WorkToolSumInfo.cs
// 创建时间：2012-10-30
// 版本说明：自动生成器版本1.1:本版本及以后版本不再扩展搜索排序功能，UI层要访问这些功能，都必须手工写逻辑层方法，以提高业务方法的重用性。
//			本版本距上个版本的改进有:缓存，基类,一对多关系，多对一关系，事务，强制规则（扩展日志和排序使用），Fix不支持objectdataSource的BUG,增加了对象的COPY和数据库对像的List整体转换的功能
//									解决了不选视图不能生成的问题
//			下个版本2.0将会增加多对多关系，完成ui界面的自动化生成
// 注意事项：请不要在此文件中写任何代码!!!请注意你的数据库命名必须依照规范，否则生成的代码不能确保可用!!!本系统中依赖于数据库中表的关系，和唯一关系，
//			因此删除使用了联级删除，请在数据库中配好相应的删除规则!!!本版本暂不支持GUID,只支持自增长主键!!!
//
// ======================================================================================================================================
using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using CanYouLib;
using CanYouLib.Exceptions;
using CanYouLib.Common;
using CanYou.OA.DAL;
using SubSonic;
using CanYou;

namespace CanYou.OA.BLL
{
	/// <summary>
	///WorkToolSumInfo  Data
	/// </summary>
	[Serializable]
	public partial class WorkToolSumInfo:CanYou.EntityObject
	{
		#region 属性
		///<summary>
		///
		///</summary>
		private int workToolSumId;
		///<summary>
		///
		///</summary>
		private string departName = String.Empty;
		///<summary>
		///
		///</summary>
		private string reason = String.Empty;
		///<summary>
		///
		///</summary>
		private string bigMoney = String.Empty;
		///<summary>
		///
		///</summary>
		private string smaMoney = String.Empty;
		///<summary>
		///
		///</summary>
		private string moneyStyle = String.Empty;
		///<summary>
		///
		///</summary>
		private string useDepartName = String.Empty;
		///<summary>
		///
		///</summary>
		private string fuTime = String.Empty;
		///<summary>
		///
		///</summary>
		private string applyName = String.Empty;
		///<summary>
		///
		///</summary>
		private int? state;
		///<summary>
		///
		///</summary>
		private string applyTime = String.Empty;
		///<summary>
		///
		///</summary>
		private string departView = String.Empty;
		///<summary>
		///
		///</summary>
		private string managerView = String.Empty;
		
		
		///一对多关系,如果
		private bool m_Loaded=false;
		#endregion
		
		#region 属性枚举
		public struct Fields
		{
			public const string WorkToolSumId=@"WorkToolSumId";
			public const string DepartName=@"DepartName";
			public const string Reason=@"Reason";
			public const string BigMoney=@"BigMoney";
			public const string SmaMoney=@"SmaMoney";
			public const string MoneyStyle=@"MoneyStyle";
			public const string UseDepartName=@"UseDepartName";
			public const string FuTime=@"FuTime";
			public const string ApplyName=@"ApplyName";
			public const string State=@"State";
			public const string ApplyTime=@"ApplyTime";
			public const string DepartView=@"DepartView";
			public const string ManagerView=@"ManagerView";
						
		}

		#endregion
		
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public WorkToolSumInfo()
		{
		}
		///<summary>
		///
		///</summary>
		public WorkToolSumInfo(int pWorkToolSumId,string pDepartName,string pReason,string pBigMoney,string pSmaMoney,string pMoneyStyle,string pUseDepartName,string pFuTime,string pApplyName,int? pState,string pApplyTime,string pDepartView,string pManagerView)
		{
			workToolSumId = pWorkToolSumId;
			departName    = pDepartName;
			reason        = pReason;
			bigMoney      = pBigMoney;
			smaMoney      = pSmaMoney;
			moneyStyle    = pMoneyStyle;
			useDepartName = pUseDepartName;
			fuTime        = pFuTime;
			applyName     = pApplyName;
			state         = pState;
			applyTime     = pApplyTime;
			departView    = pDepartView;
			managerView   = pManagerView;
			
		}
		
		
		///<summary>
		///
		///</summary>
		public WorkToolSumInfo(int workToolSumId)
		{
			 LoadFromId(workToolSumId);
		}
		
		private void LoadFromId(int workToolSumId)
		{
		 
			if (CachedEntityCommander.IsTypeRegistered(typeof(WorkToolSumInfo)))
            {
				WorkToolSumInfo workToolSumInfo=Find(GetList(), workToolSumId);
				if(workToolSumInfo==null)
					throw new AppException("未能在缓存中找到相应的键值对象");
                Copy(workToolSumInfo, this);
            }
            else
            {	WorkToolSum workToolSum=new WorkToolSum( workToolSumId);	
				if(workToolSum.IsNew)
				throw new AppException("尚未初始化");
               	LoadFromDAL(this, workToolSum);
            }
		}
		
		
		
		#endregion
		
	
		
		#region 静态方法
		
		/// <summary>
        /// 查找
        /// </summary>
        /// <param name="list"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static  WorkToolSumInfo Find(List< WorkToolSumInfo> list,  int workToolSumId)
        {
             return list.Find(delegate( WorkToolSumInfo item) { return item. workToolSumId==workToolSumId; });
        }
		
		
		/// <summary>
        /// 得到缓存键
        /// </summary>
        /// <returns></returns>
        private static string GetCacheKey()
        {
            return string.Format(CachedEntityCommander.CacheKey, typeof( WorkToolSumInfo).ToString());
        }
		
		
        /// <summary>
        /// 清空缓存
        /// </summary>
        internal static void ResetCache()
        {
            string cacheKey = GetCacheKey();
            CachedEntityCommander.RemoveCache(cacheKey);
        }
		
      
		/// <summary>
        /// 复制一个对象，采用硬编码的方式，避免反射的低效
        /// </summary>
        /// <param name="pIndustryTypeInfoFrom"></param>
        /// <param name="pIndustryTypeInfoTo"></param>
        public static void Copy(WorkToolSumInfo pWorkToolSumInfoFrom, WorkToolSumInfo pWorkToolSumInfoTo)
        {
	 		pWorkToolSumInfoTo.WorkToolSumId = pWorkToolSumInfoFrom.workToolSumId;
	 		pWorkToolSumInfoTo.DepartName = pWorkToolSumInfoFrom.departName;
	 		pWorkToolSumInfoTo.Reason = pWorkToolSumInfoFrom.reason;
	 		pWorkToolSumInfoTo.BigMoney = pWorkToolSumInfoFrom.bigMoney;
	 		pWorkToolSumInfoTo.SmaMoney = pWorkToolSumInfoFrom.smaMoney;
	 		pWorkToolSumInfoTo.MoneyStyle = pWorkToolSumInfoFrom.moneyStyle;
	 		pWorkToolSumInfoTo.UseDepartName = pWorkToolSumInfoFrom.useDepartName;
	 		pWorkToolSumInfoTo.FuTime = pWorkToolSumInfoFrom.fuTime;
	 		pWorkToolSumInfoTo.ApplyName = pWorkToolSumInfoFrom.applyName;
	 		pWorkToolSumInfoTo.State = pWorkToolSumInfoFrom.state;
	 		pWorkToolSumInfoTo.ApplyTime = pWorkToolSumInfoFrom.applyTime;
	 		pWorkToolSumInfoTo.DepartView = pWorkToolSumInfoFrom.departView;
	 		pWorkToolSumInfoTo.ManagerView = pWorkToolSumInfoFrom.managerView;
			pWorkToolSumInfoTo.Loaded=pWorkToolSumInfoFrom.Loaded;
        }
		
		/// <summary>
		/// 批量装载
		/// </summary>
		internal static void LoadFromDALPatch(List< WorkToolSumInfo> pList, WorkToolSumCollection pCollection)
        {
            foreach (WorkToolSum workToolSum in pCollection)
            {
                WorkToolSumInfo workToolSumInfo = new WorkToolSumInfo();
                LoadFromDAL(workToolSumInfo, workToolSum );
                pList.Add(workToolSumInfo);
            }
			
        }

		//从后台获取数据
		internal  static void  LoadFromDAL(WorkToolSumInfo pWorkToolSumInfo, WorkToolSum  pWorkToolSum)
		{
	 		pWorkToolSumInfo.workToolSumId = pWorkToolSum.WorkToolSumId;
	 		pWorkToolSumInfo.departName = pWorkToolSum.DepartName;
	 		pWorkToolSumInfo.reason = pWorkToolSum.Reason;
	 		pWorkToolSumInfo.bigMoney = pWorkToolSum.BigMoney;
	 		pWorkToolSumInfo.smaMoney = pWorkToolSum.SmaMoney;
	 		pWorkToolSumInfo.moneyStyle = pWorkToolSum.MoneyStyle;
	 		pWorkToolSumInfo.useDepartName = pWorkToolSum.UseDepartName;
	 		pWorkToolSumInfo.fuTime = pWorkToolSum.FuTime;
	 		pWorkToolSumInfo.applyName = pWorkToolSum.ApplyName;
	 		pWorkToolSumInfo.state = pWorkToolSum.State;
	 		pWorkToolSumInfo.applyTime = pWorkToolSum.ApplyTime;
	 		pWorkToolSumInfo.departView = pWorkToolSum.DepartView;
	 		pWorkToolSumInfo.managerView = pWorkToolSum.ManagerView;
			pWorkToolSumInfo.Loaded=true;
			
			}
		
		//数据持久化
		internal  static void  SaveToDb(WorkToolSumInfo pWorkToolSumInfo, WorkToolSum  pWorkToolSum,bool pIsNew)
		{
	 		pWorkToolSum.WorkToolSumId = pWorkToolSumInfo.workToolSumId;
	 		pWorkToolSum.DepartName = pWorkToolSumInfo.departName;
	 		pWorkToolSum.Reason = pWorkToolSumInfo.reason;
	 		pWorkToolSum.BigMoney = pWorkToolSumInfo.bigMoney;
	 		pWorkToolSum.SmaMoney = pWorkToolSumInfo.smaMoney;
	 		pWorkToolSum.MoneyStyle = pWorkToolSumInfo.moneyStyle;
	 		pWorkToolSum.UseDepartName = pWorkToolSumInfo.useDepartName;
	 		pWorkToolSum.FuTime = pWorkToolSumInfo.fuTime;
	 		pWorkToolSum.ApplyName = pWorkToolSumInfo.applyName;
	 		pWorkToolSum.State = pWorkToolSumInfo.state;
	 		pWorkToolSum.ApplyTime = pWorkToolSumInfo.applyTime;
	 		pWorkToolSum.DepartView = pWorkToolSumInfo.departView;
	 		pWorkToolSum.ManagerView = pWorkToolSumInfo.managerView;
			pWorkToolSum.IsNew=pIsNew;
			string UserName = SubsonicHelper.GetUserName();
			try
			{
				pWorkToolSum.Save(UserName);
			}
			catch(Exception ex)
			{
				LogManager.getInstance().getLogger(typeof(WorkToolSumInfo)).Error(ex);
				if(ex.Message.Contains("插入重复键"))//违反了唯一键
				{
					throw new AppException("此对象已经存在");//此处等待优化可以从唯一约束中直接取出提示来，如果没有的话，默认为原始的出错提示
				}
				throw new AppException("保存失败");
			}
			pWorkToolSumInfo.workToolSumId = pWorkToolSum.WorkToolSumId;
			//如果缓存存在，更新缓存
			if (CachedEntityCommander.IsTypeRegistered(typeof(WorkToolSumInfo))) 
            {
                ResetCache();
            }
		}
		
		/// <summary>
        /// 获得分页列表,无论是否是缓存实体都从数据库直接拿取数据
        /// </summary>
        /// <param name="pPageIndex">页数</param>
        /// <param name="pPageSize">每页列表</param>
        /// <param name="pOrderBy">排序</param>
		/// <param name="pSortExpression">排序字段</param>
		/// <param name="pRecordCount">列表行数</param>
        /// <returns>数据分页</returns>
        public static List< WorkToolSumInfo> GetPagedList(int pPageIndex,int pPageSize,SortDirection pOrderBy,string pSortExpression,out int pRecordCount)
        {
			if(pPageIndex<=1)
			pPageIndex=1;
            List< WorkToolSumInfo> list = new List< WorkToolSumInfo>();
          
            Query q = WorkToolSum .CreateQuery();
            q.PageIndex = pPageIndex;
            q.PageSize = pPageSize;
            q.ORDER_BY(pSortExpression,pOrderBy.ToString());
			WorkToolSumCollection  collection=new  WorkToolSumCollection();
		 	collection.LoadAndCloseReader(q.ExecuteReader());
          
            foreach (WorkToolSum  workToolSum  in collection)
            {
                WorkToolSumInfo workToolSumInfo = new WorkToolSumInfo();
                LoadFromDAL(workToolSumInfo,   workToolSum);
                list.Add(workToolSumInfo);
            }
			pRecordCount=q.GetRecordCount();
			
            return list;

        }
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		/// <returns></returns>
		public static List< WorkToolSumInfo> GetList()
		{
			string cacheKey = GetCacheKey();
			//本实体已经注册成缓存实体，并且缓存存在的时候，直接从缓存取
			if (CachedEntityCommander.IsTypeRegistered(typeof(WorkToolSumInfo)) && CachedEntityCommander.GetCache(cacheKey) != null)
            {
                return CachedEntityCommander.GetCache(cacheKey) as List< WorkToolSumInfo>;
            }
            else 
            {
				List< WorkToolSumInfo>  list =new List< WorkToolSumInfo>();
				WorkToolSumCollection  collection=new  WorkToolSumCollection();
				Query qry = new Query(WorkToolSum.Schema);
				collection.LoadAndCloseReader(qry.ExecuteReader());
				foreach(WorkToolSum workToolSum in collection)
				{
					WorkToolSumInfo workToolSumInfo= new WorkToolSumInfo();
					LoadFromDAL(workToolSumInfo,workToolSum);
					list.Add(workToolSumInfo);
				}
			  	//生成缓存
                if (CachedEntityCommander.IsTypeRegistered(typeof(WorkToolSumInfo))) 
                {
                    CachedEntityCommander.SetCache(cacheKey, list);
                }
				return list;
			}
			
		
		}
		#endregion
		
		
		
		
		#region 公有属性
		
		///<summary>
		///
		///</summary>
		public int WorkToolSumId
		{
			get {return workToolSumId;}
			set {workToolSumId = value;}
		}

		///<summary>
		///
		///</summary>
		public string DepartName
		{
			get {return departName;}
			set {departName = value;}
		}

		///<summary>
		///
		///</summary>
		public string Reason
		{
			get {return reason;}
			set {reason = value;}
		}

		///<summary>
		///
		///</summary>
		public string BigMoney
		{
			get {return bigMoney;}
			set {bigMoney = value;}
		}

		///<summary>
		///
		///</summary>
		public string SmaMoney
		{
			get {return smaMoney;}
			set {smaMoney = value;}
		}

		///<summary>
		///
		///</summary>
		public string MoneyStyle
		{
			get {return moneyStyle;}
			set {moneyStyle = value;}
		}

		///<summary>
		///
		///</summary>
		public string UseDepartName
		{
			get {return useDepartName;}
			set {useDepartName = value;}
		}

		///<summary>
		///
		///</summary>
		public string FuTime
		{
			get {return fuTime;}
			set {fuTime = value;}
		}

		///<summary>
		///
		///</summary>
		public string ApplyName
		{
			get {return applyName;}
			set {applyName = value;}
		}

		///<summary>
		///
		///</summary>
		public int? State
		{
			get {return state;}
			set {state = value;}
		}

		///<summary>
		///
		///</summary>
		public string ApplyTime
		{
			get {return applyTime;}
			set {applyTime = value;}
		}

		///<summary>
		///
		///</summary>
		public string DepartView
		{
			get {return departView;}
			set {departView = value;}
		}

		///<summary>
		///
		///</summary>
		public string ManagerView
		{
			get {return managerView;}
			set {managerView = value;}
		}
	
		public bool Loaded
        {
            get
            {
                return m_Loaded;
            }
            set
            {
                m_Loaded = value;
            }
        }
		
		#endregion
		
		#region 公有方法
		/// <summary>
		/// 删除
		/// </summary>
		/// <returns>是否成功</returns>
		public  override void Delete()
		{
			if(!m_Loaded)
				throw new AppException("尚未初始化");
			bool result=  (WorkToolSum.Delete(WorkToolSumId) == 1);
			//更新缓存
			if (result&&CachedEntityCommander.IsTypeRegistered(typeof(WorkToolSumInfo))) 
            {
                ResetCache();
            }
            if(!result)
			{
				throw new AppException("删除失败，数据可能被删除");
			}
		}
		
		
		/// <summary>
        /// 复制为另一个对象
        /// </summary>
        /// <param name="pIndustryTypeInfoTo"></param>
        public void CopyTo(WorkToolSumInfo pWorkToolSumInfoTo)
        {
            Copy(this,  pWorkToolSumInfoTo);
        }
		
		
		
		
		
		/// <summary>
		/// 保存
		/// </summary>
		public override void Save()
		{
			if(!m_Loaded)//新增
			{
				WorkToolSum workToolSum=new WorkToolSum();	
				SaveToDb(this, workToolSum,true);
			}
			else//修改
			{
				WorkToolSum workToolSum=new WorkToolSum(workToolSumId);	
				if(workToolSum.IsNew)
					throw new AppException("该数据已经不存在了");
				SaveToDb(this, workToolSum,false);
			}
			
		}
		 
		#endregion
		
	}
}
