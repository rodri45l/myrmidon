namespace MyrmidonAPI.Dtos;

/*var moods = _dbContext.Moods.Where(m => m.UserId == userId)
            .Select(m => new MoodDTO
            {
                MoodId = m.MoodId,
                Rating = m.Rating,
                Date = m.Date,
                UserId = m.UserId
            })
            .ToList();*/
public class MoodDTO
{
    public int MoodId { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }
}