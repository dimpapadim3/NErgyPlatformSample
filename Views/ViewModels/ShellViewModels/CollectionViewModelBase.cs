using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using NErgy.Building;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using SimpleMvvmToolkit;


namespace NErgy.Silverlight.Views.ViewModels
{
    public class CollectionViewModelBase<TModel> : ViewModelMasterDetailsBase<CollectionViewModelBase<TModel>, TModel> where TModel : class, INotifyPropertyChanged, IEditableObject, new()
    {
        public CollectionViewModelBase(TModel model)
        {
            base.Model = model;
            if(DataAccesLayerService==null) DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }
        private BlankRowCollection<TModel> _entityObjectsblankRowCollection;
        private bool _isEnabledEntityObjectEndEdit;

        private object OriginalOpaqueElements { get; set; }

        public IList<TModel> EntityObjects
        {
            get
            {
                Logger.Instance.Log("EntityObjects Type" + typeof(TModel));
                if (_entityObjectsblankRowCollection == null || _entityObjectsblankRowCollection.Count == 0)
                {
                    InitializeOpaqueElementCollection();
                }
                return _entityObjectsblankRowCollection;
            }

        }
        private TModel _selectedEntityObject;
        public TModel SelectedEntityObject
        {
            get { return _selectedEntityObject; }
            set
            {
                Logger.Instance.Log("SelectedEntityObject Type" + typeof(TModel) + " : " + SelectedEntityObject);
                _selectedEntityObject = value;
                NotifyPropertyChanged(m => m.SelectedEntityObject);
            }
        }
        public ICommand EndEditTranslucentCommand
        {
            get
            {

                return new DelegateCommand(SaveEntityObjectsChanges);
            }
        }

        public ICommand CancelEditEntityObjectCommand
        {
            get
            {

                return new DelegateCommand(RejectChangesEntityObjectCollection);
            }
        }

        public bool IsEnabledEntityObjectEndEdit
        {
            get { return _isEnabledEntityObjectEndEdit; }
            set
            {
                _isEnabledEntityObjectEndEdit = value;
                NotifyPropertyChanged(m => m.IsEnabledEntityObjectEndEdit);
            }
        }

        public IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }



        public ICommand DeleteEntity
        {
            get
            {
                return new DelegateCommand(RemoveEntityObjectFromCollection);
            }
        }


        public virtual List<TModel> GetCollection()
        {
            return new List<TModel>();
        }


        public virtual void SetCollection(IList<TModel> collection)
        {

        }

        private void NotifyEntityObjectBlankRowCollectionCollectionChanged()
        {
            NotifyPropertyChanged(m => m.EntityObjects);
            IsEnabledEntityObjectEndEdit = true;
            OriginalOpaqueElements = SimpleMvvmToolkit.ModelExtensions.Extensions.Clone(GetCollection(), new List<Type>() { typeof(TModel) });
        }

