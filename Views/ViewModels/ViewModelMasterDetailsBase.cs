using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Assets.Resources;
using NErgy.Silverlight.Views.Controllers;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class ViewModelMasterDetailsBase<T, Tmodel> : ViewModelDetailBase<T, Tmodel> where Tmodel : class,INotifyPropertyChanged, IEditableObject
    {
        public bool IsEnergyStudy
        {
            get { return Mediator.Instance.IsEnergyStudy; }
        }
        //helpers
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;
        public event EventHandler<NotificationEventArgs> SavingItemNotice;

        // Helper method to notify View of an error
        protected void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        protected void NotifySavedChanges()
        {
            Notify(ItemsSavedNotice, new NotificationEventArgs(StatusBarStrings.SavedCompletedSucesfully));
        }

        protected void NotifySavingChanges()
        {
            Notify(SavingItemNotice, new NotificationEventArgs(StatusBarStrings.SavingInProgress));
        }



        public static TT[] GetEnumValues<TT>()
        {
            var type = typeof(TT);
            if (!type.IsEnum)
                throw new ArgumentException("Type '" + type.Name + "' is not an enum");

            return (
              from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
              where field.IsLiteral
              select (TT)field.GetValue(null)
            ).ToArray();
        }


        public static string[] GetEnumStrings<TT>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("Type '" + type.Name + "' is not an enum");

            return (
              from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
              where field.IsLiteral
              select field.Name
            ).ToArray();
        }


        public static IList<EnumObject<TT>> GetEnumList<TT>()
        {
            var type = typeof(TT);
            if (!type.IsEnum)
                throw new ArgumentException("Type '" + type.Name + "' is not an enum");

            return (
                       from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                       where field.IsLiteral
                       select new EnumObject<TT>(GetLocalizedName(field.Name), (TT)field.GetValue(null), (int)field.GetValue(null))
                   ).ToList();
        }

        public static List<EnumObject<TT>> GetEnumListFromArray<TT>(List<string> greeceLocationsStrings)
        {
            return (from field in greeceLocationsStrings
                    select new EnumObject<TT>(field, greeceLocationsStrings.IndexOf(field))
                 ).ToList();
        }


        public static IList<EnumObject<TT>> GetEnumListWithArraySupport<TT>(List<string> supportStrings, int indexbase = 0)
        {
            var type = typeof(TT);
            if (!type.IsEnum)
                throw new ArgumentException("Type '" + type.Name + "' is not an enum");

            return (
                       from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                       where field.IsLiteral
                       select new EnumObject<TT>(GetStringArraySupport(field.Name, supportStrings, (int)field.GetValue(null), indexbase), (TT)field.GetValue(null), (int)field.GetValue(null))
                   ).ToList();
        }


        private static string GetStringArraySupport(string name, List<string> supportStrings, int index, int indexbase)
        {
            ResourceManager resourceManager = StandardsStrings.ResourceManager;
            var localizedstring = resourceManager.GetString(name);
            if (localizedstring == null)
            {

                if (supportStrings.Count >= index)
                    localizedstring = supportStrings[index - indexbase];}
            if (localizedstring == null)
            {
                localizedstring = name;
            }

            return localizedstring;
        }

        private static ResourceWrapper resources = new ResourceWrapper();

        private static string GetLocalizedName(string name)
        {
            ResourceManager resourceManager = StandardsStrings.ResourceManager;//   ApplicationStrings.ResourceManager; //new ResourceManager("ApplicationStrings", typeof(ApplicationStrings).GetType().Assembly);
            var localizedstring = (string)resourceManager.GetString(name);
            if (localizedstring == null) localizedstring = name;
            return localizedstring;
        }


        public virtual void BeginEdit()
        {
            base.BeginEdit();
        }

        public virtual void CancelEdit()
        {
            base.CancelEdit();
        }

        public virtual void EndEdit()
        {
            base.EndEdit();
        }


    }
}
