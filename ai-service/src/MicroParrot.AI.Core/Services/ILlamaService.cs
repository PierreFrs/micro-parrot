using MicroParrot.AI.Core.Models;

namespace MicroParrot.AI.Core.Services;

public interface ILlamaService
{
    Task<ChatResponse> GenerateResponseAsync(ChatRequest request);
} 