using MelonLoader;
using Il2CppRUMBLE.Utilities;
using RumbleModUI;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Legacy_Camera_Enabler
{
    public static class ModBuildInfo
    {
        public const string Name = "LegacyCameraEnabler";
        public const string Version = "2.1.2";
    }

    public class LegacyCameraStart : MelonMod
    {
        private string currentScene = "Loader";
        private RecordingCamera recordingCamera;
        public static Mod LegacyCameraEnabler = new Mod();
        private bool enabled = false;

        public override void OnLateInitializeMelon()
        {
            LegacyCameraEnabler.ModName = ModBuildInfo.Name;
            LegacyCameraEnabler.ModVersion = "2.1.2";
            LegacyCameraEnabler.SetFolder("LegacyCameraEnabler");
            LegacyCameraEnabler.AddToList("Enabled", true, 0, "Enable/Disable Mod", new Tags { });
            LegacyCameraEnabler.GetFromFile();
            UI.instance.UI_Initialized += UIInit;
            LegacyCameraEnabler.ModSaved += Save;
            Save();
        }

        private void Save()
        {
            enabled = (bool)LegacyCameraEnabler.Settings[0].SavedValue;
            if (currentScene != "Loader")
            {
                if (enabled)
                {
                    recordingCamera.OnLegacyRecordingCameraEnabledChanged(true);
                    recordingCamera.SaveConfiguration();
                }
                else
                {
                    recordingCamera.OnLegacyRecordingCameraEnabledChanged(false);
                    recordingCamera.SaveConfiguration();
                }
            }
        }

        private void UIInit()
        {
            UI.instance.AddMod(LegacyCameraEnabler);
        }

        public void UpdateCamera()
        {
            try
            {
                if (enabled)
                {
                    recordingCamera.OnLegacyRecordingCameraEnabledChanged(true);
                    recordingCamera.SaveConfiguration();
                }
            }
            catch { return; }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            currentScene = sceneName;
            if (currentScene == "Loader")
            {
                recordingCamera = RecordingCamera.instance;
            }
            else
            {
                UpdateCamera();
            }
        }
    }
}
