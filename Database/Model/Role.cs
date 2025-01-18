using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }

    }
}
