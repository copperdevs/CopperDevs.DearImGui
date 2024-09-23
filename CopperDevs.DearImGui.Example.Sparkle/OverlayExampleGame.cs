using Sparkle.CSharp;
using Sparkle.CSharp.Overlays;
using SparkleLogger = Sparkle.CSharp.Logging.Logger;

namespace CopperDevs.DearImGui.Example.Sparkle;

public class OverlayExampleGame: Game
{
    public OverlayExampleGame(GameSettings settings) : base(settings) {
        SparkleLogger.Message += Utility.CustomLog;
    }
    
    protected override void Init()
    {
        base.Init();
        
        Utility.SetWindowStyling();
        
        var myOverlay = new DearImGuiOverlay("DearImGui Overlay")
        {
            Enabled = true,
        };
        OverlayManager.Add(myOverlay);
    }
}