using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GSI.WebApi.DB
{
    [Table("Employees")]
    public partial class Employee
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public string? Department { get; set; }

        public DateTime? AccountExpiresDate { get; set; }

        public DateTime? LastLogonDate { get; set; }
    }
}
