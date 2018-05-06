using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Ericmas001.Logs.LoggingDb.Entities
{
    [Table("ExecutedCommands")]
    public class ExecutedCommand : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get => IdExecutedCommand;
            set => IdExecutedCommand = value;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdExecutedCommand { get; set; }

        public int IdServiceMethod { get; set; }
        public int IdClient { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset ExecutedTime { get; set; }

        public Guid? Session { get; set; }

        [StringLength(2000)]
        public string Parms { get; set; }

        [StringLength(1000)]
        public string RequestContentType { get; set; }

        public string RequestData { get; set; }

        [StringLength(1000)]
        public string ResponseContentType { get; set; }

        public string ResponseData { get; set; }

        [StringLength(500)]
        public string ResponseCode { get; set; }

        public virtual ServiceMethod ServiceMethod { get; set; }

        public virtual Client Client { get; set; }
    }
}
