﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainApp.EF.CodeFirst
{
    public class UserLogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string Word { get; set; }
        public DateTime? SearchTime { get; set; }
    }
}
