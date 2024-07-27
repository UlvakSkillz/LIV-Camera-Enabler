using MelonLoader;
using Il2CppRUMBLE.Utilities;
using System.Diagnostics;

namespace LIV_Camera_Enabler
{
    public class LIVCameraStart : MelonMod
    {
        //variables
        private string currentScene = "";
        private bool sceneChanged = false;
        private RecordingCamera playerLIV;

        //run every update
        public override void OnFixedUpdate()
        {
            if (sceneChanged)
            {
                if ((currentScene != "") && (currentScene != "Loader"))
                {
                    if (IsProcessRunning("capture"))
                    {
                        try
                        {
                            playerLIV = RecordingCamera.instance;
                            playerLIV.OnModernRecordingCameraEnabledChanged(true);
                            playerLIV.SaveConfiguration();
                            sceneChanged = false;
                        }
                        catch { return; }
                    }
                    else
                    {
                        sceneChanged = false;
                    }
                }
            }
        }

        //called when a scene is loaded
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);
            //update current scene
            currentScene = sceneName;
            sceneChanged = true;
        }

        //returns true/false if a process is running
        static bool IsProcessRunning(string processName)
        {
            //returns true if 1+ processes of the name was found
            return Process.GetProcessesByName(processName).Length > 0;
        }
    }
}
