﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Models
{
    public class ServiceHeader
    {
        [Key]
        public int Id { get; set; }
        public double Miles { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string Details { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:dd MM yyyy}")]
        public DateTime DateAdded { get; set; }
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
    }
}
