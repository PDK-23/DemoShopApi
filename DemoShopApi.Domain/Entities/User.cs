using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public int Id { get; set; }    // Tự động tăng (Identity)
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public int RoleId { get; set; }      // Foreign Key
    public Role Role { get; set; }

    // Navigation property
    public ICollection<Product> Products { get; set; }
}

