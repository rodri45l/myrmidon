


namespace MyrmidonAPI.Models;

public partial class SessionToken
{
    public Guid TokenId { get; set; }

    public Guid UserId { get; set; }

    public DateTime ExpirationTime { get; set; }

    public virtual User User { get; set; } = null!;
}
