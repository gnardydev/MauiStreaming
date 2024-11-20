using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microcharts;
using SkiaSharp;
using System.Net.Http;
using System.Net.Http.Json;

namespace MauiAppStreaming.Views
{
    public partial class MetricsPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public MetricsPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7089/api/") };
            LoadMetrics();
        }

        private async void LoadMetrics()
        {
            var metrics = await _httpClient.GetFromJsonAsync<List<Metric>>("metrics");
            var entries = metrics.ConvertAll(m => new ChartEntry(m.Valor)
            {
                Label = m.Nome,
                ValueLabel = m.Valor.ToString(),
                Color = SKColor.Parse(m.CorHex)
            });

            MetricsChart.Chart = new BarChart { Entries = entries };
        }

        public class Metric
        {
            public string Nome { get; set; }
            public float Valor { get; set; }
            public string CorHex { get; set; }
        }
    }
}
