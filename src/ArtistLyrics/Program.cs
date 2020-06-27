using ArtistLyrics.Core.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            args = new[] { "-a Queen" };

            var serviceCollection = new ServiceCollection();

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
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
            var musicBrainzService = GetMusicBrainzSerrvice();

            var artist = await musicBrainzService.GetArtistByNameAsync(ArtistName);

            artist.Songs = await  musicBrainzService.GetSongsByIdAsync(artist.Id);

            var lyricService = GetLyricService();

            foreach (var song in artist.Songs)
            {
                song.Lyrics = await lyricService.GetLyricsAsync(ArtistName, song.Title);

                Log.Debug("{@song}", song);
            }

            Console.WriteLine($"Average lyrics per song for {ArtistName} is {artist.AverageLyricCount()}");
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
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<LyricsService, LyricsService>()
                .AddSingleton<MusicBrainzService, MusicBrainzService>()
                .BuildServiceProvider();
        }
    }
}
