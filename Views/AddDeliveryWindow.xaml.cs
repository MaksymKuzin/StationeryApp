using System;
using System.Linq;
using System.Windows;
using StationeryApp.Models;

namespace StationeryApp
{
    public partial class AddDeliveryWindow : Window
    {
        private readonly AppDbContext _context;

        public AddDeliveryWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var goodsId = int.Parse(GoodsIdBox.Text);
                var supplierId = int.Parse(SupplierIdBox.Text);
                var quantity = int.Parse(QuantityBox.Text);

                // Додавання нової поставки через LINQ
                var delivery = new Delivery
                {
                    GoodsId = goodsId,
                    SupplierId = supplierId,
                    DeliveryDate = DateTime.Now,
                    Quantity = quantity
                };

                _context.Deliveries.Add(delivery);

                // Оновлення кількості товарів на складі через LINQ
                var goods = (from g in _context.Goods
                             where g.GoodsId == goodsId
                             select g).FirstOrDefault();

                if (goods != null)
                {
                    goods.StockQuantity += quantity;
                }

                _context.SaveChanges();
                MessageBox.Show("Delivery added successfully!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