        private void InitializeOpaqueElementCollection()
        {
            var coll = new List<TModel>();
            if (GetCollection() != null)
            {
                (GetCollection().ToList())
                    .ForEach(c =>
                                 {
                                     if (c is TModel) coll.Add(c as TModel);
                                     c.PropertyChanged += (s, e) => NotifyEntityObjectBlankRowCollectionCollectionChanged();
                                 });

            }
            _entityObjectsblankRowCollection = new BlankRowCollection<TModel>(coll);
            _entityObjectsblankRowCollection.CollectionChanged += (s, e) => NotifyEntityObjectBlankRowCollectionCollectionChanged();

            _entityObjectsblankRowCollection.OnEntityCreation += (s, e) =>
            {
               // if (!AddedEntityObjects.Contains(s as TModel))
                //    AddedEntityObjects.Add(s as TModel);
            };
            _entityObjectsblankRowCollection.CollectionChanged += (s, e) =>
            {
                //afou exw prosthesei ena neo antikimeno eksakolouthw na koitazw an  yparxoun alages gia na kanw enable to saving .sound legit 
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    e.NewItems.Cast<TModel>().ToList().ForEach(addeitem =>
                                                                   {
                                                                       addeitem.PropertyChanged += (obj, arg) =>
                                                                                                       {
                                                                                                           NotifyEntityObjectBlankRowCollectionCollectionChanged();
                                                                                                       };

                                                                   });
                    //e.NewItems.Cast<TModel>().ToList().ForEach(RemovedEntityObjects.Add);
                }
            };
        
        
        }

        private void RejectChangesEntityObjectCollection()
        {
            Logger.Instance.Log("RejectChangesEntityObjectCollection Type" + typeof(TModel) );
            SetCollection(OriginalOpaqueElements as IList<TModel>);
            InitializeOpaqueElementCollection();
            NotifyPropertyChanged(m => m.EntityObjects);
        }

        public void SaveEntityObjectsChanges()
        {
            SetCollection(_entityObjectsblankRowCollection.RealItems.ToList());
            Mediator.Instance.SaveBuildingData();
            DataAccesLayerService.SaveChanges(e =>
                                                  {
                                                      if (e != null)
                                                      {
                                                      }
                                                      Logger.Instance.Log("SaveEntityObjectsChanges Type" + typeof(TModel));
                                                  });
            IsEnabledEntityObjectEndEdit = false;
        }

        private void RemoveEntityObjectFromCollection()
        {
            if (_entityObjectsblankRowCollection.RealItems.Contains(SelectedEntityObject))
            {
                _entityObjectsblankRowCollection.Remove(SelectedEntityObject);
            }
        }

        public void TranslusentKeyDown(Key key)
        {
            if (key == Key.Delete)
            {
                RemoveEntityObjectFromCollection();
            }
        }


    }

    //public abstract class DataBaseEntityCollectionViewModelBase<TModel> : ViewModelMasterDetailsBase<CollectionViewModelBase<TModel>, TModel> where TModel : class, INotifyPropertyChanged, IEditableObject, new()
    //{
   
    //    public DataBaseEntityCollectionViewModelBase()
    //    {

    //    }
    //    private BlankRowCollection<TModel> _entityObjectsblankRowCollection;
    //    private bool _isEnabledEntityObjectEndEdit;

    //    private object OriginalOpaqueElements { get; set; }

    //    public IList<TModel> EntityObjects
    //    {
    //        get
    //        {
    //            if (_entityObjectsblankRowCollection == null || _entityObjectsblankRowCollection.Count == 0)
    //            {
    //                InitializeOpaqueElementCollection();
    //            }
    //            return _entityObjectsblankRowCollection;
    //        }

    //    }
    //    private TModel _selectedEntityObject;
    //    public TModel SelectedEntityObject
    //    {
    //        get { return _selectedEntityObject; }
    //        set
    //        {
    //            _selectedEntityObject = value;
    //            NotifyPropertyChanged(m => m.SelectedEntityObject);
    //        }
    //    }
    //    public ICommand EndEditTranslucentCommand
    //    {
    //        get
    //        {
    //            return new DelegateCommand(SaveEntityObjectsChanges);
    //        }
    //    }

    //    public ICommand CancelEditEntityObjectCommand
    //    {
    //        get
    //        {

    //            return new DelegateCommand(RejectChangesEntityObjectCollection);
    //        }
    //    }

    //    public bool IsEnabledEntityObjectEndEdit
    //    {
    //        get { return _isEnabledEntityObjectEndEdit; }
    //        set
    //        {
    //            _isEnabledEntityObjectEndEdit = value;
    //            NotifyPropertyChanged(m => m.IsEnabledEntityObjectEndEdit);
    //        }
    //    }

    //    public IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }



    //    public ICommand DeleteTModel
    //    {
    //        get
    //        {
    //            return new DelegateCommand(RemoveEntityObjectFromCollection);
    //        }
    //    }


    //    public virtual void GetCollectionAsync(Action<List<TModel>> completed)
    //    {
    //        completed(new List<TModel>());
    //    }


    //    public virtual void SetCollection(IList<TModel> collection)
    //    {

    //    }

    //    private void NotifyEntityObjectBlankRowCollectionCollectionChanged()
    //    {
    //        NotifyPropertyChanged(m => m.EntityObjects);
    //        IsEnabledEntityObjectEndEdit = true;
    //        //OriginalOpaqueElements = SimpleMvvmToolkit.ModelExtensions.Extensions.Clone(GetCollection(), new List<Type>() { typeof(TModel) });
    //    }

    //    private void InitializeOpaqueElementCollection()
    //    {
    //        var coll = new List<TModel>();
    //        GetCollectionAsync((system) =>
    //                               {
    //                                   if (system != null)
    //                                   {
    //                                       (system.ToList())
    //                                           .ForEach(c =>
    //                                                        {
    //                                                            if (c is TModel) coll.Add(c);
    //                                                            c.PropertyChanged +=(s, e) =>NotifyEntityObjectBlankRowCollectionCollectionChanged();
    //                                                            c.PropertyChanged += (s, e) =>
    //                                                                                     {
    //                                                                                        // IsEnabledEntityObjectEndEdit= true;
    //                                                                                     };
    //                                                        });

    //                                   }
    //                                   _entityObjectsblankRowCollection = new BlankRowCollection<TModel>(coll);
    //                                   _entityObjectsblankRowCollection.CollectionChanged +=(s, e) => NotifyEntityObjectBlankRowCollectionCollectionChanged();
    //                                   _entityObjectsblankRowCollection.OnEntityCreation += (s, e) =>
    //                                                                                      {
    //                                                                                          if (!AddedEntityObjects.Contains(s as TModel))
    //                                                                                              AddedEntityObjects.Add(s as TModel);
    //                                                                                      };
    //                                   _entityObjectsblankRowCollection.CollectionChanged += (s, e) =>
    //                                   {
    //                                       if (e.Action == NotifyCollectionChangedAction.Remove)
    //                                       {
    //                                           e.NewItems.Cast<TModel>().ToList().ForEach(RemovedEntityObjects.Add);
    //                                       }
    //                                   };

    //                               });
    //    }

    //    public IList<TModel> AddedEntityObjects = new List<TModel>();
    //    public IList<TModel> RemovedEntityObjects = new List<TModel>();

    //    private void RejectChangesEntityObjectCollection()
    //    {
    //        SetCollection(OriginalOpaqueElements as IList<TModel>);
    //        InitializeOpaqueElementCollection();
    //        NotifyPropertyChanged(m => m.EntityObjects);
    //    }

    //    public abstract void AddToTableEntity(TModel entity);
    //    public abstract void RemoveFromTableEntity(TModel entity);

    //    public virtual void SaveEntityObjectsChanges()
    //    {

    //        AddedEntityObjects.ToList().ForEach(AddToTableEntity);
    //        RemovedEntityObjects.ToList().ForEach(RemoveFromTableEntity);

    //        DoDataAccessSaveChanges();

    //        IsEnabledEntityObjectEndEdit = false;
    //    }

    //    protected virtual void DoDataAccessSaveChanges()
    //    {
    //        DataAccesLayerService.SaveChanges(e =>
    //                                              {
    //                                                  if (e != null)
    //                                                  {
    //                                                  }
    //                                              });
    //    }

    //    private void RemoveEntityObjectFromCollection()
    //    {
    //        if (_entityObjectsblankRowCollection.Contains(SelectedEntityObject))
    //        {
    //            _entityObjectsblankRowCollection.Remove(SelectedEntityObject);
    //        }
    //    }

    //    public void TranslusentKeyDown(Key key)
    //    {
    //        if (key == Key.Delete)
    //        {
    //            RemoveEntityObjectFromCollection();
    //        }
    //    }


    //}


}