namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}
                GetOrders();
                await Task.Delay(1000, stoppingToken);
            }
        }
        private async void GetOrders()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:5178/GetOrders");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    _logger.LogInformation($"Error status {response.StatusCode} al consumir WEB API de órdenes");
                }
            }
        }
    }
}
