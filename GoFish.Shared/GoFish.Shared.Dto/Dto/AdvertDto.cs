using System;
using System.ComponentModel.DataAnnotations;

namespace GoFish.Shared.Dto
{
    public class AddAdvertToStockDto : AdvertDto
    {
        public int StockQuantity { get; set; }
    }

    public class AdvertDto
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
        [DataType(DataType.Currency)]
        [Range(0, Double.MaxValue)]
        public double? Price { get; set; }

        [Range(1, Int32.MaxValue)] // TODO: See GitHub Issue #4
        public int? AdvertiserId { get; set; }

        [Required]
        [Display(Name = "Catch Type")]
        [Range(1, Int32.MaxValue)] // TODO: This'll probably end up being something else, Guid? PK from DataSource?
        public int? CatchTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLengthAttribute(200, MinimumLength = 4)] // "fish".Length!
        public string Pitch { get; set; }

        [Required]
        [Display(Name = "Fishing Method")]
        [Range(0, Int32.MaxValue)] // TODO: This'll probably end up being something else, Guid? Enum?
        public int? FishingMethod { get; set; }

        public AdvertStatus Status { get; set; } = AdvertStatus.Creating;
    }

    public enum AdvertStatus
    {
        Creating,
        Created,
        Posted,
        Published,
        Withdrawn
    }
}