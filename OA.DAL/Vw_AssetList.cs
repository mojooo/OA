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
    /// Strongly-typed collection for the Vw_AssetList class.
    /// </summary>
    [Serializable]
    public partial class Vw_AssetListCollection : ReadOnlyList<Vw_AssetList, Vw_AssetListCollection>
    {        
        public Vw_AssetListCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the vw_AssetList view.
    /// </summary>
    [Serializable]
    public partial class Vw_AssetList : ReadOnlyRecord<Vw_AssetList>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("vw_AssetList", TableType.View, DataService.GetInstance("SubsonicProvider"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarAssetId = new TableSchema.TableColumn(schema);
                colvarAssetId.ColumnName = "AssetId";
                colvarAssetId.DataType = DbType.Int32;
                colvarAssetId.MaxLength = 0;
                colvarAssetId.AutoIncrement = false;
                colvarAssetId.IsNullable = false;
                colvarAssetId.IsPrimaryKey = false;
                colvarAssetId.IsForeignKey = false;
                colvarAssetId.IsReadOnly = false;
                
                schema.Columns.Add(colvarAssetId);
                
                TableSchema.TableColumn colvarAssetName = new TableSchema.TableColumn(schema);
                colvarAssetName.ColumnName = "AssetName";
                colvarAssetName.DataType = DbType.String;
                colvarAssetName.MaxLength = 50;
                colvarAssetName.AutoIncrement = false;
                colvarAssetName.IsNullable = true;
                colvarAssetName.IsPrimaryKey = false;
                colvarAssetName.IsForeignKey = false;
                colvarAssetName.IsReadOnly = false;
                
                schema.Columns.Add(colvarAssetName);
                
                TableSchema.TableColumn colvarMemo = new TableSchema.TableColumn(schema);
                colvarMemo.ColumnName = "Memo";
                colvarMemo.DataType = DbType.String;
                colvarMemo.MaxLength = -1;
                colvarMemo.AutoIncrement = false;
                colvarMemo.IsNullable = true;
                colvarMemo.IsPrimaryKey = false;
                colvarMemo.IsForeignKey = false;
                colvarMemo.IsReadOnly = false;
                
                schema.Columns.Add(colvarMemo);
                
                TableSchema.TableColumn colvarPrice = new TableSchema.TableColumn(schema);
                colvarPrice.ColumnName = "Price";
                colvarPrice.DataType = DbType.String;
                colvarPrice.MaxLength = 50;
                colvarPrice.AutoIncrement = false;
                colvarPrice.IsNullable = true;
                colvarPrice.IsPrimaryKey = false;
                colvarPrice.IsForeignKey = false;
                colvarPrice.IsReadOnly = false;
                
                schema.Columns.Add(colvarPrice);
                
                TableSchema.TableColumn colvarType = new TableSchema.TableColumn(schema);
                colvarType.ColumnName = "Type";
                colvarType.DataType = DbType.String;
                colvarType.MaxLength = 50;
                colvarType.AutoIncrement = false;
                colvarType.IsNullable = true;
                colvarType.IsPrimaryKey = false;
                colvarType.IsForeignKey = false;
                colvarType.IsReadOnly = false;
                
                schema.Columns.Add(colvarType);
                
                TableSchema.TableColumn colvarAmount = new TableSchema.TableColumn(schema);
                colvarAmount.ColumnName = "Amount";
                colvarAmount.DataType = DbType.String;
                colvarAmount.MaxLength = 50;
                colvarAmount.AutoIncrement = false;
                colvarAmount.IsNullable = true;
                colvarAmount.IsPrimaryKey = false;
                colvarAmount.IsForeignKey = false;
                colvarAmount.IsReadOnly = false;
                
                schema.Columns.Add(colvarAmount);
                
                TableSchema.TableColumn colvarDepartName = new TableSchema.TableColumn(schema);
                colvarDepartName.ColumnName = "DepartName";
                colvarDepartName.DataType = DbType.String;
                colvarDepartName.MaxLength = 50;
                colvarDepartName.AutoIncrement = false;
                colvarDepartName.IsNullable = true;
                colvarDepartName.IsPrimaryKey = false;
                colvarDepartName.IsForeignKey = false;
                colvarDepartName.IsReadOnly = false;
                
                schema.Columns.Add(colvarDepartName);
                
                TableSchema.TableColumn colvarSiteName = new TableSchema.TableColumn(schema);
                colvarSiteName.ColumnName = "SiteName";
                colvarSiteName.DataType = DbType.String;
                colvarSiteName.MaxLength = 50;
                colvarSiteName.AutoIncrement = false;
                colvarSiteName.IsNullable = true;
                colvarSiteName.IsPrimaryKey = false;
                colvarSiteName.IsForeignKey = false;
                colvarSiteName.IsReadOnly = false;
                
                schema.Columns.Add(colvarSiteName);
                
                TableSchema.TableColumn colvarUnitName = new TableSchema.TableColumn(schema);
                colvarUnitName.ColumnName = "UnitName";
                colvarUnitName.DataType = DbType.String;
                colvarUnitName.MaxLength = 50;
                colvarUnitName.AutoIncrement = false;
                colvarUnitName.IsNullable = true;
                colvarUnitName.IsPrimaryKey = false;
                colvarUnitName.IsForeignKey = false;
                colvarUnitName.IsReadOnly = false;
                
                schema.Columns.Add(colvarUnitName);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["SubsonicProvider"].AddSchema("vw_AssetList",schema);
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
	    public Vw_AssetList()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public Vw_AssetList(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public Vw_AssetList(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public Vw_AssetList(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("AssetId")]
        [Bindable(true)]
        public int AssetId 
	    {
		    get
		    {
			    return GetColumnValue<int>("AssetId");
		    }
            set 
		    {
			    SetColumnValue("AssetId", value);
            }
        }
	      
        [XmlAttribute("AssetName")]
        [Bindable(true)]
        public string AssetName 
	    {
		    get
		    {
			    return GetColumnValue<string>("AssetName");
		    }
            set 
		    {
			    SetColumnValue("AssetName", value);
            }
        }
	      
        [XmlAttribute("Memo")]
        [Bindable(true)]
        public string Memo 
	    {
		    get
		    {
			    return GetColumnValue<string>("Memo");
		    }
            set 
		    {
			    SetColumnValue("Memo", value);
            }
        }
	      
        [XmlAttribute("Price")]
        [Bindable(true)]
        public string Price 
	    {
		    get
		    {
			    return GetColumnValue<string>("Price");
		    }
            set 
		    {
			    SetColumnValue("Price", value);
            }
        }
	      
        [XmlAttribute("Type")]
        [Bindable(true)]
        public string Type 
	    {
		    get
		    {
			    return GetColumnValue<string>("Type");
		    }
            set 
		    {
			    SetColumnValue("Type", value);
            }
        }
	      
        [XmlAttribute("Amount")]
        [Bindable(true)]
        public string Amount 
	    {
		    get
		    {
			    return GetColumnValue<string>("Amount");
		    }
            set 
		    {
			    SetColumnValue("Amount", value);
            }
        }
	      
        [XmlAttribute("DepartName")]
        [Bindable(true)]
        public string DepartName 
	    {
		    get
		    {
			    return GetColumnValue<string>("DepartName");
		    }
            set 
		    {
			    SetColumnValue("DepartName", value);
            }
        }
	      
        [XmlAttribute("SiteName")]
        [Bindable(true)]
        public string SiteName 
	    {
		    get
		    {
			    return GetColumnValue<string>("SiteName");
		    }
            set 
		    {
			    SetColumnValue("SiteName", value);
            }
        }
	      
        [XmlAttribute("UnitName")]
        [Bindable(true)]
        public string UnitName 
	    {
		    get
		    {
			    return GetColumnValue<string>("UnitName");
		    }
            set 
		    {
			    SetColumnValue("UnitName", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string AssetId = @"AssetId";
            
            public static string AssetName = @"AssetName";
            
            public static string Memo = @"Memo";
            
            public static string Price = @"Price";
            
            public static string Type = @"Type";
            
            public static string Amount = @"Amount";
            
            public static string DepartName = @"DepartName";
            
            public static string SiteName = @"SiteName";
            
            public static string UnitName = @"UnitName";
            
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
