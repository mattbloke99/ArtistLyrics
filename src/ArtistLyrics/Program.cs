using ArtistLyrics.Core.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

            //setting up logging
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

        public async Task OnExecute()
        {
            try
            {
                var musicBrainzService = GetMusicBrainzService();

                var artist = await musicBrainzService.GetArtistByNameAsync(ArtistName);

                Console.WriteLine($"Found artist: {artist.Name}");

                artist.Songs = await musicBrainzService.GetSongsByIdAsync(artist.Id);

                Console.WriteLine($"Found {artist.Songs.Count()} songs for artist {artist.Name}");

                var lyricService = GetLyricService();

                foreach (var song in artist.Songs)
                {
                    song.Lyrics = await lyricService.GetLyricsAsync(ArtistName, song.Title);

                    Log.Debug("{@song}", song);

                    Console.WriteLine($"Found lyrics with {song.LyricCount()} words for song {song.Title}");
                }

                Console.WriteLine($"Average words per song for {artist.Name} is {artist.AverageWordCount()}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error was encountered whilst executing the application.");
                throw;
            }
        }

        private LyricsService GetLyricService()
        {
            var client = _serviceProvider.GetService<IRestClient>();

            client.BaseUrl = new Uri(_configuration["LyricsApi:Url"]);

            return _serviceProvider.GetService<LyricsService>();
        }

        private MusicBrainzService GetMusicBrainzService()
        {
            var client = _serviceProvider.GetService<IRestClient>();

            client.BaseUrl = new Uri(_configuration["MusicBrainz:Url"]);

            return _serviceProvider.GetService<MusicBrainzService>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //settings up services for dependecy injection
            _serviceProvider = services
                .AddLogging(configure => configure.AddSerilog())
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<LyricsService, LyricsService>()
                .AddSingleton<MusicBrainzService, MusicBrainzService>()
                .BuildServiceProvider();
        }
    }
}
