using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ClothesForHands.ClassHelper.MaterialHelper;

namespace ClothesForHands.Windows
{
    /// <summary>
    /// Логика взаимодействия для MinQTYEditWindow.xaml
    /// </summary>
    public partial class MinQTYEditWindow : Window
    {
        public MinQTYEditWindow()
        {
            InitializeComponent();
            TBMinQTY.Text = GetMinQTY.ToString();
        }

        private void MinQTY_Click(object sender, RoutedEventArgs e)
        {
            if (TBMinQTY.Text != "")
            {
                if (int.TryParse(TBMinQTY.Text, out int MinQTY))
                {
                    if (MessageBox.Show("Сохранить изменения?",
                        "Сохранение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        GetMinQTY = MinQTY;
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Введите числовое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пустое поле", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}