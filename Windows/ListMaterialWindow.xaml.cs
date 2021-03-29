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
using ClothesForHands.ClassHelper;
using ClothesForHands.EF;
using static ClothesForHands.EF.AppData;

namespace ClothesForHands.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListMaterialWindow.xaml
    /// </summary>
    public partial class ListMaterialWindow : Window
    {
        private List<Material> materialsList = new List<Material>();
        private readonly List<string> ListSort = new List<string>()
        {
            "Наименование (по возрастанию)",
            "Наименование (по убыванию)",
            "Остаток на складе (по возрастанию)",
            "Остаток на складе (по убыванию)",
            "Стоимость (по возрастанию)",
            "Стоимость (по убыванию)"
        };
        private readonly List<string> ListFilter = new List<string>();

        private List<Material> selectMaterials = new List<Material>();

        private int NumPage = 1;
        public ListMaterialWindow()
        {
            InitializeComponent();

            var typeMaterial = Context.MaterialType.OrderBy(i => i.IdMaterialType).ToList();
            foreach (var item in typeMaterial)
            {
                ListFilter.Add(item.MaterialTypeName);
            }
            ListFilter.Insert(0, "Все типы");
            CBFilter.ItemsSource = ListFilter;
            CBFilter.SelectedIndex = 0;

            CBSort.ItemsSource = ListSort;
            CBSort.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            materialsList = Context.Material.ToList();

            //Сортировка
            switch (CBSort.SelectedIndex)
            {
                case 0:
                    materialsList = materialsList.OrderBy(i => i.MaterialName).ToList();
                    break;
                case 1:
                    materialsList = materialsList.OrderByDescending(i => i.MaterialName).ToList();
                    break;
                case 2:
                    materialsList = materialsList.OrderBy(i => i.StockQTY).ToList();
                    break;
                case 3:
                    materialsList = materialsList.OrderByDescending(i => i.StockQTY).ToList();
                    break;
                case 4:
                    materialsList = materialsList.OrderBy(i => i.Price).ToList();
                    break;
                case 5:
                    materialsList = materialsList.OrderByDescending(i => i.Price).ToList();
                    break;
            }

            //Фильтрация
            if (CBFilter.SelectedIndex != 0)
            {
                materialsList = materialsList.Where(i => i.IdMaterialType == CBFilter.SelectedIndex).ToList();
            }

            //Поиск
            materialsList = materialsList.Where(i => i.GetName.ToLower().Contains(TBMaterialName.Text)).ToList();

            //Вывлд по страницам и вывод количества записей
            TBItemAll.Text = materialsList.Count().ToString();

            materialsList = materialsList.Skip((NumPage - 1) * 15).Take(15).ToList();

            TBItemCount.Text = materialsList.Count().ToString();


            //Кнопки по переходу по страницам
            if (NumPage > 2)
            {
                BtnFirst.Content = NumPage - 1;
                BtnSecond.Content = NumPage;
                BtnThird.Content = NumPage + 1;
            }
            else if (NumPage < 3)
            {
                BtnFirst.Content = 1;
                BtnSecond.Content = 2;
                BtnThird.Content = 3;
            }

            SolidColorBrush brush = new SolidColorBrush();
            SolidColorBrush brushwhite = new SolidColorBrush();

            brush.Color = Color.FromRgb(165, 232, 135);
            brushwhite.Color = Color.FromRgb(255, 255, 255);

            if (Convert.ToInt32(BtnFirst.Content) == NumPage)
            {
                BtnFirst.Background = brush;
            }
            else
            {
                BtnFirst.Background = brushwhite;
            }
            if (Convert.ToInt32(BtnSecond.Content) == NumPage)
            {
                BtnSecond.Background = brush;
            }
            else
            {
                BtnSecond.Background = brushwhite;
            }
            if (Convert.ToInt32(BtnThird.Content) == NumPage)
            {
                BtnThird.Background = brush;
            }
            else
            {
                BtnThird.Background = brushwhite;
            }

            if (materialsList.Count != 15)
            {
                BtnThird.IsEnabled = false;
            }
            else
            {
                BtnThird.IsEnabled = true;
            }

            LVMaterial.ItemsSource = materialsList;
        }
        private void TBMaterialName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NumPage = 1;
            Filter();
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumPage = 1;
            Filter();
        }

        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumPage = 1;
            Filter();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NumPage > 1)
            {
                NumPage--;
            }

            Filter();
        }
        private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            NumPage = Convert.ToInt32(BtnFirst.Content);

            Filter();
        }
        private void BtnSecond_Click(object sender, RoutedEventArgs e)
        {
            NumPage = Convert.ToInt32(BtnSecond.Content);

            Filter();
        }
        private void BtnThird_Click(object sender, RoutedEventArgs e)
        {
            NumPage = Convert.ToInt32(BtnThird.Content);

            Filter();
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (materialsList.Count == 15)
            {
                NumPage++;
            }

            Filter();
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            MaterialHelper.AddEditTitle = "Добавление материала";
            AddEditMaterialWindow addEdit = new AddEditMaterialWindow();
            addEdit.ShowDialog();

            Context.SaveChanges();
            Filter();
        }
        private void EditMaterial_Click(object sender, RoutedEventArgs e)
        {
            MaterialHelper.AddEditTitle = "Изменение материала";
            AddEditMaterialWindow addEdit = new AddEditMaterialWindow(LVMaterial.SelectedItem as Material);
            addEdit.ShowDialog();

            Context.SaveChanges();
            Filter();
        }

        private void LVMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BTN_MinQTY.Visibility = Visibility.Visible;
            BTN_EditMaterial.Visibility = Visibility.Visible;
        }

        private void MinQTY_Click(object sender, RoutedEventArgs e)
        {
            foreach (Material item in LVMaterial.SelectedItems)
            {
                selectMaterials.Add(item);
            }

            if (selectMaterials != null)
            {
                MaterialHelper.GetMinQTY = selectMaterials.Max(i => i.MinQTY);

                MinQTYEditWindow minQTYWindow = new MinQTYEditWindow();
                minQTYWindow.ShowDialog();

                foreach (Material item in selectMaterials)
                {
                    item.MinQTY = MaterialHelper.GetMinQTY;
                }

                Context.SaveChanges();

                selectMaterials.Clear();
            }

            Filter();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}