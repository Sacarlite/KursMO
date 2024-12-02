using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MetaInfo.TaskVisualization.Task1
{
    public partial class Task1ViewModel : ObservableObject, IDataErrorInfo
    {
        [ObservableProperty]
        private double alpfa;

        [ObservableProperty]
        private double betta;

        [ObservableProperty]
        private bool extrType;

        [ObservableProperty]
        private double tau;

        [ObservableProperty]
        private double mu;

        [ObservableProperty]
        private double delta;

        [ObservableProperty]
        private double g;

        [ObservableProperty]
        private double a;

        [ObservableProperty]
        private double n;

        [ObservableProperty]
        private double minT1;

        [ObservableProperty]
        private double maxT1;

        [ObservableProperty]
        private double minT2;

        [ObservableProperty]
        private double maxT2;

        [ObservableProperty]
        private double minDiff;

        [ObservableProperty]
        private double eps;

        [ObservableProperty]
        private double step;

        public Task1ViewModel()
        {
            ExtrType = true;
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "MinT1":
                        if (MinT1 < -273)
                        {
                            error = "Температура не может быть меньше -273℃";
                        }
                        break;
                    case "MinT2":
                        if (MinT2 < -273)
                        {
                            error = "Температура не может быть меньше -273℃";
                        }
                        break;
                    case "MaxT2":
                        if (MaxT2 < -273)
                        {
                            error = "Температура не может быть меньше -273℃";
                        }
                        break;
                    case "MaxT1":
                        if (MaxT1 < -273)
                        {
                            error = "Температура не может быть меньше -273℃";
                        }
                        break;
                    case "N":
                        if (N <= 0)
                        {
                            error = "Количество теплообменников не может быть меньше 0";
                        }
                        break;
                    case "G":
                        if (G <= 0)
                        {
                            error = "Расход реакционной массы не может быть меньше 0";
                        }
                        break;
                    case "A":
                        if (A <= 0)
                        {
                            error = "Давление не может быть отрицательным";
                        }
                        break;
                    case "Tau":
                        if (Tau <= 0)
                        {
                            error = "Время не может быть меньше нуля";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error => throw new NotImplementedException();

        public bool Validation()
        {
            bool flag = true;
            if (Alpfa == 0 || Betta == 0 || Mu == 0 || Delta == 0)
            {
                flag = false;
            }
            return flag;
        }
    }
}
