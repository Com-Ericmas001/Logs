using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Com.Ericmas001.Logs.LoggingDb.Entities
{
    [Table("ServiceMethods")]
    public class ServiceMethod : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get => IdServiceMethod;
            set => IdServiceMethod = value;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceMethod()
        {
            ExecutedCommands = new HashSet<ExecutedCommand>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServiceMethod { get; set; }

        [Required]
        [StringLength(200)]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(100)]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(100)]
        public string MethodName { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExecutedCommand> ExecutedCommands { get; set; }
    }
}
