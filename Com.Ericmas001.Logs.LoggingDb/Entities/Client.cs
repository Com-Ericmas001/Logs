using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Com.Ericmas001.Logs.LoggingDb.Entities
{
    [Table("Clients")]
    public class Client : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get => IdClient;
            set => IdClient = value;
        }

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public Client()
        {
            ExecutedCommands = new HashSet<ExecutedCommand>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClient { get; set; }

        [Required]
        [StringLength(100)]
        public string IpAddress { get; set; }

        [Required]
        [StringLength(4000)]
        public string UserAgent { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExecutedCommand> ExecutedCommands { get; set; }
    }
}
