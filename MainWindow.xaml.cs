using System.Linq;
using System.Windows;

namespace StationeryApp
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadGoods();
        }

        // Завантаження товарів через LINQ
        private void LoadGoods()
        {
            var goodsList = (from g in _context.Goods
                             select new
                             {
                                 g.GoodsId,
                                 g.GoodsName,
                                 g.GoodsColor,
                                 g.StockQuantity,
                                 g.UnitPrice
                             }).ToList();

            GoodsDataGrid.ItemsSource = goodsList;
        }

        // Додавання поставки
        private void AddDelivery_Click(object sender, RoutedEventArgs e)
        {
            var addDeliveryWindow = new AddDeliveryWindow(_context);
            addDeliveryWindow.ShowDialog();
            LoadGoods(); // Оновлення даних після додавання поставки
        }

        // Розрахунок загальної вартості товарів постачальника через LINQ
        private void CalculateStockValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Отримати ID постачальника з текстового поля
                if (!int.TryParse(SupplierIdBox.Text, out int supplierId))
                {
                    MessageBox.Show("Invalid Supplier ID. Please enter a valid number.");
                    return;
                }

                // Розрахунок вартості товарів на складі через LINQ
                var totalStockValue = (from d in _context.Deliveries
                                       where d.SupplierId == supplierId
                                       join g in _context.Goods on d.GoodsId equals g.GoodsId
                                       select d.Quantity * g.UnitPrice).Sum();

                MessageBox.Show($"Загальна вартість товарів для постачальника {supplierId}: {totalStockValue:C}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating stock value: {ex.Message}");
            }
        }
    }
}
