using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LiveChartsDemo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public SeriesCollection PieSeries { get; set; }
        public SeriesCollection ColumnSeries { get; set; }
        public SeriesCollection LineSeries { get; set; }

        // Метки для оси X
        private string[] _monthLabels = { "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек" };
        public string[] MonthLabels
        {
            get => _monthLabels;
            set { _monthLabels = value; OnPropertyChanged(); }
        }

        // Команда для обновления данных
        public ICommand UpdateDataCommand { get; }

        public MainViewModel()
        {
            // Инициализация круговой диаграммы
            PieSeries = new SeriesCollection
            {
                new PieSeries { Title = "Категория A", Values = new ChartValues<double> { 35 } },
                new PieSeries { Title = "Категория B", Values = new ChartValues<double> { 25 } },
                new PieSeries { Title = "Категория C", Values = new ChartValues<double> { 20 } },
                new PieSeries { Title = "Категория D", Values = new ChartValues<double> { 20 } }
            };

            // Инициализация гистограммы
            ColumnSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Продажи 2024",
                    Values = new ChartValues<double> { 10, 15, 12, 18, 22, 28, 35, 42, 50, 55, 60, 65 }
                }
            };

            // Инициализация линейного графика
            LineSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Температура",
                    Values = new ChartValues<double> { -5, 0, 4, 10, 15, 20, 22, 21, 16, 9, 3, -2 },
                    LineSmoothness = 0,    // Делает линии прямыми, а не сглаженными
                    PointGeometry = null,   // Убираем маркеры на точках
                    StrokeThickness = 3
                }
            };

            UpdateDataCommand = new RelayCommand(UpdateData);
        }

        private void UpdateData()
        {
            var random = new Random();

            // Обновление круговой диаграммы
            var newPieSeries = new SeriesCollection();
            foreach (PieSeries series in PieSeries)
            {
                var newValue = random.Next(10, 60);
                newPieSeries.Add(new PieSeries { Title = series.Title, Values = new ChartValues<double> { newValue } });
            }
            PieSeries = newPieSeries;
            OnPropertyChanged(nameof(PieSeries));

            // Обновление гистограммы
            if (ColumnSeries[0] is ColumnSeries colSeries)
            {
                var newValues = new ChartValues<double>();
                for (int i = 0; i < 12; i++)
                    newValues.Add(random.Next(20, 100));
                colSeries.Values = newValues;
            }

            // Обновление линейного графика
            if (LineSeries[0] is LineSeries lineSeries)
            {
                var newValues = new ChartValues<double>();
                for (int i = 0; i < 12; i++)
                    newValues.Add(random.Next(-10, 35));
                lineSeries.Values = newValues;
            }
        }
    }
}
