﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Taxi1
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Taxi")]
	public partial class DataClasses3DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertZakazchik(Zakazchik instance);
    partial void UpdateZakazchik(Zakazchik instance);
    partial void DeleteZakazchik(Zakazchik instance);
    #endregion
		
		public DataClasses3DataContext() : 
				base(global::Taxi1.Properties.Settings.Default.TaxiConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses3DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses3DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses3DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses3DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Zakazchik> Zakazchik
		{
			get
			{
				return this.GetTable<Zakazchik>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Zakazchik")]
	public partial class Zakazchik : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID_klient;
		
		private string _number;
		
		private string _mesto_otpravleniya;
		
		private string _mesto_pribytiya;
		
		private string _stoimost_zakaza;
		
		private string _Spasb_oplat;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_klientChanging(int value);
    partial void OnID_klientChanged();
    partial void OnnumberChanging(string value);
    partial void OnnumberChanged();
    partial void Onmesto_otpravleniyaChanging(string value);
    partial void Onmesto_otpravleniyaChanged();
    partial void Onmesto_pribytiyaChanging(string value);
    partial void Onmesto_pribytiyaChanged();
    partial void Onstoimost_zakazaChanging(string value);
    partial void Onstoimost_zakazaChanged();
    partial void OnSpasb_oplatChanging(string value);
    partial void OnSpasb_oplatChanged();
    #endregion
		
		public Zakazchik()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_klient", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID_klient
		{
			get
			{
				return this._ID_klient;
			}
			set
			{
				if ((this._ID_klient != value))
				{
					this.OnID_klientChanging(value);
					this.SendPropertyChanging();
					this._ID_klient = value;
					this.SendPropertyChanged("ID_klient");
					this.OnID_klientChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_number", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string number
		{
			get
			{
				return this._number;
			}
			set
			{
				if ((this._number != value))
				{
					this.OnnumberChanging(value);
					this.SendPropertyChanging();
					this._number = value;
					this.SendPropertyChanged("number");
					this.OnnumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[mesto otpravleniya]", Storage="_mesto_otpravleniya", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string mesto_otpravleniya
		{
			get
			{
				return this._mesto_otpravleniya;
			}
			set
			{
				if ((this._mesto_otpravleniya != value))
				{
					this.Onmesto_otpravleniyaChanging(value);
					this.SendPropertyChanging();
					this._mesto_otpravleniya = value;
					this.SendPropertyChanged("mesto_otpravleniya");
					this.Onmesto_otpravleniyaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[mesto pribytiya]", Storage="_mesto_pribytiya", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string mesto_pribytiya
		{
			get
			{
				return this._mesto_pribytiya;
			}
			set
			{
				if ((this._mesto_pribytiya != value))
				{
					this.Onmesto_pribytiyaChanging(value);
					this.SendPropertyChanging();
					this._mesto_pribytiya = value;
					this.SendPropertyChanged("mesto_pribytiya");
					this.Onmesto_pribytiyaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[stoimost zakaza]", Storage="_stoimost_zakaza", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string stoimost_zakaza
		{
			get
			{
				return this._stoimost_zakaza;
			}
			set
			{
				if ((this._stoimost_zakaza != value))
				{
					this.Onstoimost_zakazaChanging(value);
					this.SendPropertyChanging();
					this._stoimost_zakaza = value;
					this.SendPropertyChanged("stoimost_zakaza");
					this.Onstoimost_zakazaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Spasb oplat]", Storage="_Spasb_oplat", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Spasb_oplat
		{
			get
			{
				return this._Spasb_oplat;
			}
			set
			{
				if ((this._Spasb_oplat != value))
				{
					this.OnSpasb_oplatChanging(value);
					this.SendPropertyChanging();
					this._Spasb_oplat = value;
					this.SendPropertyChanged("Spasb_oplat");
					this.OnSpasb_oplatChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
