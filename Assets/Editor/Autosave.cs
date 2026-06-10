using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
    // Interval in minutes (1.0f = 1 minute)
    private static float saveInterval = 1.0f; 
    private static double nextSaveTime;

    static AutoSave()
    {
        // Initialize the timer
        nextSaveTime = EditorApplication.timeSinceStartup + (saveInterval * 60);
        
        // Add to the Editor's update loop
        EditorApplication.update += Update;
        Debug.Log("<color=green>Silent Auto-Save Active:</color> Saving every " + saveInterval + " minute(s).");
    }

    private static void Update()
    {
        // Don't save while you're actually playing/testing the game
        if (EditorApplication.isPlaying) return;

        if (EditorApplication.timeSinceStartup > nextSaveTime)
        {
            SaveProjectSilent();
            nextSaveTime = EditorApplication.timeSinceStartup + (saveInterval * 60);
        }
    }

    private static void SaveProjectSilent()
    {
        // This is the "Ctrl + S" command—no popups!
        EditorSceneManager.SaveOpenScenes();
        
        // Also save project assets like Prefabs and Materials
        AssetDatabase.SaveAssets();

        Debug.Log("<color=cyan>Auto-Save:</color> Project saved silently at " + System.DateTime.Now.ToString("HH:mm:ss"));
    }
}