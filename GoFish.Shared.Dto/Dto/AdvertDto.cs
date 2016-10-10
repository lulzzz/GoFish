using System;
using System.ComponentModel.DataAnnotations;

namespace GoFish.Shared.Dto
{
    public class AdvertDto
    {
        private Guid _advertId;

        public Guid Id
        {
            get
            {
                if (_advertId == Guid.Empty)
                    return Guid.NewGuid();

                return _advertId;
            }
            set
            {
                _advertId = value;
            }
        }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int? Quantity { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double? Price { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)] // TODO: See GitHub Issue #4
        public int? AdvertiserId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)] // TODO: This'll probably end up being something else, Guid? PK from DataSource?
        public int? CatchTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLengthAttribute(200, MinimumLength = 4)] // "fish".Length!
        public string Pitch { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)] // TODO: This'll probably end up being something else, Guid? Enum?
        public int? FishingMethod { get; set; }

        public AdvertStatus Status { get; set; } = AdvertStatus.Creating;
    }

    public enum AdvertStatus
    {
        Creating,
        Created
    }
}