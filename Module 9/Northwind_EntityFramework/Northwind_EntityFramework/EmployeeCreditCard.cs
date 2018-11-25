namespace Northwind_EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class EmployeeCreditCard
    {
        [Key]
        public int CreditCardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CardHolderName { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
