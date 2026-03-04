using System;

namespace Peli_olio
{
    // Rajapinta: kaikki pelimuodot/tasot joita voidaan "pelata"
    public interface IPlayableMode
    {
        string Name { get; }
        int RecommendedPlayers { get; }
        void Start();
        void Pause();
        void End();
    }

    // Luokka: Yksinpeli (Story Mode)
    public class StoryMode : IPlayableMode
    {
        public string Name { get; }
        public int RecommendedPlayers => 1;

        public string Chapter { get; private set; }
        public int Difficulty { get; private set; } // 1..5

        private bool _running;

        public StoryMode(string name, string startingChapter, int difficulty)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Chapter = startingChapter ?? throw new ArgumentNullException(nameof(startingChapter));
            Difficulty = Math.Clamp(difficulty, 1, 5);
        }

        public void Start()
        {
            _running = true;
            Console.WriteLine($"🎮 Aloitetaan {Name} | Luku: {Chapter} | Vaikeus: {Difficulty}/5");
        }

        public void Pause()
        {
            if (!_running)
            {
                Console.WriteLine("⏸️ Ei voi pauselle: tila ei ole käynnissä.");
                return;
            }

            _running = false;
            Console.WriteLine($"⏸️ Tauko: {Name} (Luku: {Chapter})");
        }

        public void End()
        {
            _running = false;
            Console.WriteLine($"🏁 Lopetettu: {Name} | Edistyminen tallennettu (Luku: {Chapter}).");
        }

        public void SetChapter(string chapter)
        {
            Chapter = chapter ?? throw new ArgumentNullException(nameof(chapter));
        }
    }

    // Luokka: Moninpeli (Multiplayer)
    public class MultiplayerMode : IPlayableMode
    {
        public string Name { get; }
        public int RecommendedPlayers { get; }

        public string Map { get; private set; }
        public bool FriendlyFire { get; private set; }

        private bool _running;

        public MultiplayerMode(string name, int recommendedPlayers, string map, bool friendlyFire)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            RecommendedPlayers = Math.Max(2, recommendedPlayers);
            Map = map ?? throw new ArgumentNullException(nameof(map));
            FriendlyFire = friendlyFire;
        }

        public void Start()
        {
            _running = true;
            Console.WriteLine($"🕹️ Käynnissä: {Name} | Pelaajia: ~{RecommendedPlayers} | Kartta: {Map} | Friendly fire: {(FriendlyFire ? "ON" : "OFF")}");
        }

        public void Pause()
        {
            if (!_running)
            {
                Console.WriteLine("⏸️ Ei voi pauselle: moninpeli ei ole käynnissä.");
                return;
            }

            _running = false;
            Console.WriteLine($"⏸️ Moninpeli pauselle: {Name} ({Map})");
        }

        public void End()
        {
            _running = false;
            Console.WriteLine($"🏁 Peli päättyi: {Name} | Tilastot lähetetty palvelimelle.");
        }

        public void SetMap(string map)
        {
            Map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public void ToggleFriendlyFire()
        {
            FriendlyFire = !FriendlyFire;
        }
    }

    // Esimerkkikäyttö: sama rajapinta toimii eri pelimuodoille
    public static class Program
    {
        public static void Main()
        {
            IPlayableMode[] modes =
            {
                new StoryMode("Tarina", "Prologi", difficulty: 3),
                new MultiplayerMode("Arena", recommendedPlayers: 6, map: "Neon City", friendlyFire: false)
            };

            foreach (var mode in modes)
            {
                mode.Start();
                mode.Pause();
                mode.Start(); // jatketaan
                mode.End();
                Console.WriteLine();
            }
        }
    }
}
