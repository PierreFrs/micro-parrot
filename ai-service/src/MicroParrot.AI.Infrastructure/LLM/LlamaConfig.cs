namespace MicroParrot.AI.Infrastructure.LLM;

public class LlamaConfig
{
    public string ModelPath { get; set; } = string.Empty;
    public int MaxContextLength { get; set; } = 2048;
    public int MaxTokens { get; set; } = 256;
    public float Temperature { get; set; } = 0.8f;
} 