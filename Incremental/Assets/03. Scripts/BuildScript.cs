using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;


public class BuildScript : MonoBehaviour
{
    private static string AOS_BUILD_PATH = "Build/AOS";

    private const string CHECK_AOS_SETTING_APK = "Build/AOSSettingAPK.txt";
    private const string CHECK_AOS_SETTING_AAB = "Build/AOSSettingAAB.txt";

    private const string CHECK_BUILD = "Build/checkedBuilding.txt";

    [MenuItem("Build/AOS/APK")]
    static void BuildAOSAPK() => SetAOS(form: CHECK_AOS_SETTING_APK);
    [MenuItem("Build/AOS/AAB")]
    static void BuildAOSAAB() => SetAOS(form: CHECK_AOS_SETTING_AAB);

    static void SetAOS(string form)
    {
        StreamWriter file = File.CreateText(form);
        file.Close();

        bool isAAB = form.Equals(CHECK_AOS_SETTING_AAB) ? true : false;

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.buildAppBundle = isAAB;

        CheckCI();
    }

    static void BuildAOS(bool isAAB)
    {
        string filePath = CHECK_BUILD;
        StreamWriter file = File.CreateText(filePath);
        file.Close();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        string extension = isAAB == true ? ".aab" : ".apk";
        buildPlayerOptions.locationPathName = AOS_BUILD_PATH + "/" + "TODO - VERSION" + extension;

        buildPlayerOptions.options = BuildOptions.None;
        buildPlayerOptions.scenes = GetScenes();
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Android;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    private static string[] GetScenes()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        List<string> sceneList = new List<string>();

        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.enabled)
                sceneList.Add(scene.path);
        }

        return sceneList.ToArray();
    }

    static IEnumerator CheckCompiling()
    {
        while (EditorApplication.isCompiling || EditorApplication.isUpdating)
            yield return null;

        EditorApplication.delayCall += () =>
        {
            if (File.Exists(CHECK_AOS_SETTING_APK))
            {
                File.Delete(CHECK_AOS_SETTING_APK);
                BuildScript.BuildAOS(isAAB: false);
            }

            if (File.Exists(CHECK_AOS_SETTING_AAB))
            {
                File.Delete(CHECK_AOS_SETTING_AAB);
                BuildScript.BuildAOS(isAAB: false);
            }
        };
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void CheckCI()
    {
        if (File.Exists(CHECK_AOS_SETTING_APK) || File.Exists(CHECK_AOS_SETTING_AAB))
            EditorCoroutine.StartCoroutine(CheckCompiling());
    }

    public int callbackOrder { get { return 0; } }
    public void OnPostprocessBuild(BuildReport report)
    {
        if (File.Exists(CHECK_BUILD))
        {
            File.Delete(CHECK_BUILD);

            EditorApplication.delayCall += () => { EditorApplication.Exit(0); };
        }
    }
}

class EditorCoroutine
{
    private IEnumerator iEnumerator = null;

    private EditorCoroutine(IEnumerator iEnumerator)
    {
        this.iEnumerator = iEnumerator;
    }

    public static EditorCoroutine StartCoroutine(IEnumerator iEnumerator)
    {
        EditorCoroutine editorCoroutine = new EditorCoroutine(iEnumerator);
        editorCoroutine.Start();

        return editorCoroutine;
    }

    private void Start()
    {
        EditorApplication.update -= Update;
        EditorApplication.update += Update;
    }

    public void Stop() => EditorApplication.update -= Update;

    private void Update()
    {
        if (!iEnumerator.MoveNext())
            Stop();
    }
}
