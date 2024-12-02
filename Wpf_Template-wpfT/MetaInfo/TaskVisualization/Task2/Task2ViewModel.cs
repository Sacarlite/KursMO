using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MetaInfo.TaskVisualization.Task2
{
    public partial class Task2ViewModel : ObservableObject, IDataErrorInfo
    {
        [ObservableProperty]
        private double alpfa;

        [ObservableProperty]
        private double betta;

        [ObservableProperty]
        private double gamma;

        [ObservableProperty]
        private bool extrType;

        [ObservableProperty]
        private double tau;

        [ObservableProperty]
        private double a1;

        [ObservableProperty]
        private double a2;

        [ObservableProperty]
        private double v1;

        [ObservableProperty]
        private double v2;

        [ObservableProperty]
        private double minT1;

        [ObservableProperty]
        private double maxT1;

        [ObservableProperty]
        private double minT2;

        [ObservableProperty]
        private double maxT2;

        [ObservableProperty]
        private double maxSumm;

        [ObservableProperty]
        private double eps;

        [ObservableProperty]
        private double step;

        public Task2ViewModel()
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
                    case "V1":
                        if (V1 <= 0)
                        {
                            error = "Объём не может быть меньше 0";
                        }
                        break;
                    case "V2":
                        if (V2 <= 0)
                        {
                            error = "Объём не может быть меньше 0";
                        }
                        break;
                    case "A1":
                        if (A1 <= 0)
                        {
                            error = "Расход реакционной массы не может быть меньше 0";
                        }
                        break;
                    case "A2":
                        if (A2 <= 0)
                        {
                            error = "Расход реакционной массы не может быть меньше 0";
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
            if (Alpfa == 0 || Betta == 0 || Gamma == 0)
            {
                flag = false;
            }
            return flag;
        }
    }
}
