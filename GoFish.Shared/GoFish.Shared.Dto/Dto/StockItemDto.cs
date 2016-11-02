using System;
using System.ComponentModel.DataAnnotations;

namespace GoFish.Shared.Dto
{
    public class StockItemDto
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                    return Guid.NewGuid();

                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [Required]
        [Display(Name = "Product")]
        [Range(1, Int32.MaxValue)] // TODO: This'll probably end up being something else, Guid? PK from DataSource?
        public int ProductTypeId { get; set; }

        [Required]
        [Range(1, 100000)]
        public int? Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, Double.MaxValue)]
        public double? Price { get; set; }
        public int OwnerId { get; set; }
        public Guid AdvertId { get; set; }
    }
}