using BepInEx;
using UnityEngine;

namespace AlwaysSick
{
    [BepInPlugin("com.coder23848.alwayssick", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
#pragma warning disable IDE0051 // Visual Studio is whiny
        private void OnEnable()
#pragma warning restore IDE0051
        {
            // Plugin startup logic
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;

            On.Player.ctor += Player_ctor;
            On.Player.Update += Player_Update;
        }

        private void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
        {
            if (self.slugcatStats.name != SlugcatStats.Name.Red && self.redsIllness != null)
            {
                self.redsIllness.Update(); // Vanilla only calls this if you're Hunter, so it needs to be called manually if you aren't.
            }
            orig(self, eu);
        }

        private void Player_ctor(On.Player.orig_ctor orig, Player self, AbstractCreature abstractCreature, World world)
        {
            orig(self, abstractCreature, world);
            if (!self.playerState.isGhost &&
                !(self.redsIllness != null && self.redsIllness.cycle > PluginOptions.IntensityMultiplier.Value)) // If Hunter is sick for real, don't make them healthier.
            {
                self.redsIllness = new(self, PluginOptions.IntensityMultiplier.Value);
            }
        }

        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);
            Debug.Log("Always Sick config setup: " + MachineConnector.SetRegisteredOI(PluginInfo.PLUGIN_GUID, PluginOptions.Instance));
        }
    }
}