using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FreemanMVC.Models
{
    public class Cart
    {
        public Cart(IEnumerable<CartLine> Lines)
        {
            lineCollection.AddRange(Lines);
        }

        public readonly List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = lineCollection
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();
            
            if(line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveItem(Product product) =>
            lineCollection.RemoveAll(l=>l.Product.ProductId == product.ProductId);

        public virtual decimal ComputerTotalValue()=>
            lineCollection.Sum(e=>e.Product.Price * e.Quantity);

        public virtual void Clear()=>lineCollection.Clear();

        public IEnumerable<CartLine> Lines => lineCollection;

    }

    public class CartLine
    {
        public int CartLineId { get; set;}
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
