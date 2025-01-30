using MicroParrot.AI.Core.Services;
using MicroParrot.AI.Infrastructure.LLM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Ollama service
builder.Services.Configure<OllamaConfig>(builder.Configuration.GetSection("Ollama"));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ILlamaService, OllamaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
