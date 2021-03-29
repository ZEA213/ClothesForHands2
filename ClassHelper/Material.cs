namespace ClothesForHands.EF
{
    public partial class Material
    {
        public string GetName { get => MaterialType.MaterialTypeName + "  |  " + MaterialName; }
        public string GetMinQTY { get => "Минимальное количество: " + MinQTY + " " + Unit.UnitName; }
        public string GetStockQTY { get => "Остаток: " + StockQTY + " " + Unit.UnitName; }

        public string GetSuppliers
        {
            get
            {
                string suppliers = "";

                foreach (var item in Supplier)
                {
                    if (suppliers != "")
                    {
                        suppliers += ", ";
                    }
                    suppliers += (item.SupplierName);
                }

                if (suppliers != "")
                {
                    return "Поставщики: " + suppliers;
                }
                else
                    return "Поставщики отсутствуют";
            }
        }

        public string GetImage
        {
            get
            {
                if (Image != "")
                {
                    return $"/Photo{Image}";
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetColor
        {
            get
            {
                if (StockQTY < MinQTY)
                {
                    return "#f19292";
                }
                else if (StockQTY >= MinQTY * 3)
                {
                    return "#ffba01";
                }
                else
                {
                    return "#B3F4E9";
                }
            }
        }
    }
}