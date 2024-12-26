using System;
using System.Collections.Generic;

namespace Project_Clothing.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public int? SellerId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal InitialPrice { get; set; }

    public decimal CurrentPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime EndTime { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual User? Seller { get; set; }
}
