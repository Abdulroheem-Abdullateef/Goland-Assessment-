
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

public class User
{
    internal IEnumerable<object> Roles;

    public int Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public ClaimsIdentity? UserName { get; internal set; }
}