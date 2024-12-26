using System;
using System.Collections.Generic;

namespace Project_Clothing.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }

    public int NotificationType { get; set; }

    public virtual User? User { get; set; }
}
