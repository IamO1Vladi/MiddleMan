namespace MiddleMan.Common.Utilities;

public static class ApiUtilities
{
    public static async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries = 3, int delayMilliseconds = 1000)
    {
        for (int retry = 0; retry < maxRetries; retry++)
        {
            try
            {
                return await action();
            }
            catch (Exception ex) when (retry < maxRetries - 1)
            {
                // Log the exception if necessary
                Console.WriteLine($"Attempt {retry + 1} failed: {ex.Message}");
                await Task.Delay(delayMilliseconds);
            }
        }

        throw new Exception("Maximum retry attempts exceeded.");
    }

}