using System.Text.Json;
using System.Threading;
using Microsoft.Maui.Controls;

namespace NotatnikBadowski4cDziala
{
    public partial class MainPage : ContentPage
    {
        /*private void timer()
        {
            var timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => Update();
            timer.Start();
        }
        private void Update()
        {
            DateTime teraz = DateTime.Now;
            timestamp.Text = teraz.ToString();
        }*/
        List<Notatka> nazwy = new List<Notatka>();
        double lat;
        double lon;
        string input = "";
        class Notatka
        {
            public string nazwa { get; set; }
            public Notatka(string nazwa)
            {
                this.nazwa = nazwa;
            }
        }
        public MainPage()
        {
            InitializeComponent();
            location();
            //timer();
        }
        private async Task<string> location()
        {
            Location location = await Geolocation.Default.GetLocationAsync();
            lat = location.Latitude;
            lon = location.Longitude;
            //lokacja.Text = $"Latitude: {lat}, Longitude: {lon}";
            return "none";
        }
        private void zapisz(object sender, EventArgs e)
        {
            ZrobJson(nazwy);
        }
        private void ZrobJson(List<Notatka> DoSerializacji)
        {
            string path = Path.Combine(FileSystem.Current.AppDataDirectory, "nazwy.json");
            File.Delete(path);
            using FileStream file = File.OpenWrite(path);
            JsonSerializer.Serialize(file, DoSerializacji);
            info2.Text = "Notatki zapisano pomyślnie!";
            file.Close();
            /*using FileStream test = File.OpenRead(path);
            info2.Text = File.ReadAllText(path);*/
        }
        private async void zapisywanie(string text, string targetFileName)
        {
            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
            using FileStream outputStream = File.OpenWrite(targetFile);
            using StreamWriter streamWriter = new StreamWriter(outputStream);
            await streamWriter.WriteAsync(text);
            info3.Text += $"Plik {targetFileName} dodano pomyślnie!\n";
            tresc.Text = "";
        }
        private void dodaj(object sender, EventArgs e)
        {
            long unixtime1 = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds(); // to dziala bardzo dobrze
            string nazwaPliku = unixtime1.ToString() + ".txt";
            nazwy.Add(new Notatka(nazwaPliku));
            string data = DateTime.Now.ToString();
            string lokalizacja = lat.ToString() + ", " + lon.ToString();
            input = (tresc.Text).ToString();
            string text = $"Data: {data}, lokalizacja: {lokalizacja}, podane przez uzytkownika: {input}";
            zapisywanie(text, nazwaPliku);
        }
    }
}