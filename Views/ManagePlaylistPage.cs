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
    public partial class ManagePlaylistsPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public ManagePlaylistsPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7089/api/") };
            LoadPlaylists();
        }

        private async void LoadPlaylists()
        {
            var playlists = await _httpClient.GetFromJsonAsync<List<Playlist>>("playlist");
            PlaylistsCollection.ItemsSource = playlists;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var playlistId = (int)button.CommandParameter;
            await DisplayAlert("Editar", $"Editar playlist {playlistId}", "OK");
            // Aqui você pode redirecionar para outra tela ou implementar a edição.
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var playlistId = (int)button.CommandParameter;
            var response = await _httpClient.DeleteAsync($"playlist/{playlistId}");
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Playlist excluída!", "OK");
                LoadPlaylists(); // Recarregar as playlists.
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível excluir a playlist.", "OK");
            }
        }
    }

    public class Playlist
    {
        public int ID { get; set; }
        public string Nome { get; set; }
    }
}
