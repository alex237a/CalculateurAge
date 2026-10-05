using System.Collections.ObjectModel;
using CalculateurAge.Models;
using CalculateurAge.Services;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private readonly IThemeService _themeService;

    // Contient l'ÉTAT de l'écran et les ACTIONS possibles.
    // Champs privés : la vraie donnée.
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _joursRestants = "";
    private bool _isDarkTheme;

    // Propriétés publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value))
                  CalculerCommand.Rafraichir(); }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set => SetField(ref _isDarkTheme, value);
    }

    // Historique observable
    public ObservableCollection<CalculItem> Historique { get; } = new();

    // Commandes
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }
    public RelayCommand BasculerThemeCommand { get; }
    public RelayCommand ViderHistoriqueCommand { get; }

    public CalculateurViewModel()
    {
        _themeService = new ThemeService();

        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));

        EffacerCommand = new RelayCommand(Effacer);
        BasculerThemeCommand = new RelayCommand(BasculerTheme);
        ViderHistoriqueCommand = new RelayCommand(ViderHistorique);
    }

    // La logique métier : calcul d'âge + historique + jours restants
    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        ResultatVisible = true;

        // Calcul des jours restants avant anniversaire
        var prochainAnniv = DateNaissance.AddYears(age + 1);
        var joursRestants = (prochainAnniv - DateTime.Today).Days;
        JoursRestants = $"Jours avant anniversaire : {joursRestants}";

        // Ajouter à l'historique
        Historique.Insert(0, new CalculItem(Nom, age));
    }

    // Effacer tous les champs
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        ResultatVisible = false;
        JoursRestants = "";
        IsDarkTheme = false;
        _themeService.SetDarkTheme(false);
    }

    // Basculer entre thèmes clair/sombre
    private void BasculerTheme()
    {
        IsDarkTheme = !IsDarkTheme;
        _themeService.SetDarkTheme(IsDarkTheme);
    }

    // Vider l'historique
    private void ViderHistorique()
    {
        Historique.Clear();
    }
}