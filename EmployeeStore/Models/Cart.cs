using System.Collections.Generic;
using System.Linq;

namespace EmployeeStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Employee product, int quantity, decimal sum)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    TotalSum = sum
                });
            }
            else
            {
                line.Quantity += quantity;
                line.TotalSum += sum;
            }
        }

        public virtual void RemoveLine(Employee product) =>
            lineCollection.RemoveAll(l => l.Product.Id == product.Id);

        public virtual decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public virtual void Clear() =>
            lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines =>
            lineCollection;
    }
}
