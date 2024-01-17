using MelonLoader;
using UnityEngine;
using RUMBLE.Utilities;

namespace LIV_Camera_Enabler
{
    public class LIVCameraStart : MelonMod
    {
        //variables
        private string currentScene = "";
        private bool sceneChanged = false;
        private bool livInitialized = false;
        private LIV.SDK.Unity.LIV playerLIV;

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
        public override void OnUpdate()
        {
            //normal updates
            base.OnUpdate();
            if (sceneChanged)
            {
                livInitialized = false;
                if ((currentScene != "") && (currentScene != "Loader"))
                {
                    if (!livInitialized)
                    {
                        try
                        {
                            playerLIV = GameObject.Find("Game Instance/Initializable/RecordingCamera").GetComponent<RecordingCamera>().GetActiveLIVComponent().liv;
                            playerLIV.enabled = true;
                            livInitialized = true;
                            MelonLogger.Msg("F10 Menu Camera Enabled");
                        }
                        catch
                        {
                            return;
                        }
                    }
                    else
                    {
                        playerLIV.enabled = true;
                        MelonLogger.Msg("F10 Menu Camera Enabled");
                    }
                }
                sceneChanged = false;
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
    }
}
