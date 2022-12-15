using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    [Table("commands")]
    public class Command
    {
        [PrimaryKey, Identity]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [NotNull]
        [Column("howto")]
        public string HowTo { get; set; }

        [Required]
        [NotNull]
        [Column("line")]
        public string Line { get; set; }

        [Required]
        [NotNull]
        [Column("platform")]
        public string Platform { get; set; }

    }
}
