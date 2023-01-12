namespace MyrmidonAPI.Entities;

public class AddLoginRequest
{

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool Sex { get; set; } // True for female, False for male.

    public string? Gender { get; set; }

    public string? Password { get; set; }
    
    
}