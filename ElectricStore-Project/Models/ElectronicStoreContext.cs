using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Models;

public partial class ElectronicStoreContext : DbContext
{
    public ElectronicStoreContext()
    {
    }

    public ElectronicStoreContext(DbContextOptions<ElectronicStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<ExportReceipt> ExportReceipts { get; set; }

    public virtual DbSet<ExportReceiptDetail> ExportReceiptDetails { get; set; }

    public virtual DbSet<ImportReceipt> ImportReceipts { get; set; }

    public virtual DbSet<ImportReceiptDetail> ImportReceiptDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<PromotionApply> PromotionApplies { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SpecificationAttribute> SpecificationAttributes { get; set; }

    public virtual DbSet<StockLog> StockLogs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ElectronicStore;User=sa;Password=1;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3214EC0786301E82");

            entity.ToTable("Address");

            entity.Property(e => e.FullAddress).HasColumnName("Full_Address");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Address_User");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC07E5474E41");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .HasColumnName("Brand_Name");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07693D4035");

            entity.ToTable("Cart");

            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Cart_User");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart_Ite__3214EC07FA5355D1");

            entity.ToTable("Cart_Item");

            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_CartItem_Cart");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CartItem_Product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07ADE177E7");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC0717BF7FC5");

            entity.ToTable("Comment");

            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_At");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC078D5E4160");

            entity.ToTable("Country");

            entity.Property(e => e.Country1)
                .HasMaxLength(100)
                .HasColumnName("Country");
        });

        modelBuilder.Entity<ExportReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Export_R__3214EC07DC9DF2AA");

            entity.ToTable("Export_Receipt");

            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_At");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.ExportStatus)
                .HasMaxLength(20)
                .HasColumnName("Export_Status");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ExportReceipts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_ExportReceipt_User");

            entity.HasOne(d => d.Order).WithMany(p => p.ExportReceipts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ExportReceipt_Order");
        });

        modelBuilder.Entity<ExportReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Export_R__3214EC075A1B405B");

            entity.ToTable("Export_Receipt_Detail");

            entity.Property(e => e.ExportReceiptId).HasColumnName("Export_Receipt_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Unit_Price");

            entity.HasOne(d => d.ExportReceipt).WithMany(p => p.ExportReceiptDetails)
                .HasForeignKey(d => d.ExportReceiptId)
                .HasConstraintName("FK_ExportReceiptDetail_ExportReceipt");

            entity.HasOne(d => d.Product).WithMany(p => p.ExportReceiptDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ExportReceiptDetail_Product");
        });

        modelBuilder.Entity<ImportReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Import_R__3214EC07C80F2E23");

            entity.ToTable("Import_Receipt");

            entity.Property(e => e.ApprovedAt)
                .HasColumnType("datetime")
                .HasColumnName("Approved_At");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_At");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.ImportStatus)
                .HasMaxLength(20)
                .HasColumnName("Import_Status");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.ReferenceCode)
                .HasMaxLength(20)
                .HasColumnName("Reference_Code");
            entity.Property(e => e.SourceFile).HasColumnName("Source_File");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ImportReceipts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_ImportReceipt_User");
        });

        modelBuilder.Entity<ImportReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Import_R__3214EC079EC25999");

            entity.ToTable("Import_Receipt_Detail");

            entity.Property(e => e.ImportReceiptId).HasColumnName("Import_Receipt_Id");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.QuantityActual).HasColumnName("Quantity_Actual");
            entity.Property(e => e.QuantityExpected).HasColumnName("Quantity_Expected");
            entity.Property(e => e.UnitCost)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Unit_Cost");

            entity.HasOne(d => d.ImportReceipt).WithMany(p => p.ImportReceiptDetails)
                .HasForeignKey(d => d.ImportReceiptId)
                .HasConstraintName("FK_ImportReceiptDetail_ImportReceipt");

            entity.HasOne(d => d.Product).WithMany(p => p.ImportReceiptDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ImportReceiptDetail_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07D05D0B7D");

            entity.ToTable("Order");

            entity.Property(e => e.AddressId).HasColumnName("Address_Id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_At");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Discount_Amount");
            entity.Property(e => e.PaymentMethod).HasColumnName("Payment_Method");
            entity.Property(e => e.PaymentStatus).HasColumnName("Payment_Status");
            entity.Property(e => e.PromotionId).HasColumnName("Promotion_Id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Total_Amount");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Order_Address");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethod)
                .HasConstraintName("FK_Order_Payment");

            entity.HasOne(d => d.PaymentStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentStatus)
                .HasConstraintName("FK_Order_PaymentStatus");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK_Order_Promotion");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK_Order_Status");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_De__3214EC074536F620");

            entity.ToTable("Order_Detail");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Unit_Price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_St__3214EC07C32CC96C");

            entity.ToTable("Order_Status");

            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC073A5CFCD4");

            entity.ToTable("Payment");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment___3214EC0789A255EC");

            entity.ToTable("Payment_Status");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07B5441BCA");

            entity.ToTable("Product");

            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Gift).HasMaxLength(255);
            entity.Property(e => e.ImportPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Import_Price");
            entity.Property(e => e.InstallmentTag).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasColumnName("Is_Active");
            entity.Property(e => e.MadeIn).HasColumnName("Made_In");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OriginalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Sale_Price");
            entity.Property(e => e.StockQuantity).HasColumnName("Stock_Quantity");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.MadeInNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.MadeIn)
                .HasConstraintName("FK_Product_Country");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Supplier_Id");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product___3214EC075CD40B33");

            entity.ToTable("Product_Image");

            entity.Property(e => e.DisplayOrder).HasColumnName("Display_Order");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<ProductSpecificationMapping>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.AttributeId }).HasName("PK__Product___FEE8BE111ED0FEB1");

            entity.ToTable("Product_Specification_Mapping");

            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.AttributeId).HasColumnName("Attribute_Id");
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductSpecificationMappings)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PSMAttributeID_SAId");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC07C7EC2B5C");

            entity.ToTable("Promotion");

            entity.HasIndex(e => e.Code, "UQ__Promotio__A25C5AA788CE7CB7").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DiscountValue)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Discount_value");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.IsActive).HasColumnName("Is_Active");
            entity.Property(e => e.MaxDiscountAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Max_Discount_Amount");
            entity.Property(e => e.MinOrderValue)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Min_order_value");
            entity.Property(e => e.PromotionApplyId).HasColumnName("Promotion_Apply_Id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.UsageLimit).HasColumnName("Usage_Limit");
            entity.Property(e => e.UsedCount).HasColumnName("Used_Count");

            entity.HasOne(d => d.PromotionApply).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.PromotionApplyId)
                .HasConstraintName("FK_Promotion_PromotionApply");
        });

        modelBuilder.Entity<PromotionApply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC076E6B128B");

            entity.ToTable("Promotion_Apply");

            entity.Property(e => e.CategoryType).HasColumnName("Category_Type");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0774AFF6D5");

            entity.ToTable("Role");

            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        modelBuilder.Entity<SpecificationAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Specific__3214EC0781D0AAA0");

            entity.ToTable("Specification_Attribute");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.SpecificationAttributes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_SACategoryId_CategoryId");
        });

        modelBuilder.Entity<StockLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stock_Lo__3214EC0746908AFE");

            entity.ToTable("Stock_Log");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.ExportReceiptId).HasColumnName("Export_Receipt_Id");
            entity.Property(e => e.ImportReceiptId).HasColumnName("Import_Receipt_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.RefType)
                .HasMaxLength(20)
                .HasColumnName("Ref_Type");
            entity.Property(e => e.StockAfter).HasColumnName("Stock_After");
            entity.Property(e => e.StockBefore).HasColumnName("Stock_Before");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.ExportReceipt).WithMany(p => p.StockLogs)
                .HasForeignKey(d => d.ExportReceiptId)
                .HasConstraintName("FK_StockLog_ExportReceipt");

            entity.HasOne(d => d.ImportReceipt).WithMany(p => p.StockLogs)
                .HasForeignKey(d => d.ImportReceiptId)
                .HasConstraintName("FK_StockLog_ImportReceipt");

            entity.HasOne(d => d.Product).WithMany(p => p.StockLogs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_StockLog_Product");

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockLogs)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_SL_S_Supplier_Id");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC072A744A6E");

            entity.ToTable("Supplier");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0782C8B709");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105342191D47F").IsUnique();

            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_At");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("Full_Name");
            entity.Property(e => e.IsActive).HasColumnName("Is_Active");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
