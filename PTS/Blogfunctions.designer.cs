﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PTS
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="pts")]
	public partial class BlogfunctionsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public BlogfunctionsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ptsConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BlogfunctionsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BlogfunctionsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BlogfunctionsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BlogfunctionsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_BlogCategory")]
		public int sp_BlogCategory([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Name", DbType="VarChar(50)")] string name)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), name);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_BlogKeyword")]
		public int sp_BlogKeyword([global::System.Data.Linq.Mapping.ParameterAttribute(Name="BlogName", DbType="VarChar(50)")] string blogName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Keyword", DbType="VarChar(50)")] string keyword)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), blogName, keyword);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CreateBlog")]
		public int CreateBlog([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="VarChar(100)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Title", DbType="VarChar(100)")] string title, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Snippet", DbType="VarChar(MAX)")] string snippet, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Post", DbType="VarChar(MAX)")] string post, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Category", DbType="VarChar(50)")] string category, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Published", DbType="Bit")] System.Nullable<bool> published)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName, title, snippet, post, category, published);
			return ((int)(result.ReturnValue));
		}
	}
}
#pragma warning restore 1591
