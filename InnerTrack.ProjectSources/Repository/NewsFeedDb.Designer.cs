﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace InnerTrack.ProjectSources.Repository
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class NewsFeedEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new NewsFeedEntities object using the connection string found in the 'NewsFeedEntities' section of the application configuration file.
        /// </summary>
        public NewsFeedEntities() : base("name=NewsFeedEntities", "NewsFeedEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NewsFeedEntities object.
        /// </summary>
        public NewsFeedEntities(string connectionString) : base(connectionString, "NewsFeedEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NewsFeedEntities object.
        /// </summary>
        public NewsFeedEntities(EntityConnection connection) : base(connection, "NewsFeedEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<NewsFeedEntry> NewsFeedEntries
        {
            get
            {
                if ((_NewsFeedEntries == null))
                {
                    _NewsFeedEntries = base.CreateObjectSet<NewsFeedEntry>("NewsFeedEntries");
                }
                return _NewsFeedEntries;
            }
        }
        private ObjectSet<NewsFeedEntry> _NewsFeedEntries;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the NewsFeedEntries EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToNewsFeedEntries(NewsFeedEntry newsFeedEntry)
        {
            base.AddObject("NewsFeedEntries", newsFeedEntry);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InnerTrackModel", Name="NewsFeedEntry")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class NewsFeedEntry : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new NewsFeedEntry object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="newsFeedId">Initial value of the NewsFeedId property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        /// <param name="createdBy">Initial value of the CreatedBy property.</param>
        /// <param name="createdOn">Initial value of the CreatedOn property.</param>
        public static NewsFeedEntry CreateNewsFeedEntry(global::System.Int32 id, global::System.Int32 newsFeedId, global::System.String title, global::System.String createdBy, global::System.DateTime createdOn)
        {
            NewsFeedEntry newsFeedEntry = new NewsFeedEntry();
            newsFeedEntry.Id = id;
            newsFeedEntry.NewsFeedId = newsFeedId;
            newsFeedEntry.Title = title;
            newsFeedEntry.CreatedBy = createdBy;
            newsFeedEntry.CreatedOn = createdOn;
            return newsFeedEntry;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 NewsFeedId
        {
            get
            {
                return _NewsFeedId;
            }
            set
            {
                OnNewsFeedIdChanging(value);
                ReportPropertyChanging("NewsFeedId");
                _NewsFeedId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("NewsFeedId");
                OnNewsFeedIdChanged();
            }
        }
        private global::System.Int32 _NewsFeedId;
        partial void OnNewsFeedIdChanging(global::System.Int32 value);
        partial void OnNewsFeedIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                OnMessageChanging(value);
                ReportPropertyChanging("Message");
                _Message = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Message");
                OnMessageChanged();
            }
        }
        private global::System.String _Message;
        partial void OnMessageChanging(global::System.String value);
        partial void OnMessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                OnCreatedByChanging(value);
                ReportPropertyChanging("CreatedBy");
                _CreatedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CreatedBy");
                OnCreatedByChanged();
            }
        }
        private global::System.String _CreatedBy;
        partial void OnCreatedByChanging(global::System.String value);
        partial void OnCreatedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                OnCreatedOnChanging(value);
                ReportPropertyChanging("CreatedOn");
                _CreatedOn = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CreatedOn");
                OnCreatedOnChanged();
            }
        }
        private global::System.DateTime _CreatedOn;
        partial void OnCreatedOnChanging(global::System.DateTime value);
        partial void OnCreatedOnChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                OnUpdatedByChanging(value);
                ReportPropertyChanging("UpdatedBy");
                _UpdatedBy = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("UpdatedBy");
                OnUpdatedByChanged();
            }
        }
        private global::System.String _UpdatedBy;
        partial void OnUpdatedByChanging(global::System.String value);
        partial void OnUpdatedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> UpdatedOn
        {
            get
            {
                return _UpdatedOn;
            }
            set
            {
                OnUpdatedOnChanging(value);
                ReportPropertyChanging("UpdatedOn");
                _UpdatedOn = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("UpdatedOn");
                OnUpdatedOnChanged();
            }
        }
        private Nullable<global::System.DateTime> _UpdatedOn;
        partial void OnUpdatedOnChanging(Nullable<global::System.DateTime> value);
        partial void OnUpdatedOnChanged();

        #endregion
    
    }

    #endregion
    
}
