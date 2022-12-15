using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    [Table("commands")]
    public class CommandUpdateDto
    {
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
