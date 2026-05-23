using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PromotionId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }

    public int? PaymentMethod { get; set; }

    public int? PaymentStatus { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<ExportReceipt> ExportReceipts { get; set; } = new List<ExportReceipt>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Payment? PaymentMethodNavigation { get; set; }

    public virtual PaymentStatus? PaymentStatusNavigation { get; set; }

    public virtual Promotion? Promotion { get; set; }

    public virtual OrderStatus? StatusNavigation { get; set; }

    public virtual User? User { get; set; }
}
