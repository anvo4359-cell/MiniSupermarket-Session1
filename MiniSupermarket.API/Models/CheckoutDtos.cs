
namespace MiniSupermarket.API.Models
{
    // Một sản phẩm trong giỏ hàng thanh toán
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // Thông tin yêu cầu thanh toán
    public class CheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
