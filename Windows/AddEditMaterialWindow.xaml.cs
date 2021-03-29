using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static ClothesForHands.EF.AppData;
using ClothesForHands.EF;
using ClothesForHands.ClassHelper;

namespace ClothesForHands.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditMaterialWindow.xaml
    /// </summary>
    public partial class AddEditMaterialWindow : Window
    {
        private readonly List<string> ListMaterialType = new List<string>();
        private readonly List<string> ListUnit = new List<string>();
        Material addMaterial = new Material();

        public AddEditMaterialWindow()
        {
            InitializeComponent();
            Title += AddEditTitle;
            TBTitle.Text = AddEditTitle;

            var typeMaterial = Context.MaterialType.OrderBy(i => i.IdMaterialType).ToList();
            foreach (var item in typeMaterial)
            {
                ListMaterialType.Add(item.MaterialTypeName);
            }
            CBMaterialType.ItemsSource = ListMaterialType;
            CBMaterialType.SelectedIndex = 0;

            var Unit = Context.Unit.OrderBy(i => i.IdUnit).ToList();
            foreach (var item in Unit)
            {
                ListUnit.Add(item.UnitName);
            }
            CBUnit.ItemsSource = ListUnit;
            CBUnit.SelectedIndex = 0;
        }

        public AddEditMaterialWindow(Material material)
        {
            InitializeComponent();
            Title += AddEditTitle;
            TBTitle.Text = AddEditTitle;

            var typeMaterial = Context.MaterialType.OrderBy(i => i.IdMaterialType).ToList();
            foreach (var item in typeMaterial)
            {
                ListMaterialType.Add(item.MaterialTypeName);
            }
            CBMaterialType.ItemsSource = ListMaterialType;
            CBMaterialType.SelectedIndex = 0;

            var Unit = Context.Unit.OrderBy(i => i.IdUnit).ToList();
            foreach (var item in Unit)
            {
                ListUnit.Add(item.UnitName);
            }
            CBUnit.ItemsSource = ListUnit;
            CBUnit.SelectedIndex = 0;

            TBoxName.Text = material.MaterialName;
            TBoxStockQTY.Text = material.StockQTY.ToString();
            TBoxPackQTY.Text = material.PackQTY.ToString();
            TBoxMinQTY.Text = material.MinQTY.ToString();
            CBMaterialType.SelectedIndex = material.IdMaterialType - 1;
            CBUnit.SelectedIndex = material.IdUnit - 1;

            if (material.Image != "")
            {
                MaterialImage.Source = new BitmapImage(new Uri(material.Image));
            }

            MaterialHelper.material = material;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            var resultClick = MessageBox.Show("Добавить?", "Добавление нового материала", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultClick == MessageBoxResult.Yes)
            {
                if (TBoxImage.Text != "")
                {
                    var format = TBoxImage.Text.Split('.')[TBoxImage.Text.Split('.').Length - 1];

                    string namePhoto = $@"\materials\{random.Next()}.{format}";

                    File.Copy(TBoxImage.Text, $@"..\..\{namePhoto}");
                    addMaterial.Image = namePhoto;
                }
                addMaterial.MaterialName = TBoxName.Text;
                addMaterial.IdMaterialType = CBMaterialType.SelectedIndex + 1;
                addMaterial.StockQTY = Convert.ToInt32(TBoxStockQTY.Text);
                addMaterial.PackQTY = Convert.ToInt32(TBoxPackQTY.Text);
                addMaterial.MinQTY = Convert.ToInt32(TBoxMinQTY.Text);
                addMaterial.IdUnit = CBUnit.SelectedIndex + 1;

                Context.Material.Add(addMaterial); // добавление материала


                // добавление поставщиков для материала
                
                
                Context.SaveChanges();

                Context.Material.Add(material);

                Context.SaveChanges();

                Close();
            }
        }
        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                TBoxImage.Text = openFileDialog.FileName;
                MaterialImage.Source = new BitmapImage(new Uri(TBoxImage.Text));
            }

        }
        private void Supplier_Click(object sender, RoutedEventArgs e)
        {
            material = addMaterial;

            AddSupplier addSupplier = new AddSupplier();
            addSupplier.ShowDialog();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Title = "Одежда для ручек  –  ";
            TBTitle.Text = "";
            Close();
        }

        private void CBUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TBUnitPack.Text = CBUnit.SelectedItem.ToString();
            TBUnitMin.Text = CBUnit.SelectedItem.ToString();
        }
    }
}