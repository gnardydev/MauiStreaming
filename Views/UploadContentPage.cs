using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Net.Http.Json;
using Microcharts;
using SkiaSharp;
namespace MauiAppStreaming.Views
{
    public partial class UploadContentPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public UploadContentPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7089/api/") };

            SelectFileButton.Clicked += async (s, e) =>
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    await DisplayAlert("Arquivo Selecionado", result.FullPath, "OK");
                }
            };

            UploadButton.Clicked += async (s, e) =>
            {
                var conteudo = new
                {
                    Titulo = TitleEntry.Text,
                    Descricao = DescriptionEditor.Text
                };

                var response = await _httpClient.PostAsJsonAsync("conteudo", conteudo);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sucesso", "Conteúdo enviado com sucesso!", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", "Não foi possível enviar o conteúdo.", "OK");
                }
            };
        }
    }
}
