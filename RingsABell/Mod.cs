using GDWeave;
using RingsABell.Patches;

namespace RingsABell;

public class Mod : IMod {
    public Mod(IModInterface modInterface) {
        modInterface.RegisterScriptMod(new PlayerInitPatch());
        modInterface.RegisterScriptMod(new StepPatch());
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
