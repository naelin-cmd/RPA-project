using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("PlatformBotTable")]
    public partial class PlatformBotTable
    {
        [Key]
        public int BotId { get; set; }
        public int PlatformId { get; set; }

        [ForeignKey(nameof(PlatformId))]
        [InverseProperty(nameof(PlatformTable.PlatformBotTables))]
        public virtual PlatformTable Platform { get; set; }
        [InverseProperty("Bot")]
        public virtual BotsTable BotsTable { get; set; }
    }
}
