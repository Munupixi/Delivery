using System;
using System.Collections.Generic;

namespace Delivery.Models;

public partial class UserOrder
{
    public UserOrder(int userOrderId, int? statusId, DateTime? orderDate, string? clientCompleteName, string? clientPhone, string? itemTitle, string? location, int? executorId)
    {
        UserOrderId = userOrderId;
        StatusId = statusId;
        OrderDate = orderDate;
        ClientCompleteName = clientCompleteName;
        ClientPhone = clientPhone;
        ItemTitle = itemTitle;
        Location = location;
        ExecutorId = executorId;
    }

    public int UserOrderId { get; set; }

    public int? StatusId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? ClientCompleteName { get; set; }

    public string? ClientPhone { get; set; }

    public string? ItemTitle { get; set; }

    public string? Location { get; set; }

    public int? ExecutorId { get; set; }

    public virtual User? Executor { get; set; }

    public virtual Status? Status { get; set; }
}
