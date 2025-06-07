using System.ComponentModel.DataAnnotations; // Nếu có
using System.ComponentModel.DataAnnotations.Schema;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Không cho tự tăng
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}
