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
namespace CanYou.OA.DAL{
    /// <summary>
    /// Strongly-typed collection for the Vw_EmployeeIdOfFinaceManager class.
    /// </summary>
    [Serializable]
    public partial class Vw_EmployeeIdOfFinaceManagerCollection : ReadOnlyList<Vw_EmployeeIdOfFinaceManager, Vw_EmployeeIdOfFinaceManagerCollection>
    {        
        public Vw_EmployeeIdOfFinaceManagerCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the vw_EmployeeIdOfFinaceManager view.
    /// </summary>
    [Serializable]
    public partial class Vw_EmployeeIdOfFinaceManager : ReadOnlyRecord<Vw_EmployeeIdOfFinaceManager>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("vw_EmployeeIdOfFinaceManager", TableType.View, DataService.GetInstance("SubsonicProvider"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarEmployeeId = new TableSchema.TableColumn(schema);
                colvarEmployeeId.ColumnName = "EmployeeId";
                colvarEmployeeId.DataType = DbType.Int32;
                colvarEmployeeId.MaxLength = 0;
                colvarEmployeeId.AutoIncrement = false;
                colvarEmployeeId.IsNullable = false;
                colvarEmployeeId.IsPrimaryKey = false;
                colvarEmployeeId.IsForeignKey = false;
                colvarEmployeeId.IsReadOnly = false;
                
                schema.Columns.Add(colvarEmployeeId);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["SubsonicProvider"].AddSchema("vw_EmployeeIdOfFinaceManager",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public Vw_EmployeeIdOfFinaceManager()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public Vw_EmployeeIdOfFinaceManager(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public Vw_EmployeeIdOfFinaceManager(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public Vw_EmployeeIdOfFinaceManager(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("EmployeeId")]
        [Bindable(true)]
        public int EmployeeId 
	    {
		    get
		    {
			    return GetColumnValue<int>("EmployeeId");
		    }
            set 
		    {
			    SetColumnValue("EmployeeId", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string EmployeeId = @"EmployeeId";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
