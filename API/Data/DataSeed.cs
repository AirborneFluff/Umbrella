namespace API.Data;

public sealed class DataSeed
{
    public static async Task EnsureCreatedAsync(DataContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }
}