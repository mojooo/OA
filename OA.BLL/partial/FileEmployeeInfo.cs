﻿// ======================================================================================================================================
// 类说明
//=======================================================================================================================================
// 创建者：sk @Copy Right 残友软件有限公司OA项目 
// 文件名：FileEmployeeInfo.cs
// 创建时间：2012-3-29
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
	///FileEmployeeInfo  Data
	/// </summary>
	[Serializable]
	public partial class FileEmployeeInfo:CanYou.EntityObject
	{
		#region 属性
		///<summary>
		///
		///</summary>
		private int fileEmployeeId;
		///<summary>
		///
		///</summary>
		private int? fileId;
		///<summary>
		///
		///</summary>
		private int? employeeId;
		///<summary>
		///
		///</summary>
		private int? isMsg;
		///<summary>
		///
		///</summary>
		private int? isDelete;
		
		
		///一对多关系,如果
		private bool m_Loaded=false;
		#endregion
		
		#region 属性枚举
		public struct Fields
		{
			public const string FileEmployeeId=@"FileEmployeeId";
			public const string FileId=@"FileId";
			public const string EmployeeId=@"EmployeeId";
			public const string IsMsg=@"IsMsg";
			public const string IsDelete=@"IsDelete";
						
		}

		#endregion
		
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FileEmployeeInfo()
		{
		}
		///<summary>
		///
		///</summary>
		public FileEmployeeInfo(int pFileEmployeeId,int? pFileId,int? pEmployeeId,int? pIsMsg,int? pIsDelete)
		{
			fileEmployeeId = pFileEmployeeId;
			fileId         = pFileId;
			employeeId     = pEmployeeId;
			isMsg          = pIsMsg;
			isDelete       = pIsDelete;
			
		}
		
		
		///<summary>
		///
		///</summary>
		public FileEmployeeInfo(int fileEmployeeId)
		{
			 LoadFromId(fileEmployeeId);
		}
		
		private void LoadFromId(int fileEmployeeId)
		{
		 
			if (CachedEntityCommander.IsTypeRegistered(typeof(FileEmployeeInfo)))
            {
				FileEmployeeInfo fileEmployeeInfo=Find(GetList(), fileEmployeeId);
				if(fileEmployeeInfo==null)
					throw new AppException("未能在缓存中找到相应的键值对象");
                Copy(fileEmployeeInfo, this);
            }
            else
            {	FileEmployee fileEmployee=new FileEmployee( fileEmployeeId);	
				if(fileEmployee.IsNew)
				throw new AppException("尚未初始化");
               	LoadFromDAL(this, fileEmployee);
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
        public static  FileEmployeeInfo Find(List< FileEmployeeInfo> list,  int fileEmployeeId)
        {
             return list.Find(delegate( FileEmployeeInfo item) { return item. fileEmployeeId==fileEmployeeId; });
        }
		
		
		/// <summary>
        /// 得到缓存键
        /// </summary>
        /// <returns></returns>
        private static string GetCacheKey()
        {
            return string.Format(CachedEntityCommander.CacheKey, typeof( FileEmployeeInfo).ToString());
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
        public static void Copy(FileEmployeeInfo pFileEmployeeInfoFrom, FileEmployeeInfo pFileEmployeeInfoTo)
        {
	 		pFileEmployeeInfoTo.FileEmployeeId = pFileEmployeeInfoFrom.fileEmployeeId;
	 		pFileEmployeeInfoTo.FileId = pFileEmployeeInfoFrom.fileId;
	 		pFileEmployeeInfoTo.EmployeeId = pFileEmployeeInfoFrom.employeeId;
	 		pFileEmployeeInfoTo.IsMsg = pFileEmployeeInfoFrom.isMsg;
	 		pFileEmployeeInfoTo.IsDelete = pFileEmployeeInfoFrom.isDelete;
			pFileEmployeeInfoTo.Loaded=pFileEmployeeInfoFrom.Loaded;
        }
		
		/// <summary>
		/// 批量装载
		/// </summary>
		internal static void LoadFromDALPatch(List< FileEmployeeInfo> pList, FileEmployeeCollection pCollection)
        {
            foreach (FileEmployee fileEmployee in pCollection)
            {
                FileEmployeeInfo fileEmployeeInfo = new FileEmployeeInfo();
                LoadFromDAL(fileEmployeeInfo, fileEmployee );
                pList.Add(fileEmployeeInfo);
            }
			
        }

		//从后台获取数据
		internal  static void  LoadFromDAL(FileEmployeeInfo pFileEmployeeInfo, FileEmployee  pFileEmployee)
		{
	 		pFileEmployeeInfo.fileEmployeeId = pFileEmployee.FileEmployeeId;
	 		pFileEmployeeInfo.fileId = pFileEmployee.FileId;
	 		pFileEmployeeInfo.employeeId = pFileEmployee.EmployeeId;
	 		pFileEmployeeInfo.isMsg = pFileEmployee.IsMsg;
	 		pFileEmployeeInfo.isDelete = pFileEmployee.IsDelete;
			pFileEmployeeInfo.Loaded=true;
			
			}
		
		//数据持久化
		internal  static void  SaveToDb(FileEmployeeInfo pFileEmployeeInfo, FileEmployee  pFileEmployee,bool pIsNew)
		{
	 		pFileEmployee.FileEmployeeId = pFileEmployeeInfo.fileEmployeeId;
	 		pFileEmployee.FileId = pFileEmployeeInfo.fileId;
	 		pFileEmployee.EmployeeId = pFileEmployeeInfo.employeeId;
	 		pFileEmployee.IsMsg = pFileEmployeeInfo.isMsg;
	 		pFileEmployee.IsDelete = pFileEmployeeInfo.isDelete;
			pFileEmployee.IsNew=pIsNew;
			string UserName = SubsonicHelper.GetUserName();
			try
			{
				pFileEmployee.Save(UserName);
			}
			catch(Exception ex)
			{
				LogManager.getInstance().getLogger(typeof(FileEmployeeInfo)).Error(ex);
				if(ex.Message.Contains("插入重复键"))//违反了唯一键
				{
					throw new AppException("此对象已经存在");//此处等待优化可以从唯一约束中直接取出提示来，如果没有的话，默认为原始的出错提示
				}
				throw new AppException("保存失败");
			}
			pFileEmployeeInfo.fileEmployeeId = pFileEmployee.FileEmployeeId;
			//如果缓存存在，更新缓存
			if (CachedEntityCommander.IsTypeRegistered(typeof(FileEmployeeInfo))) 
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
        public static List< FileEmployeeInfo> GetPagedList(int pPageIndex,int pPageSize,SortDirection pOrderBy,string pSortExpression,out int pRecordCount)
        {
			if(pPageIndex<=1)
			pPageIndex=1;
            List< FileEmployeeInfo> list = new List< FileEmployeeInfo>();
          
            Query q = FileEmployee .CreateQuery();
            q.PageIndex = pPageIndex;
            q.PageSize = pPageSize;
            q.ORDER_BY(pSortExpression,pOrderBy.ToString());
			FileEmployeeCollection  collection=new  FileEmployeeCollection();
		 	collection.LoadAndCloseReader(q.ExecuteReader());
          
            foreach (FileEmployee  fileEmployee  in collection)
            {
                FileEmployeeInfo fileEmployeeInfo = new FileEmployeeInfo();
                LoadFromDAL(fileEmployeeInfo,   fileEmployee);
                list.Add(fileEmployeeInfo);
            }
			pRecordCount=q.GetRecordCount();
			
            return list;

        }
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		/// <returns></returns>
		public static List< FileEmployeeInfo> GetList()
		{
			string cacheKey = GetCacheKey();
			//本实体已经注册成缓存实体，并且缓存存在的时候，直接从缓存取
			if (CachedEntityCommander.IsTypeRegistered(typeof(FileEmployeeInfo)) && CachedEntityCommander.GetCache(cacheKey) != null)
            {
                return CachedEntityCommander.GetCache(cacheKey) as List< FileEmployeeInfo>;
            }
            else 
            {
				List< FileEmployeeInfo>  list =new List< FileEmployeeInfo>();
				FileEmployeeCollection  collection=new  FileEmployeeCollection();
				Query qry = new Query(FileEmployee.Schema);
				collection.LoadAndCloseReader(qry.ExecuteReader());
				foreach(FileEmployee fileEmployee in collection)
				{
					FileEmployeeInfo fileEmployeeInfo= new FileEmployeeInfo();
					LoadFromDAL(fileEmployeeInfo,fileEmployee);
					list.Add(fileEmployeeInfo);
				}
			  	//生成缓存
                if (CachedEntityCommander.IsTypeRegistered(typeof(FileEmployeeInfo))) 
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
		public int FileEmployeeId
		{
			get {return fileEmployeeId;}
			set {fileEmployeeId = value;}
		}

		///<summary>
		///
		///</summary>
		public int? FileId
		{
			get {return fileId;}
			set {fileId = value;}
		}

		///<summary>
		///
		///</summary>
		public int? EmployeeId
		{
			get {return employeeId;}
			set {employeeId = value;}
		}

		///<summary>
		///
		///</summary>
		public int? IsMsg
		{
			get {return isMsg;}
			set {isMsg = value;}
		}

		///<summary>
		///
		///</summary>
		public int? IsDelete
		{
			get {return isDelete;}
			set {isDelete = value;}
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
			bool result=  (FileEmployee.Delete(FileEmployeeId) == 1);
			//更新缓存
			if (result&&CachedEntityCommander.IsTypeRegistered(typeof(FileEmployeeInfo))) 
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
        public void CopyTo(FileEmployeeInfo pFileEmployeeInfoTo)
        {
            Copy(this,  pFileEmployeeInfoTo);
        }
		
		
		
		
		
		/// <summary>
		/// 保存
		/// </summary>
		public override void Save()
		{
			if(!m_Loaded)//新增
			{
				FileEmployee fileEmployee=new FileEmployee();	
				SaveToDb(this, fileEmployee,true);
			}
			else//修改
			{
				FileEmployee fileEmployee=new FileEmployee(fileEmployeeId);	
				if(fileEmployee.IsNew)
					throw new AppException("该数据已经不存在了");
				SaveToDb(this, fileEmployee,false);
			}
			
		}
		 
		#endregion
		
	}
}
