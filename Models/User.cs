using System;
using System.Collections.Generic;

namespace Delivery.Models;

public partial class User
{
    public static User? ActiveUser;
    public int UserId { get; set; }

    public int? RoleId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserOrder> UserOrders { get; set; } = new List<UserOrder>();
}
