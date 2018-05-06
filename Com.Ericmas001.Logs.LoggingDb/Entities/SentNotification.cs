using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Ericmas001.Logs.LoggingDb.Entities
{
    [Table("SentNotifications")]
    public class SentNotification : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get => IdSentNotification;
            set => IdSentNotification = value;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSentNotification { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset SentTime { get; set; }

        [Required]
        public bool Success { get; set; }

        [StringLength(1000)]
        [Required]
        public string Topic { get; set; }

        [StringLength(1000)]
        [Required]
        public string Title { get; set; }

        [StringLength(1000)]
        [Required]
        public string Message { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public string Error { get; set; }
    }
}
