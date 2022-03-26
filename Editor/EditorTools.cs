using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

namespace Bearbones
{
    public class EditorTools : EditorWindow
    {
        static string[] organizeOptions = new string[] { "None", "By Type", "By Object" };
        static int organizeSelection = 0;

        static string[] byTypeOptions = new string[] { "Scenes", "Scripts", "Visuals", "Visuals/Animations", "Visuals/Materials", "Visuals/Models", "Visuals/Prefabs", "Visuals/Sprite", "Visuals/Audio", "Visuals/Other" };
        static int byTypeIndex = 0;
        static string byTypeName = "New";

        static string byObjectName = "New Object";

        static AddRequest addRequest;

        static ListRequest listRequest;


        [MenuItem("Bearbones/Editor Tools")]
        public static void ShowWindow()
        {
            GetWindow(typeof(EditorTools));
        }

        private void OnGUI()
        {
            CreateOrganizationGUI();
            CreatePackagesGUI();
        }

        private static void CreateOrganizationGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Organize", EditorStyles.boldLabel);
            organizeSelection = EditorGUILayout.Popup(organizeSelection, organizeOptions);
            EditorGUILayout.EndHorizontal();

            switch (organizeOptions[organizeSelection])
            {
                case "By Type":
                    CreateByTypeGUI();
                    break;
                case "By Object":
                    CreateByObjectGUI();
                    break;
            }
        }

        private static void CreatePackagesGUI()
        {
            GUILayout.Label("Packages", EditorStyles.boldLabel);
            if (GUILayout.Button("Add URP"))
            {
                AddPackage("com.unity.render-pipelines.universal");
            }

            if (GUILayout.Button("Add Cinemachine"))
            {
                AddPackage("com.unity.cinemachine");
            }

            if (GUILayout.Button("Add New Input System"))
            {
                AddPackage("com.unity.inputsystem");
            }

            if (GUILayout.Button("Add VFX Graph"))
            {
                AddPackage("com.unity.visualeffectgraph");
            }

            if (GUILayout.Button("Add Shader Graph"))
            {
                AddPackage("com.unity.shadergraph");
            }

            if (GUILayout.Button("Show installed packages"))
            {
                ListPackages();
            }
            
        }

        static void ListPackages()
       {
           listRequest = Client.List();    // List packages installed for the Project
           EditorApplication.update += Progress;
       }

       static void Progress()
       {
           if (listRequest.IsCompleted)
           {
               if (listRequest.Status == StatusCode.Success)
                   foreach (var package in listRequest.Result)
                       Debug.Log("Package name: " + package.name);
               else if (listRequest.Status >= StatusCode.Failure)
                   Debug.Log(listRequest.Error.message);

               EditorApplication.update -= Progress;
           }
       }

        

        private static void AddPackage(string _package) {
            addRequest = Client.Add(_package);
        }

    //     static void Progress()
    //    {
    //        if (addRequest.IsCompleted)
    //        {
    //            if (addRequest.Status == StatusCode.Success)
    //                Debug.Log("Installed: " + addRequest.Result.packageId);
    //            else if (addRequest.Status >= StatusCode.Failure)
    //                Debug.Log(addRequest.Error.message);

    //            EditorApplication.update -= Progress;
    //        }
    //    }

        private static void CreateByTypeGUI()
        {
            if (GUILayout.Button("Create Default Folders By Type"))
                CreateDefaultFoldersByType();
        }

        private static void CreateByObjectGUI()
        {
            if (GUILayout.Button("Create Default Folders By Object"))
                CreateDefaultFoldersByObject();

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            byObjectName = EditorGUILayout.TextField("Object name:", byObjectName);
            if (GUILayout.Button("Create new object"))
                CreateNewObject();
            EditorGUILayout.EndHorizontal();
        }

        public static void CreateDefaultFoldersByType()
        {
            List<string> paths = new List<string>();
            paths.Add("1 - Scenes");

            paths.Add("2 - Scripts");

            paths.Add("3 - Visuals");
            paths.Add("3 - Visuals/Prefabs");
            paths.Add("3 - Visuals/Models");
            paths.Add("3 - Visuals/Materials");
            paths.Add("3 - Visuals/Sprites");
            paths.Add("3 - Visuals/Animations");

            paths.Add("4 - Sound");
            paths.Add("4 - Sound/Music");
            paths.Add("4 - Sound/SFX");

            paths.Add("5 - Settings");
            paths.Add("6 - Downloaded Assets");

            CreateDirectories(paths);
            Refresh();
        }

        public static void CreateDefaultFoldersByObject()
        {
            List<string> paths = new List<string>();
            paths.Add("1 - Scenes");
            paths.Add("2 - Objects");
            paths.Add("3 - Settings");
            paths.Add("Plugins");
            paths.Add("DownloadedAssets");

            CreateDirectories(paths);
            Refresh();
        }

        public static void CreateNewObject()
        {
            List<string> paths = new List<string>();
            paths.Add("2 - Objects/" + byObjectName);

            paths.Add("2 - Objects/" + byObjectName + "/Prefabs");

            paths.Add("2 - Objects/" + byObjectName + "/Scripts");

            paths.Add("2 - Objects/" + byObjectName + "/Visuals");
            paths.Add("2 - Objects/" + byObjectName + "/Visuals/Models");
            paths.Add("2 - Objects/" + byObjectName + "/Visuals/Materials");
            paths.Add("2 - Objects/" + byObjectName + "/Visuals/Sprites");
            paths.Add("2 - Objects/" + byObjectName + "/Visuals/Animations");

            paths.Add("2 - Objects/" + byObjectName + "/Sound");
            paths.Add("2 - Objects/" + byObjectName + "/Sound/Music");
            paths.Add("2 - Objects/" + byObjectName + "/Sound/SFX");

            CreateDirectories(paths);
            Refresh();
        }

        public static void CreateDirectories(List<string> _paths)
        {
            foreach (string path in _paths)
            {
                CreateDirectory(Combine(dataPath, path));
            }
        }
    }
}