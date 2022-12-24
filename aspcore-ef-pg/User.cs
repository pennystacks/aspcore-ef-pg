using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace aspcore_ef_pg;

public class User
{
    [Column("id")]
    public int? Id { get; set; }
    [Column("email")]
    public string Email { get; set;  }
    [Column("name")]
    public string Name { get; set; }

    [Column("is_admin")] public bool? IsAdmin { get; set; }
}

public class PostUserDto
{
    public string Email { get; set;  }
    public string Name { get; set; }
}