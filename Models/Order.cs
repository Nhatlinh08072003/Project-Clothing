using System;
using System.Collections.Generic;

namespace Project_Clothing.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? BuyerId { get; set; }

    public int? SellerId { get; set; }

    public decimal FinalPrice { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? ShippingAddress { get; set; }

    public string? PaymentMethod { get; set; }

    public int? PaymentStatus { get; set; }

    public int? OrderStatus { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? Seller { get; set; }
}
