﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CountingApp.Server.DbModels
{
    public class TransactionDbModel
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string TransactionType { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal TotalAmount { get; set; }

        public string TransactionData { get; set; }
    }
}
