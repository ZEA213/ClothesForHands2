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
using static ClothesForHands.EF.AppData;
using ClothesForHands.EF;


namespace ClothesForHands.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Window
    {

        List<Supplier> SupplierList = new List<Supplier>();

        public AddSupplier()
        {
            InitializeComponent();

            CBSupplier.ItemsSource = Context.Supplier.ToList();
            CBSupplier.DisplayMemberPath = "SupplierName";

            foreach (var item in Context.Material.Where(i => i.IdMaterial == material.IdMaterial))
            {
                SupplierList.Add((Supplier)item.Supplier);
            }

            LVSupplierList.ItemsSource = SupplierList;
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Context.Supplier.Where(i => i.IdSupplier == CBSupplier.SelectedIndex + 1))
            {
                SupplierList.Add(item);
            }

            LVSupplierList.ItemsSource = SupplierList;
            
        }

        private void SaveSupplier_Click(object sender, RoutedEventArgs e)
        {
            material.Supplier = SupplierList;
            Close();
        }
    }
}
