using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildScript : MonoBehaviour
{
    private const string VERSION = "1.0.";
    private const string DAY_CALCULATEVERSION = "01/21/2023 00:00:00";
    private const string BUILDINFO_PATH = "BuildInfo/buildinfo.txt";
    private const string BUILDINFO_FINISHVERSIONSETTING = "BuildInfo/finishversionsetting.txt";

    private const string AOS_BUILD_PATH = "Build/AOS";

    private const string CHECK_AOS_SETTING_APK = "Build/AOSSettingAPK.txt";
    private const string CHECK_AOS_SETTING_AAB = "Build/AOSSettingAAB.txt";

    private const string CHECK_BUILD = "Build/checkedBuilding.txt";

    private static string[] _str_buildInfo = null;

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

        if (isAAB)
        {
            EditorUserBuildSettings.buildAppBundle = isAAB;
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        }

        if (File.Exists(BUILDINFO_FINISHVERSIONSETTING))
            _str_buildInfo = GetVersion();
        else
            _str_buildInfo = SetVersion(form: form);

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.AeDeong.Incremental");
        PlayerSettings.bundleVersion = $"{VERSION}{_str_buildInfo[0]}";
        PlayerSettings.productName = "Incremental";

        PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(_str_buildInfo[2]);

        PlayerSettings.Android.keystoreName = "src/AeDeong.keystore";
        PlayerSettings.Android.keystorePass = "password";
        PlayerSettings.Android.keyaliasName = "aedeong";
        PlayerSettings.Android.keyaliasPass = "password";

        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);

        CheckCI();
    }

    static void BuildAOS(bool isAAB)
    {
        string filePath = CHECK_BUILD;
        StreamWriter file = File.CreateText(filePath);
        file.Close();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        string extension = isAAB == true ? ".aab" : ".apk";
        buildPlayerOptions.locationPathName = AOS_BUILD_PATH + "/" + $"{VERSION}{_str_buildInfo[0]}{_str_buildInfo[1]}" + extension;

        buildPlayerOptions.options = BuildOptions.None;
        buildPlayerOptions.scenes = GetScenes();
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Android;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
            Debug.Log("AOSBuild succeeded: " + summary.totalSize + " bytes");

        if (summary.result == BuildResult.Failed)
            Debug.Log("AOSBuild failed");
    }

    static string[] SetVersion(string form)
    {
        if (!File.Exists(BUILDINFO_PATH))
            Debug.LogError("빌드 버전 정보가 존재하지 않습니다.");

        string[] buildInfo = File.ReadAllText(BUILDINFO_PATH).Split(',');
        if (buildInfo.Length != 3)
            Debug.LogError("빌드 버전 정보의 형식이 잘못되었습니다.\n" +
                                "weekNumber,buildNumber,bundleVersionCode");

        TimeSpan timeSpan = DateTime.Now - Convert.ToDateTime(DAY_CALCULATEVERSION);
        int weekNumber = timeSpan.Days / 7;

        int buildNumber = 0;
        if (Convert.ToInt32(buildInfo[0]) == weekNumber)
            buildNumber = Convert.ToInt32(buildInfo[1]) + 1;

        int bundleVersionCode = Convert.ToInt32(buildInfo[2]);
        if (form.Equals(CHECK_AOS_SETTING_AAB))
            ++bundleVersionCode;

        File.WriteAllText(path: BUILDINFO_PATH, contents: string.Format("{0},{1},{2}", weekNumber, buildNumber, bundleVersionCode));
        buildInfo = File.ReadAllText(BUILDINFO_PATH).Split(',');

        if (form.Equals(CHECK_AOS_SETTING_AAB))
        {
            StreamWriter file = File.CreateText(BUILDINFO_FINISHVERSIONSETTING);
            file.Close();
        }

        return buildInfo;
    }

    static string[] GetVersion() => File.ReadAllText(BUILDINFO_PATH).Split(',');

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
