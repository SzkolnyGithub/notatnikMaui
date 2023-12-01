namespace projekt1Badowski4c;

public partial class MainPage : ContentPage
{
    DateTime teraz = DateTime.Now;
    double lat;
    double lon;
    string[] nazwy = new string[50];
    int j = 0;
    string nazwa = "notatka";
    string input = "";
    public MainPage()
    {
        InitializeComponent();
        GetCachedLocation();
        timestamp.Text = teraz.ToString();
        lokacja.Text = "Latitude: " + lat.ToString() + ", Longitude: " + lon.ToString();
    }
    public async Task<string> GetCachedLocation()
    {
        Location location = await Geolocation.Default.GetLocationAsync();
        if (location != null)
        {
            location.Latitude = lat;
            location.Longitude = lon;
        }
        else
        {
            lat = -1;
            lon = -1;
        }

        return "None";
    }
    private void zapisz(object sender, EventArgs e)
    {
        nazwa = "notatka";
        string data = teraz.ToString();
        string lokalizacja = lat.ToString() + ", " + lon.ToString();
        input = (tresc.Text).ToString();
        string text = "Data: " + data + ", lokalizacja: " + lokalizacja + ", podane przez użytkownika: " + input;
        nazwa = nazwa + j.ToString() + ".txt";
        nazwy[j] = nazwa;
        j++;
        //wynik.Text = nazwa.GetType().ToString();
        zapisywanie(text, nazwa);
    }
    private void zapisywanie(string text, string targetFileName)
    {
        string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
        using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        streamWriter.WriteAsync(text);
        input = "";
    } // https://learn.microsoft.com/en-us/answers/questions/991205/how-to-write-a-text-file-in-maui
    // https://www.google.com/search?client=firefox-b-e&q=.net+maui+tabbed+page+examples
    // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/tabs?view=net-maui-8.0
    // https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/geolocation?view=net-maui-8.0&tabs=android
    private void odczytaj(object sender, EventArgs e)
    {
        wynik.Text = test(nazwy[1]).ToString();
    }
    private string test(string targetFileName)
    {
        string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
        using FileStream InputStream = System.IO.File.OpenRead(targetFile);
        using StreamReader reader = new StreamReader(InputStream);
        return reader.ReadToEnd();
    }
}

