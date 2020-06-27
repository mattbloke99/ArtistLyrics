using ArtistLyrics.Core.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ArtistLyrics
{
    [Command(Name = "ArtistLyrics", Description = "")]
    [HelpOption("-?")]
    class Program
    {
        private static ServiceProvider _serviceProvider;
        private static IConfiguration _configuration;

        static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            ConfigureServices(serviceCollection);

            return CommandLineApplication.Execute<Program>(args);
        }

        [Option(Description = "Artist Name")]
        [Required]
        public string ArtistName { get; }

        private async Task OnExecute()
        {
            try
            {
                var musicBrainzService = GetMusicBrainzSerrvice();

                var artist = await musicBrainzService.GetArtistByNameAsync(ArtistName);

                artist.Songs = await musicBrainzService.GetSongsByIdAsync(artist.Id);

                var lyricService = GetLyricService();

                foreach (var song in artist.Songs)
                {
                    song.Lyrics = await lyricService.GetLyricsAsync(ArtistName, song.Title);

                    Log.Debug("{@song}", song);
                }

                Console.WriteLine($"Average words per song for {ArtistName} is {artist.AverageWordCount()}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        private LyricsService GetLyricService()
        {
            var client = _serviceProvider.GetService<IRestClient>();

            client.BaseUrl = new Uri(_configuration["LyricsApi:Url"]);

            return _serviceProvider.GetService<LyricsService>();
        }

        private MusicBrainzService GetMusicBrainzSerrvice()
        {
            var client = _serviceProvider.GetService<IRestClient>();

            client.BaseUrl = new Uri(_configuration["MusicBrainz:Url"]);

            return _serviceProvider.GetService<MusicBrainzService>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            _serviceProvider = services
                .AddLogging(configure => configure.AddSerilog())
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<LyricsService, LyricsService>()
                .AddSingleton<MusicBrainzService, MusicBrainzService>()
                .BuildServiceProvider();
        }
    }
}
