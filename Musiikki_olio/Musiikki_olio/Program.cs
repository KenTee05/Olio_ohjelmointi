using System;

namespace Musiikki_olio
{
    // Rajapinta: kaikki "toistettavat" toteuttavat tämän
    public interface IPlayable
    {
        string Title { get; }
        TimeSpan Duration { get; }   // kesto (voi olla 0 esim. radio)
        void Play();
        void Pause();
        void Stop();
    }

    // Luokka: Kappale
    public class Song : IPlayable
    {
        public string Title { get; }
        public string Artist { get; }
        public string Album { get; }
        public TimeSpan Duration { get; }

        private bool _isPlaying;

        public Song(string title, string artist, string album, TimeSpan duration)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Artist = artist ?? throw new ArgumentNullException(nameof(artist));
            Album = album ?? throw new ArgumentNullException(nameof(album));
            Duration = duration;
        }

        public void Play()
        {
            _isPlaying = true;
            Console.WriteLine($"▶️ Soitetaan: {Artist} – {Title} ({Album}) [{Duration:mm\\:ss}]");
        }

        public void Pause()
        {
            if (!_isPlaying)
            {
                Console.WriteLine("⏸️ Ei voi pausea: kappaletta ei soiteta.");
                return;
            }

            _isPlaying = false;
            Console.WriteLine($"⏸️ Tauko: {Artist} – {Title}");
        }

        public void Stop()
        {
            _isPlaying = false;
            Console.WriteLine($"⏹️ Pysäytetty: {Artist} – {Title}");
        }

        public override string ToString() => $"{Artist} – {Title}";
    }

    // Pieni demo: sama rajapinta toimii eri toteutuksille
    public class RadioStation : IPlayable
    {
        public string Title { get; }
        public TimeSpan Duration => TimeSpan.Zero; // ei “kestoa” samalla tavalla
        public string Frequency { get; }

        public RadioStation(string title, string frequency)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency));
        }

        public void Play() => Console.WriteLine($"📻 Kuunnellaan radioasemaa: {Title} ({Frequency})");
        public void Pause() => Console.WriteLine($"⏸️ Radio pauselle: {Title}");
        public void Stop() => Console.WriteLine($"⏹️ Radio pois: {Title}");
    }

    // Esimerkkikäyttö
    public static class Program
    {
        public static void Main()
        {
            IPlayable[] queue =
            {
                new Song("Lose Yourself", "Eminem", "8 Mile", TimeSpan.FromSeconds(326)),
                new RadioStation("YleX", "93.7 MHz"),
                new Song("Sandstorm", "Darude", "Before the Storm", TimeSpan.FromSeconds(225))
            };

            foreach (var item in queue)
            {
                item.Play();
                item.Pause();
                item.Stop();
                Console.WriteLine();
            }
        }
    }
}
