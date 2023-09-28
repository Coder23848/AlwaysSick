using Menu.Remix.MixedUI;
using UnityEngine;

namespace AlwaysSick
{
    public class PluginOptions : OptionInterface
    {
        public static PluginOptions Instance = new();

        public static Configurable<int> IntensityMultiplier = Instance.config.Bind("IntensityMultiplier", 0, new ConfigurableInfo("The intensity of the sickness. 0 is equivalent to cycle 0, 1 is equivalent to cycle -1, 2 is equivalent to cycle -2, etc.", new ConfigAcceptableRange<int>(0, 1000)));

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[1];

            Tabs[0] = new(Instance, "Options");
            IntBoxOption(IntensityMultiplier, 100, 0, "Intensity");
        }

        private void CheckBoxOption(Configurable<bool> setting, float pos, string label)
        {
            Tabs[0].AddItems(new OpCheckBox(setting, new(50, 550 - pos * 30)) { description = setting.info.description }, new OpLabel(new Vector2(90, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
        private void SliderOption(Configurable<float> setting, int size, float pos, string label)
        {
            Tabs[0].AddItems(new OpFloatSlider(setting, new(50, 545 - pos * 30), size) { description = setting.info.description }, new OpLabel(new Vector2(60 + size, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
        private void IntBoxOption(Configurable<int> setting, int size, float pos, string label)
        {
            Tabs[0].AddItems(new OpUpdown(setting, new(50, 545 - pos * 30), 100) { description = setting.info.description }, new OpLabel(new Vector2(60 + size, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
    }
}