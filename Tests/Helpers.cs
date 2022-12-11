namespace Tests
{
    internal static class Helpers
    {
        private const uint _apiCallIntervalSeconds = 4;
        private static DateTimeOffset _lastCallAt = DateTimeOffset.MinValue;

        internal static async Task DoRateLimiting()
        {
            if (_lastCallAt.AddSeconds(_apiCallIntervalSeconds) > DateTimeOffset.UtcNow)
            {
                var waitTime = _lastCallAt.AddSeconds(_apiCallIntervalSeconds) - DateTimeOffset.UtcNow;
                await Task.Delay(TimeSpan.FromSeconds(waitTime.TotalSeconds));
            }

            _lastCallAt = DateTimeOffset.UtcNow;
        }
    }
}
