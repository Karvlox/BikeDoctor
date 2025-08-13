using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BikeDoctor.Controller;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<FeedbackResponse>>> GetAllFeedback()
    {
        var feedback = await _feedbackService.GetAllFeedbackAsync();
        return Ok(feedback);
    }

    [HttpGet("{phoneNumber}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<FeedbackResponse>>> GetFeedbackByPhone(int phoneNumber)
    {
        var feedback = await _feedbackService.GetFeedbackByPhoneAsync(phoneNumber);
        if (!feedback.Any())
            return NotFound();
        return Ok(feedback);
    }

    [HttpGet("metrics")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetMetrics()
    {
        var metrics = await _feedbackService.GetMetricsAsync();
        return Ok(metrics);
    }

    [HttpGet("metrics/phrases")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetPhraseMetrics()
    {
        var metrics = await _feedbackService.GetPhraseMetricsAsync();
        return Ok(metrics);
    }

    [HttpGet("metrics/words")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetWordMetrics()
    {
        var metrics = await _feedbackService.GetWordMetricsAsync();
        return Ok(metrics);
    }
}