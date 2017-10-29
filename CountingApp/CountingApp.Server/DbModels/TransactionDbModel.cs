using System;
using System.ComponentModel.DataAnnotations;

namespace CountingApp.Server.DbModels
{
    public class TransactionDbModel
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string SerializedData { get; set; }
    }
}
