using UnityEngine;
using System.Collections;
using UnityEditor;

public class CommandBuild
{

    private static string EnName = "Remove Bomb";

    private static string ZhName = "拆弹专家";

    private static string[] s_levels = { "Assets/Scenes/LoadingScene.unity", "Assets/Scenes/MainScene.unity", "Assets/Scenes/GameScene.unity" };

    [MenuItem("Build/BuildReleaseEn")]
    public static void BuildReleaseEn()
    {
        SetKeyStore();
        PlayerSettings.productName = EnName;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "Release;EN");
        BuildPipeline.BuildPlayer(s_levels, "E:\\Build\\android\\removebomb_release_en.apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Build/BuildReleaseZh")]
    public static void BuildReleaseZh()
    {
        SetKeyStore();
        PlayerSettings.productName = ZhName;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "Release");
        BuildPipeline.BuildPlayer(s_levels, "E:\\Build\\android\\removebomb_release_zh.apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Build/BuildDebugEn")]
    public static void BuildDebugZh()
    {
        SetKeyStore();
        PlayerSettings.productName = ZhName;
        BuildPipeline.BuildPlayer(s_levels, "E:\\Build\\android\\removebomb_zh_debug.apk", BuildTarget.Android, BuildOptions.None);
    }

    private static void SetKeyStore()
    {
        PlayerSettings.Android.keystorePass = "ybw6816110";
        PlayerSettings.Android.keyaliasName = "topstudio";
        PlayerSettings.Android.keyaliasPass = "ybw6816110";
    }

}
