using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models;

[Table("Orders")]
public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required, StringLength(50)]
    public string OrderCode { get; set; } = string.Empty;

    public int? CustomerId { get; set; }

    [Required, StringLength(100)]
    public string CashierUsername { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal FinalAmount { get; set; }

    public int RewardPoints { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
}
