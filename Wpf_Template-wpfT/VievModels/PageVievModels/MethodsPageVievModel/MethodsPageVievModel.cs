using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Mvvm.Native;
using Domain.MethodsBD;
using Domain.UserBd;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace VievModel.PageVievModels.MethodsPageVievModel
{
    public partial class MethodsPageVievModel : ObservableObject, IMethodsPageVievModel
    {
        private const string AddClassification = "Добавить классификацию";
        private const string AddMethod = "Добавить метод";
        private IMethodsDatabaseLocator methodsDatabaseLocator;
        [ObservableProperty]
        string serchedName;
        [ObservableProperty]
        BindingList<Method> methods;
        private BindingList<Method> allMethods;
        [ObservableProperty]
        string addButtonText;
        [ObservableProperty]
        ObservableCollection<Сlassification> classifications;
        private ObservableCollection<Сlassification> allClassifications;
       [ObservableProperty]
        Сlassification? selectedClassification;
        [ObservableProperty]
        Visibility classificationVisability= Visibility.Visible;
        [ObservableProperty]
        Visibility methodsVisability = Visibility.Collapsed;
        [ObservableProperty]
        private bool? data;
        partial void OnSelectedClassificationChanged(Сlassification? value)
        {
                Methods = new BindingList<Method>(allMethods.Where(u => u.Classification== value).ToObservableCollection());

        }
     
        private void ItemsViewModel()
        {
            Methods.ListChanged += Methods_ListChanged; ;
        }

        private void Methods_ListChanged(object? sender, ListChangedEventArgs e)
        {
            OnSelectedClassificationChanged(SelectedClassification);
        }

        partial void OnDataChanging(bool? value)
        {
            if (value == true)
            {
                AddButtonText = AddMethod;
                MethodsVisability = Visibility.Visible;
                ClassificationVisability=Visibility.Collapsed;
            }
            else
            {
                Classifications = allClassifications;
                AddButtonText = AddClassification;
                MethodsVisability = Visibility.Collapsed;
                ClassificationVisability = Visibility.Visible;
            }
        }
        [RelayCommand]
        void AddData()
        {
            if (Data == true)
            {
                Methods = new BindingList<Method>(allMethods.Where(u => u.Name == SerchedName).ToList());
            }
            else
            {
                Classifications = allClassifications.Where(u => u.Name == SerchedName).ToObservableCollection();
            }
        }
        [RelayCommand]
        void Search()
        {
            if (Data == true)
            {
                Methods = new BindingList<Method>(allMethods.Where(u => u.Name == SerchedName).ToList());
            }
            else
            {
                Classifications = allClassifications.Where(u => u.Name == SerchedName).ToObservableCollection();
            }
        }
        [RelayCommand]
        void DeleteMethod(int Id)
        {
            var method = allMethods.Where(u => u.Id == Id).FirstOrDefault();
            if (method != null)
            {
                allMethods.Remove(method);
                Methods.Remove(method);
                methodsDatabaseLocator.Context.Methods.Remove(method);
                methodsDatabaseLocator.Context.SaveChanges();
            }
            MessageBox.Show("Метод успешно удалён");
        }
        [RelayCommand]
        void DeleteClassification(int Id)
        {
            var classification = Classifications.Where(u => u.Id == Id).FirstOrDefault();
            if (classification != null)
            {
                Classifications.Remove(classification);
                allClassifications.Remove(classification);
                methodsDatabaseLocator.Context.Сlasses.Remove(classification);
                methodsDatabaseLocator.Context.SaveChanges();
            }
            MessageBox.Show("Классификация успешно удалена");

        }
        [RelayCommand]
        void PageLoading()
        {
            allClassifications = methodsDatabaseLocator.Context.Сlasses.ToObservableCollection();
            allMethods = new BindingList<Method>(methodsDatabaseLocator.Context.Methods.ToObservableCollection());
            Methods = allMethods;
            Classifications = allClassifications;
            ItemsViewModel();
            Data = false;
         
        }

        private void Elem_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (SelectedClassification is not null)
                Methods = new BindingList<Method>(allMethods.Where(u => u.Classification.Id == SelectedClassification.Id).ToObservableCollection());
        }

        public MethodsPageVievModel(IMethodsDatabaseLocator methodsDatabaseLocator)
        {
            this.methodsDatabaseLocator = methodsDatabaseLocator;
        }

        public void Dispose()
        {
 
        }
    }
}
