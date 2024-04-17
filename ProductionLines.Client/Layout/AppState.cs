namespace ProductionLines.Client.Layout;
public class AppState
{
    public bool open;

    public void OpenDrawer()
    {
        open = !open;
        NotifyStateChanged();
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}