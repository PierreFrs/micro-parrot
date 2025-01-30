namespace MicroParrot.AI.Core.Models;

public class ChatResponse
{
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? Error { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
} 