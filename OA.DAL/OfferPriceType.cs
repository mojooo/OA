using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
namespace CanYou.OA.DAL
{
	/// <summary>
	/// Strongly-typed collection for the OfferPriceType class.
	/// </summary>
    [Serializable]
	public partial class OfferPriceTypeCollection : ActiveList<OfferPriceType, OfferPriceTypeCollection>
	{	   
		public OfferPriceTypeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>OfferPriceTypeCollection</returns>
		public OfferPriceTypeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                OfferPriceType o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the OfferPriceType_tb table.
	/// </summary>
	[Serializable]
	public partial class OfferPriceType : ActiveRecord<OfferPriceType>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public OfferPriceType()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public OfferPriceType(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public OfferPriceType(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public OfferPriceType(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("OfferPriceType_tb", TableType.Table, DataService.GetInstance("SubsonicProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarOfferPriceTypeId = new TableSchema.TableColumn(schema);
				colvarOfferPriceTypeId.ColumnName = "OfferPriceTypeId";
				colvarOfferPriceTypeId.DataType = DbType.Int32;
				colvarOfferPriceTypeId.MaxLength = 0;
				colvarOfferPriceTypeId.AutoIncrement = true;
				colvarOfferPriceTypeId.IsNullable = false;
				colvarOfferPriceTypeId.IsPrimaryKey = true;
				colvarOfferPriceTypeId.IsForeignKey = false;
				colvarOfferPriceTypeId.IsReadOnly = false;
				colvarOfferPriceTypeId.DefaultSetting = @"";
				colvarOfferPriceTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfferPriceTypeId);
				
				TableSchema.TableColumn colvarOfferPriceTypeName = new TableSchema.TableColumn(schema);
				colvarOfferPriceTypeName.ColumnName = "OfferPriceTypeName";
				colvarOfferPriceTypeName.DataType = DbType.String;
				colvarOfferPriceTypeName.MaxLength = 20;
				colvarOfferPriceTypeName.AutoIncrement = false;
				colvarOfferPriceTypeName.IsNullable = true;
				colvarOfferPriceTypeName.IsPrimaryKey = false;
				colvarOfferPriceTypeName.IsForeignKey = false;
				colvarOfferPriceTypeName.IsReadOnly = false;
				colvarOfferPriceTypeName.DefaultSetting = @"";
				colvarOfferPriceTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfferPriceTypeName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SubsonicProvider"].AddSchema("OfferPriceType_tb",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("OfferPriceTypeId")]
		[Bindable(true)]
		public int OfferPriceTypeId 
		{
			get { return GetColumnValue<int>(Columns.OfferPriceTypeId); }
			set { SetColumnValue(Columns.OfferPriceTypeId, value); }
		}
		  
		[XmlAttribute("OfferPriceTypeName")]
		[Bindable(true)]
		public string OfferPriceTypeName 
		{
			get { return GetColumnValue<string>(Columns.OfferPriceTypeName); }
			set { SetColumnValue(Columns.OfferPriceTypeName, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varOfferPriceTypeName)
		{
			OfferPriceType item = new OfferPriceType();
			
			item.OfferPriceTypeName = varOfferPriceTypeName;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varOfferPriceTypeId,string varOfferPriceTypeName)
		{
			OfferPriceType item = new OfferPriceType();
			
				item.OfferPriceTypeId = varOfferPriceTypeId;
			
				item.OfferPriceTypeName = varOfferPriceTypeName;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn OfferPriceTypeIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn OfferPriceTypeNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string OfferPriceTypeId = @"OfferPriceTypeId";
			 public static string OfferPriceTypeName = @"OfferPriceTypeName";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
