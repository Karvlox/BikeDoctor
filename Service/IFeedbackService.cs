using BikeDoctor.Models;
using BikeDoctor.Repository;

namespace BikeDoctor.Service;

public interface IFeedbackService
{
    Task<IEnumerable<FeedbackResponse>> GetAllFeedbackAsync();
    Task<IEnumerable<FeedbackResponse>> GetFeedbackByPhoneAsync(int phoneNumber);
    Task<object> GetMetricsAsync();
    Task<object> GetPhraseMetricsAsync();
    Task<object> GetWordMetricsAsync();
}
