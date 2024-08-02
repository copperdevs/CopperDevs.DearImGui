namespace CopperDevs.DearImGui;

public interface IImGuiRenderer
{
    public void Setup();
    public void Begin();
    public void End();
    public void Shutdown();
}