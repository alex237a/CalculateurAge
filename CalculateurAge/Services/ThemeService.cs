namespace CalculateurAge.Services;

public interface IThemeService
{
    void SetDarkTheme(bool isDark);
    bool IsDarkTheme { get; }
}

public class ThemeService : IThemeService
{
    private bool _isDarkTheme;

    public bool IsDarkTheme => _isDarkTheme;

    public void SetDarkTheme(bool isDark)
    {
        _isDarkTheme = isDark;
        Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
    }
}