using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using Newtonsoft.Json;

namespace NotatnikBadowski4cDziala;
public partial class NewPage1 : ContentPage
{
    public class Notatka
    {
        List<Notatka> nazwy = new List<Notatka>();
        public string nazwa { get; set; }
        public Notatka() { }
        public Notatka(string nazwa)
        {
            this.nazwa = nazwa;
        }
    }
    public ObservableCollection<Notatka> Notes { get; set; }
    public NewPage1()
    {
        InitializeComponent();
        Notes = new ObservableCollection<Notatka>();
        BindingContext = this;
}
    public void dodaj(object sender, EventArgs e)
    {
        Notes.Clear();
        string path = Path.Combine(FileSystem.Current.AppDataDirectory, "nazwy.json");
        string jsonString = File.ReadAllText(path);
        var temp = JsonConvert.DeserializeObject<List<Notatka>>(jsonString);
        int iter = 0;
        while(iter < temp.Count)
        {
            Notes.Add(new Notatka { nazwa = temp[iter].nazwa });
            iter++;
        }
    }
    private void klik(object sender, SelectedItemChangedEventArgs e)
    {
        wynik.Text = odczytywanie((e.SelectedItem as Notatka).nazwa);
    }
    private string odczytywanie(string targetFileName)
    {
        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
        using FileStream InputStream = File.OpenRead(targetFile);
        using StreamReader reader = new StreamReader(InputStream);
        return reader.ReadToEnd();
    }
}