using Microsoft.AspNetCore.Mvc;
using MicroParrot.AI.Core.Services;
using MicroParrot.AI.Core.Models;

namespace MicroParrot.AI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ILlamaService _llamaService;
    private readonly ILogger<ChatController> _logger;
    private static readonly Dictionary<string, int> _rateLimits = new();
    private const int MaxRequestsPerMinute = 10;

    public ChatController(ILlamaService llamaService, ILogger<ChatController> logger)
    {
        _llamaService = llamaService;
        _logger = logger;
    }

    [HttpPost("ask")]
    public async Task<ActionResult<ChatResponse>> Ask([FromBody] ChatRequest request)
    {
        // Rate limiting
        var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        if (!CheckRateLimit(clientIp))
        {
            return StatusCode(429, new ChatResponse 
            { 
                Success = false, 
                Error = "Too many requests. Please try again later." 
            });
        }

        // Input validation
        if (string.IsNullOrEmpty(request.Message))
        {
            return BadRequest(new ChatResponse 
            { 
                Success = false, 
                Error = "Message cannot be empty" 
            });
        }

        // Content moderation (example)
        if (ContainsInappropriateContent(request.Message))
        {
            return BadRequest(new ChatResponse 
            { 
                Success = false, 
                Error = "Message contains inappropriate content" 
            });
        }

        try
        {
            var response = await _llamaService.GenerateResponseAsync(request);
            
            // Add response metadata
            response.Metadata ??= new Dictionary<string, object>();
            response.Metadata["timestamp"] = DateTime.UtcNow;
            response.Metadata["requestId"] = Guid.NewGuid().ToString();

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chat request");
            return StatusCode(500, new ChatResponse 
            { 
                Success = false, 
                Error = "An error occurred while processing your request" 
            });
        }
    }

    private bool CheckRateLimit(string clientIp)
    {
        var now = DateTime.UtcNow;
        if (!_rateLimits.ContainsKey(clientIp))
        {
            _rateLimits[clientIp] = 1;
            return true;
        }

        if (_rateLimits[clientIp] >= MaxRequestsPerMinute)
        {
            return false;
        }

        _rateLimits[clientIp]++;
        return true;
    }

    private bool ContainsInappropriateContent(string message)
    {
        // Example implementation
        var bannedWords = new[] { "inappropriate1", "inappropriate2" };
        return bannedWords.Any(word => 
            message.Contains(word, StringComparison.OrdinalIgnoreCase));
    }
} 