using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.BuildingViews
{
    public class UsageViewModel : ViewModelMasterDetailsBase<UsageViewModel, ProjectViewModel>
    {

        public UsageViewModel()
        {
            //this.Model = project;
            IsTreeViewVisibility = false;
            IsbuttonVisibility = true;
            UsageTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(UsageStrings));

            UsageTitleStrings = new List<string>()
                                                 {
                                                    "Κατοικίας",
                                                     "Προσωρινής διαμονής",
                                                     "Συνάθροισης κοινού",
                                                     "Εκπαίδευσης",
                                                     "Υγείας και Κοινωνικής Πρόνοιας",
                                                     "Σωφρονισμού"
                                                     ,"Εμπορίου",
                                                     "Γραφείων"

                                                 };

        }

        public List<string> UsageTitleStrings { get; set; }




        private List<string> UsageStrings = new List<string>()
                                                 {
                                                     "Μονοκατοικία"
                                    , "Πολυκατοικία"
                                     ,"Ξενοδοχείο - Ετήσιας λειτουργίας"
                                     , "Ξενοδοχείο - Θερινής λειτουργίας"
                                     , "Ξενοδοχείο - Χειμερινής λειτουργίας"
                                     , "Ξενώνες - Ετήσιας λειτουργίας"
                                     , "Ξενώνες - Θερινής λειτουργίας"
                                     , "Ξενώνες - Χειμερινής λειτουργίας"
                                    ,	 "Οικοτροφεία"
                                    ,	 "Κοιτώνες"
                                    ,	 "Εστιατόρια"
                                    ,	 "Ζαχαροπλαστεία"
                                    ,	 "Καφενεία"
                                    ,	 "Νυχτερινά κέντρα διασκέδασης"
                                    ,	 "Μουσικές σκηνές"
                                    ,	 "Θέατρα"
                                    ,	 "Κινηματογράφοι"
                                    ,	 "Χώροι συναυλιών"
                                    ,	 "Χώροι εκθέσεων"
                                    ,	 "Μουσεία"
                                    ,	 "Χώροι συνεδρίων"
                                    ,	 "Αμφιθέατρα"
                                    ,	 "Αίθουσες δικαστηρίων"
                                    ,	 "Τράπεζες"
                                    ,	 "Αίθουσες πολλαπλών χρήσεων"
                                    ,	 "Κλειστό γυμναστήριο"
                                    ,	 "Κλειστό κολυμβητήριο"
                                    ,	 "Νηπιαγωγεία"
                                    ,	 "Πρωτοβάθμιας εκπαίδευσης"
                                    ,	 "Δευτεροβάθμιας εκπαίδευσης"
                                    ,	 "Τριτοβάθμιας εκπαίδευσης"
                                    ,	 "Αίθουσες διδασκαλίας"
                                    ,	 "Φροντιστήρια"
                                    ,	 "Ωδεία"
                                    ,	 "Νοσοκομεία"
                                    ,	 "Κλινικές"
                                    ,	 "Αγροτικά ιατρεία"
                                    ,	 "Υγειονομικοί σταθμοί"
                                    ,	 "Κέντρα υγείας"
                                    ,	 "Ιατρεία"
                                    ,	 "Ψυχιατρεία"
                                    ,	 "Ιδρύματα"
                                    ,	 "Οίκοι ευγηρίας"
                                    ,	 "Βρεφοκομεία"
                                    ,	 "Βρεφικοί σταθμοί"
                                    ,	 "Παιδικοί σταθμοί"
                                    ,	 "Κρατητήρια"
                                    ,	 "Αναμορφωτήρια"
                                    ,	 "Φυλακές"
                                    ,	 "Αστυνομικές διευθύνσεις"
                                    ,	 "Εμπορικά κέντρα"
                                    ,	 "Αγορές"
                                    ,	 "Υπεραγορές"
                                    ,	 "Καταστήματα"
                                    ,	 "Φαρμακεία"
                                    ,	 "Ινστιτούτα γυμναστικής"
                                    ,	 "Κουρεία"
                                    ,	 "Κομμωτήρια"
                                    ,	 "Γραφεία"
                                    ,	 "Βιβλιοθήκες"



                                                 };

        public List<EnumObject<object>> UsageTypes { get; set; }

        private EnumObject<object> _selectedUsageType;
        public EnumObject<object> SelectedUsageType
        {
            get { return _selectedUsageType; }
            set
            {
                _selectedUsageType = value;

                NotifyPropertyChanged(vm => vm.SelectedUsageType);
            }
        }

        private bool _isbuttonVisibility;
        public bool IsbuttonVisibility
        {
            get { return _isbuttonVisibility; }
            set
            {
                _isbuttonVisibility = value;
                NotifyPropertyChanged(vm => vm.IsbuttonVisibility);
            }
        }

        private bool _isTreeViewVisibility;
        public bool IsTreeViewVisibility
        {
            get { return _isTreeViewVisibility; }
            set
            {
                _isTreeViewVisibility = value;
                NotifyPropertyChanged(vm => vm.IsTreeViewVisibility);
            }
        }


        public void SetNewUsageById(int newValue)
        {
            this.SelectedUsageType = UsageTypes.FirstOrDefault(u => u.Id == newValue);
            // if (SelectedUsageType != null)
            //     this.Model.ProjectUseageId = this.SelectedUsageType.Id;
        }
    }

    public class ZoneUsageViewModel : ViewModelMasterDetailsBase<UsageViewModel, ThermalZoneViewModel>
    {

        public ZoneUsageViewModel()
        {
            //this.Model = project;
            IsTreeViewVisibility = false;
            IsbuttonVisibility = true;
            UsageTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(CorrectedUsageStrings));

            UsageTitleStrings = new List<string>()
                                                 {
                                                    "Κατοικίας",
                                                     "Προσωρινής διαμονής",
                                                     "Συνάθροισης κοινού",
                                                     "Εκπαίδευσης",
                                                     "Υγείας και Κοινωνικής Πρόνοιας",
                                                     "Σωφρονισμού"
                                                     ,"Εμπορίου",
                                                     "Γραφείων"

                                                 };

        }

        public List<string> UsageTitleStrings { get; set; }

        private List<string> CorrectedUsageStrings = new List<string>()
                                                {
        "Μονοκατοικία, πολυκατοικία"
, "Ξενοδοχείο ετήσιας λειτουργίας"
, "Ξενοδοχείο ετήσιας λειτουργίας - Υπνοδωμάτια"
, "Ξενοδοχείο ετήσιας λειτουργίας - Κοινόχρηστοι χώροι"
, "Ξενοδοχείο θερινής λειτουργίας"
, "Ξενοδοχείο θερινής λειτουργίας - Υπνοδωμάτια"
, "Ξενοδοχείο θερινής λειτουργίας - Κοινόχρηστοι χώροι"
, "Ξενοδοχείο χειμερινής λειτουργίας"
, "Ξενοδοχείο χειμερινής λειτουργίας - Υπνοδωμάτια"
, "Ξενοδοχείο χειμερινής λειτουργίας - Κοινόχρηστοι χώροι"
, "Ξενώνες ετήσιας λειτουργίας"
, "Ξενώνες ετήσιας λειτουργίας - Υπνοδωμάτια"
, "Ξενώνες ετήσιας λειτουργίας - Κοινόχρηστοι χώροι"
, "Ξενώνες θερινής λειτουργίας"
, "Ξενώνες θερινής λειτουργίας - Υπνοδωμάτια"
, "Ξενώνες θερινής λειτουργίας - Κοινόχρηστοι χώροι"
, "Ξενώνες χειμερινής λειτουργίας"
, "Ξενώνες χειμερινής λειτουργίας - Υπνοδωμάτια"
, "Ξενώνες χειμερινής λειτουργίας - Κοινόχρηστοι χώροι"
, "Οικοτροφεία και Κοιτώνες"
, "Υπνοδωμάτια"
, "Κοινόχρηστοι χώροι"
, "Εστιατόρια"
, "Ζαχαροπλαστεία, Καφενεία"
, "Νυχτερινά κέντρα διασκέδασης, Μουσικές σκηνές"
, "Θέατρα, Κινηματογράφοι"
, "Χώροι συναυλιών"
, "Χώροι εκθέσεων, Μουσεία"
, "Χώροι συνεδρίων, Αμφιθέατρα, Αίθουσες δικαστηρίων"
, "Τράπεζες"
, "Αίθουσες πολλαπλών χρήσεων"
, "Κλειστό γυμναστήριο, Κλειστό κολυμβητήριο"
, "Λουτρά (κοινόχρηστα)"
, "Νηπιαγωγεία"
, "Πρωτοβάθμια εκπαίδευση, Δευτεροβάθμιας εκπαίδευση"
, "Τριτοβάθμιας εκπαίδευσης, Αίθουσες διδασκαλίας"
, "Φροντιστήρια, Ωδεία"
, "Νοσοκομεία, Κλινικές"
, "Αίθουσες ασθενών (δωμάτια)"
, "Χειρουργεία (τακτικά)"
, "Εξωτερικά ιατρεία"
, "Αγροτικά ιατρεία, Υγειονομικοί σταθμοί, Κέντρα υγείας, Ιατρεία"
, "Ψυχιατρεία, Ιδρύματα ατόμων με ειδικές ανάγκες, Ιδρύματα χρονίως πασχόντων, Οίκοι ευγηρίας, Βρεφοκομεία"
, "Βρεφικοί σταθμοί, Παιδικοί σταθμοί"
, "Κρατητήρια, Αναμορφωτήρια, Φυλακές"
, "Αστυνομικές Δ/νσεις"
, "Εμπορικά κέντρα, Αγορές και υπεραγορές"
, "Καταστήματα, Φαρμακεία"
, "Ινστιτούτα γυμναστικής, Κουρεία και κομμωτήρια"
, "Γραφεία"
, "Βιβλιοθήκες"

                                                 };
        private List<string> UsageStrings = new List<string>()
                                                 {
                                                  "	1 Μονοκατοικία, πολυκατοικία			"
,"	2 Ξενοδοχείο ετήσιας λειτουργίας			"
,"	3 Ξενοδοχείο ετήσιας λειτουργίας - Υπνοδωμάτια			"
,"	4 Ξενοδοχείο ετήσιας λειτουργίας - Κοινόχρηστοι χώροι			"
,"	5 Ξενοδοχείο θερινής λειτουργίας			"
,"	6 Ξενοδοχείο θερινής λειτουργίας - Υπνοδωμάτια			"
,"	7 Ξενοδοχείο θερινής λειτουργίας - Κοινόχρηστοι χώροι			"
,"	8 Ξενοδοχείο χειμερινής λειτουργίας			"
,"	9 Ξενοδοχείο χειμερινής λειτουργίας - Υπνοδωμάτια			"
,"	10 Ξενοδοχείο χειμερινής λειτουργίας - Κοινόχρηστοι χώροι			"
,"	11 Ξενώνες ετήσιας λειτουργίας			"
,"	12 Ξενώνες ετήσιας λειτουργίας - Υπνοδωμάτια			"
,"	13 Ξενώνες ετήσιας λειτουργίας - Κοινόχρηστοι χώροι			"
,"	14 Ξενώνες θερινής λειτουργίας			"
,"	15 Ξενώνες θερινής λειτουργίας - Υπνοδωμάτια			"
,"	16 Ξενώνες θερινής λειτουργίας - Κοινόχρηστοι χώροι			"
,"	17 Ξενώνες χειμερινής λειτουργίας			"
,"	18 Ξενώνες χειμερινής λειτουργίας - Υπνοδωμάτια			"
,"	19 Ξενώνες χειμερινής λειτουργίας - Κοινόχρηστοι χώροι			"
,"	20 Οικοτροφεία και Κοιτώνες			"
,"	21 Υπνοδωμάτια			"
,"	22 Κοινόχρηστοι χώροι			"
,"	23 Εστιατόρια			"
,"	24 Ζαχαροπλαστεία, Καφενεία			"
,"	25 Νυχτερινά κέντρα διασκέδασης, Μουσικές σκηνές			"
,"	26 Θέατρα, Κινηματογράφοι			"
,"	27 Χώροι συναυλιών			"
,"	28 Χώροι εκθέσεων, Μουσεία			"
,"	29 Χώροι συνεδρίων, Αμφιθέατρα, Αίθουσες δικαστηρίων			"
,"	30 Τράπεζες			"
,"	31 Αίθουσες πολλαπλών χρήσεων 			"
,"	32 Κλειστό γυμναστήριο, Κλειστό κολυμβητήριο			"
,"	33 Λουτρά (κοινόχρηστα)			"
,"	34 Νηπιαγωγεία			"
,"	35 Πρωτοβάθμια εκπαίδευση, Δευτεροβάθμιας εκπαίδευση			"
,"	36 Τριτοβάθμιας εκπαίδευσης, Αίθουσες διδασκαλίας			"
,"	37 Φροντιστήρια, Ωδεία			"
,"	38 Νοσοκομεία, Κλινικές			"
,"	39 Αίθουσες ασθενών (δωμάτια)			"
,"	40 Χειρουργεία (τακτικά)			"
,"	41 Εξωτερικά ιατρεία			"
,"	42 Αγροτικά ιατρεία, Υγειονομικοί σταθμοί, Κέντρα υγείας, Ιατρεία			"
,"	43 Ψυχιατρεία, Ιδρύματα ατόμων με ειδικές ανάγκες, Ιδρύματα χρονίως πασχόντων, Οίκοι ευγηρίας, Βρεφοκομεία			"
,"	44 Βρεφικοί σταθμοί, Παιδικοί σταθμοί			"
,"	45 Κρατητήρια, Αναμορφωτήρια, Φυλακές			"
,"	46 Αστυνομικές Δ/νσεις			"
,"	47 Εμπορικά κέντρα, Αγορές και υπεραγορές			"
,"	48 Καταστήματα, Φαρμακεία			"
,"	49 Ινστιτούτα γυμναστικής, Κουρεία και κομμωτήρια			"
,"	50 Γραφεία			"
,"	51 Βιβλιοθήκες			"


                                                 };

        public static List<EnumObject<object>> UsageTypes { get; set; }

        private EnumObject<object> _selectedUsageType;
        public EnumObject<object> SelectedUsageType
        {
            get { return _selectedUsageType; }
            set
            {
                _selectedUsageType = value;

                NotifyPropertyChanged(vm => vm.SelectedUsageType);
            }
        }

        private bool _isbuttonVisibility;
        public bool IsbuttonVisibility
        {
            get { return _isbuttonVisibility; }
            set
            {
                _isbuttonVisibility = value;
                NotifyPropertyChanged(vm => vm.IsbuttonVisibility);
            }
        }

        private bool _isTreeViewVisibility;
        public bool IsTreeViewVisibility
        {
            get { return _isTreeViewVisibility; }
            set
            {
                _isTreeViewVisibility = value;
                NotifyPropertyChanged(vm => vm.IsTreeViewVisibility);
            }
        }


        public void SetNewUsageById(int newValue)
        {
            this.SelectedUsageType = UsageTypes.FirstOrDefault(u => u.Id == newValue);
            // if (SelectedUsageType != null)
            //     this.Model.ProjectUseageId = this.SelectedUsageType.Id;
        }
    }


}
