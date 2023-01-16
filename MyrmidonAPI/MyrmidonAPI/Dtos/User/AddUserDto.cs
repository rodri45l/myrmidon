namespace MyrmidonAPI.Dtos.User;

public class AddUserDto
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool Sex { get; set; } // True for female, False for male.

    public string? Gender { get; set; }
    
    public virtual string? UserName { get; set; }
    public string? Password { get; set; }
}