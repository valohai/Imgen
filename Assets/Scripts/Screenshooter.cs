using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshooter : MonoBehaviour
{
    private bool ssEnabled = false;
    private bool loopEnabled = false;
    private string savePath = "";

    void Start()
    {
        if (GetEnvironmentVariable("IMGEN_TAKE_SCREENSHOTS", "") == "1")
        {
            ssEnabled = true;
        }
        if (GetEnvironmentVariable("IMGEN_LOOP", "") == "1")
        {
            loopEnabled = true;
        }
        savePath = GetEnvironmentVariable(
            "IMGEN_SCREENSHOT_PATH",
            System.Environment.CurrentDirectory
        );

        Debug.Log("Take screenshots = " + ssEnabled);
        Debug.Log("Loop mode = " + loopEnabled);
        Debug.Log("Screenshot directory = " + savePath);

        if (loopEnabled)
        {
            StartCoroutine(Loop());
        }
    }

    private string GetEnvironmentVariable(string key, string defaultValue)
    {
        var envs = Environment.GetEnvironmentVariables();
        if (envs.Contains(key))
        {
            return envs[key].ToString();
        }
        return defaultValue;
    }

    private IEnumerator Loop()
    {
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Iteration " + i);
            if (ssEnabled)
            {
                CaptureNamedScreenshot();
            }
            Debug.Log("Done.");
            yield return new WaitForSeconds(1);
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void CaptureNamedScreenshot()
    {
        var name = GetFilename();
        Debug.Log("Writing screenshot: " + name);
        ScreenCapture.CaptureScreenshot(name);
    }

    private string GetFilename()
    {
        return string.Format(
            "{0}/img_{1}.png",
            savePath,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff")
        );
    }
}
