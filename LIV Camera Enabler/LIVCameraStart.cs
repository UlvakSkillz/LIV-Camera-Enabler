using MelonLoader;
using UnityEngine;
using RUMBLE.Utilities;
using System.Diagnostics;
using RUMBLE.Serialization;

namespace LIV_Camera_Enabler
{
    public class LIVCameraStart : MelonMod
    {
        //variables
        private string currentScene = "";
        private bool sceneChanged = false;
        private RecordingCamera playerLIV;

        //initializes things
        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();
        }

        public override void OnLateInitializeMelon()
        {
            base.OnLateInitializeMelon();
        }

        //run every update
        public override void OnFixedUpdate()
        {
            //normal updates
            base.OnFixedUpdate();
            if (sceneChanged)
            {
                if ((currentScene != "") && (currentScene != "Loader"))
                {
                    if (IsProcessRunning("capture"))
                    {
                        try
                        {
                            playerLIV = GameObject.Find("Game Instance/Initializable/RecordingCamera").GetComponent<RecordingCamera>();
                            playerLIV.OnModernRecordingCameraEnabledChanged(true);
                            playerLIV.SaveConfiguration();
                            sceneChanged = false;
                            MelonLogger.Msg("F10 Camera Enabled");
                        }
                        catch
                        {
                            return;
                        }
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
