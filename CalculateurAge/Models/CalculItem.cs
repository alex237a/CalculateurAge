namespace CalculateurAge.Models;

public class CalculItem
{
    public string Nom { get; set; }
    public int Age { get; set; }
    public DateTime DateCalcul { get; set; }

    public CalculItem(string nom, int age)
    {
        Nom = nom;
        Age = age;
        DateCalcul = DateTime.Now;
    }

    public override string ToString() =>
        $"{Nom}, {Age} ans - {DateCalcul:dd/MM/yyyy HH:mm:ss}";
}