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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        ObservableCollection<Method> methods;
        private ObservableCollection<Method> allMethods;
        [ObservableProperty]
        string addButtonText;
        [ObservableProperty]
        ObservableCollection<Сlassification> classifications;
        private ObservableCollection<Сlassification> allClassifications;
       [ObservableProperty]
        Сlassification selectedClassification;
        [ObservableProperty]
        Visibility classificationVisability= Visibility.Visible;
        [ObservableProperty]
        Visibility methodsVisability = Visibility.Collapsed;
        [ObservableProperty]
        private bool? data;
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
                Methods = allMethods.Where(u => u.Name == SerchedName).ToObservableCollection();
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
                Methods = allMethods.Where(u => u.Name == SerchedName).ToObservableCollection();
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
            Classifications = allClassifications;
            allMethods = methodsDatabaseLocator.Context.Methods.ToObservableCollection();
            Methods = allMethods;
            Data = false;
        }
        public MethodsPageVievModel(IMethodsDatabaseLocator methodsDatabaseLocator)
        {
            this.methodsDatabaseLocator = methodsDatabaseLocator;
            Сlassification сlassification1 = new Сlassification("Test1");
            Сlassification сlassification2 = new Сlassification("Test2");
            methodsDatabaseLocator.Context.Сlasses.AddRange(сlassification1, сlassification2);
            Method method1 = new Method("test", "test", "test") { Classification = сlassification1 };
            Method method2 = new Method("test", "test", "test") { Classification = сlassification1 };
            Method method3 = new Method("test", "test", "test") { Classification = сlassification1 };
            Method method4 = new Method("test", "test", "test") { Classification = сlassification2 };
            Method method5 = new Method("test", "test", "test") { Classification = сlassification2 };
            methodsDatabaseLocator.Context.Methods.AddRange(method1, method2, method3, method4, method5);
            methodsDatabaseLocator.Context.SaveChanges();
        }

        public void Dispose()
        {
 
        }
    }
}
