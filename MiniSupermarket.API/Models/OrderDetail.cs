using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models;

[Table("OrderDetails")]
public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }
    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Subtotal { get; set; }

    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
