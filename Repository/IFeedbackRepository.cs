using BikeDoctor.Models;

namespace BikeDoctor.Repository;

public interface IFeedbackRepository
{
    Task<List<FeedbackResponse>> GetFeedbackResponsesAsync();
}
