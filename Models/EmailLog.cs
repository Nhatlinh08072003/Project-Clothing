using System;
using System.Collections.Generic;

namespace Project_Clothing.Models;

public partial class EmailLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public int EmailType { get; set; }

    public string? EmailContent { get; set; }

    public DateTime? SentAt { get; set; }

    public int? Status { get; set; }

    public virtual User? User { get; set; }
}
