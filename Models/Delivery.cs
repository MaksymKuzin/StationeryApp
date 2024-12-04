using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryApp.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int GoodsId { get; set; }
        public Goods Goods { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
    }

}
