// =============================================
// File: Models/Product.cs
// Purpose: Represents a product in the system.
// This model maps directly to the Products table in the database.
// =============================================

using System.ComponentModel.DataAnnotations;

namespace ProductCRUD.Models
{
    public class Product
    {
        // Primary key — auto-set by the database
        public int Id { get; set; }

        // Product name — required, max 100 characters
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        // Product price — required, must be non-negative
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        // Stock quantity — required, must be non-negative
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }
    }
}
