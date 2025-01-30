namespace MicroParrot.AI.Infrastructure.LLM;

public class OllamaConfig
{
    public string BaseUrl { get; set; } = "http://localhost:11434";
    public string ModelName { get; set; } = "deepseek-r1";
    public float Temperature { get; set; } = 0.8f;
} 