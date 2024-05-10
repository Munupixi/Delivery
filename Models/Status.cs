using System;
using System.Collections.Generic;

namespace Delivery.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<UserOrder> UserOrders { get; set; } = new List<UserOrder>();
}
