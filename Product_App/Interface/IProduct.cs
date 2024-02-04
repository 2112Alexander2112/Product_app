using Product_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_App.Interface
{
    internal interface IProduct
    {

        void loadData();
        void saveData();
        void AddProduct(Product prod);
        void ChangeProduct(Product prod);
    }
}
