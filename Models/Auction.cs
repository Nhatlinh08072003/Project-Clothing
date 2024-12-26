using System;
using System.Collections.Generic;

namespace Project_Clothing.Models;

public partial class Auction
{
    public int AuctionId { get; set; }

    public int? ProductId { get; set; }

    public int? BidderId { get; set; }

    public decimal BidAmount { get; set; }

    public DateTime? BidTime { get; set; }

    public int? Status { get; set; }

    public virtual User? Bidder { get; set; }

    public virtual Product? Product { get; set; }
}
