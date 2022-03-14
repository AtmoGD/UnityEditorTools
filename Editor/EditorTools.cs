using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace Bearbones
{
    public class EditorTools : EditorWindow
    {
        static string[] byTypeOptions = new string[] { "Scenes", "Scripts", "Visuals", "Visuals/Animations", "Visuals/Materials", "Visuals/Models", "Visuals/Prefabs", "Visuals/Sprite", "Visuals/Audio", "Visuals/Other" };
        static int byTypeIndex = 0;
        static string byTypeName = "New";


        static string newObjectName = "New Object";

        [MenuItem("Bearbones/Editor Tools")]
        public static void ShowWindow()
        {
            GetWindow(typeof(EditorTools));
        }

        private void OnGUI() {
            GUILayout.Label("Organized by Object", EditorStyles.boldLabel);
            if (GUILayout.Button("Create Default Folders By Object"))
                CreateDefaultFoldersByObject();

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            newObjectName = EditorGUILayout.TextField ("Object name:", newObjectName);
            if(GUILayout.Button("Create new object"))
                CreateNewObject();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(35);

            GUILayout.Label("Organized by Type", EditorStyles.boldLabel);
            if (GUILayout.Button("Create Default Folders By Type"))
                CreateDefaultFoldersByType();

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Parent Folder", EditorStyles.label);
            byTypeIndex = EditorGUILayout.Popup(byTypeIndex, byTypeOptions);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            byTypeName = EditorGUILayout.TextField ("Folder name:", byTypeName);

            if(GUILayout.Button("Create new Folders"))
                CreateNewObject();

            EditorGUILayout.EndHorizontal();
        }

        [MenuItem("Tools/Create Default Folders By Type")]
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

        [MenuItem("Tools/Create Default Folders By Object"), ContextMenu("Create Default Folders By Object")]
        public static void CreateDefaultFoldersByObject()
        {
            List<string> paths = new List<string>();
            paths.Add("1 - Scenes");
            paths.Add("2 - Objects");
            paths.Add("3 - Settings");
            paths.Add("DownloadedAssets");

            CreateDirectories(paths);
            Refresh();
        }

        [MenuItem("Tools/Create new Object"), ContextMenu("Create new Object")]
        public static void CreateNewObject() 
        {
            List<string> paths = new List<string>();
            paths.Add("2 - Objects/" + newObjectName);

            paths.Add("2 - Objects/" + newObjectName + "/Prefabs");

            paths.Add("2 - Objects/" + newObjectName + "/Scripts");

            paths.Add("2 - Objects/" + newObjectName + "/Visuals");
            paths.Add("2 - Objects/" + newObjectName + "/Visuals/Models");
            paths.Add("2 - Objects/" + newObjectName + "/Visuals/Materials");
            paths.Add("2 - Objects/" + newObjectName + "/Visuals/Sprites");
            paths.Add("2 - Objects/" + newObjectName + "/Visuals/Animations");

            paths.Add("2 - Objects/" + newObjectName + "/Sound");
            paths.Add("2 - Objects/" + newObjectName + "/Sound/Music");
            paths.Add("2 - Objects/" + newObjectName + "/Sound/SFX");

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