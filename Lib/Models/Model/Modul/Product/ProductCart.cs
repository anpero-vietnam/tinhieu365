using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class ProductCart
    {
        string thumb, title;
        int quantity, id, price;

        public int Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Thumb
        {
            get
            {
                return thumb;
            }

            set
            {
                thumb = value;
            }
        }

        public ProductCart()
        {
            id = 0;
            Price = 0;
            Thumb = "";
            Title = "";
            Quantity = 0;
        }
    }
}
