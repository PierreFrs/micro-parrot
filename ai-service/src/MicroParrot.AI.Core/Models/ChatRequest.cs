namespace MicroParrot.AI.Core.Models;

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
    public string? Context { get; set; }
    public Dictionary<string, string>? Parameters { get; set; }
} 