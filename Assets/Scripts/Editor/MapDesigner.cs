using UnityEngine;
using UnityEditor;
using System.Linq;

public class MapDesigner : EditorWindow
{

    Texture2D headerTexture;
    Texture2D createTexture;
    Texture2D editTexture;

    Color headerColor = Color.blue;
    Color createColor = Color.green;
    Color editColor = Color.yellow;

    Rect headerRect;
    Rect createRect;
    Rect editRect;

    static MapData mapData;

    public static MapData mapInfo { get { return mapData; } }


    [MenuItem("iel Puzzle/Map Designer")]
    private static void ShowWindow()
    {
        var window = GetWindow<MapDesigner>();
        window.titleContent = new GUIContent("Map Designer");
        window.Show();
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawCreateSettings();
        DrawEditSettings();
    }

    private void OnEnable()
    {
        InitTextures();
        InitData();
    }

    public static void InitData()
    {
        mapData = (MapData)ScriptableObject.CreateInstance(typeof(MapData));
    }

    void InitTextures()
    {
        headerTexture = new Texture2D(1, 1);
        headerTexture.SetPixel(0, 0, headerColor);
        headerTexture.Apply();

        createTexture = new Texture2D(1, 1);
        createTexture.SetPixel(0, 0, createColor);
        createTexture.Apply();

        editTexture = new Texture2D(1, 1);
        editTexture.SetPixel(0, 0, editColor);
        editTexture.Apply();
    }
    void DrawLayouts()
    {
        headerRect.x = 0;
        headerRect.y = 0;
        headerRect.width = Screen.width;
        headerRect.height = 50;

        createRect.x = 0;
        createRect.y = 50;
        createRect.width = Screen.width / 2f;
        createRect.height = Screen.height - 50;

        editRect.x = Screen.width / 2f;
        editRect.y = 50;
        editRect.width = Screen.width / 2f;
        editRect.height = Screen.height - 50;

        GUI.DrawTexture(headerRect, headerTexture);
        GUI.DrawTexture(createRect, createTexture);
        GUI.DrawTexture(editRect, editTexture);
    }
    void DrawHeader()
    {
        GUILayout.BeginArea(headerRect);

        GUILayout.Label("Level Designer");

        GUILayout.EndArea();
    }
    void DrawCreateSettings()
    {
        GUILayout.BeginArea(createRect);

        GUILayout.Label("Create New Level");

        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.ShowWindow(GeneralSettings.SettingsType.Create);
        }

        GUILayout.EndArea();
    }
    void DrawEditSettings()
    {
        GUILayout.BeginArea(editRect);

        GUILayout.Label("Edit Existing Level");
        if (GUILayout.Button("Edit", GUILayout.Height(40)))
        {
            GeneralSettings.ShowWindow(GeneralSettings.SettingsType.Edit);
        }

        GUILayout.EndArea();
    }


}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        Create,
        Edit
    }

    enum Plane
    {
        YZ,
        XZ
    }
    static SettingsType dataSettings;
    static GeneralSettings window;
    Plane plane;

    public static void ShowWindow(SettingsType setting)
    {
        dataSettings = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSettings)
        {
            case SettingsType.Create:
                DrawSettings((MapData)MapDesigner.mapInfo);
                break;
            case SettingsType.Edit:
                DrawSettings((MapData)MapDesigner.mapInfo);
                break;
        }
    }

    void DrawSettings(MapData mapData)
    {

        if (mapData.mapObjects == null)
        {
            mapData.size = EditorGUILayout.Vector3IntField("Map Size: ", mapData.size);
            if (GUILayout.Button("Set Map Size", GUILayout.Height(40)))
            {
                mapData.GenerateObjects();
            }
        } else {

            EditorGUILayout.LabelField("Map Size:");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("X: " +mapData.size.x);
            EditorGUILayout.LabelField("Y: " +mapData.size.y);
            EditorGUILayout.LabelField("Z: " +mapData.size.z);

            EditorGUILayout.EndHorizontal();
        }

        mapData.playerStart = EditorGUILayout.Vector3IntField("Start Position: ", mapData.playerStart);

        plane = (Plane)EditorGUILayout.EnumPopup(plane);

        if (mapData.mapObjects != null)
        {
            DrawPlane(mapData, plane);
        }
    }

    void DrawPlane(MapData mapData, Plane currentPlane)
    {
        if (currentPlane == Plane.XZ)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < mapData.size.x; x++)
            {
                GUILayout.Label("X" + x);
            }
            GUILayout.EndHorizontal();
            for (int y = 0; y < mapData.size.y; y++)
            {
                GUILayout.Label("Y Layer " + y + ":");
                for (int z = 0; z < mapData.size.z; z++)
                {
                    GUILayout.BeginHorizontal();
                    for (int x = 0; x < mapData.size.x; x++)
                    {
                        if (x == 0)
                        {
                            GUILayout.Label("Z" + z + ":");
                        }
                        var mapObject = mapData.GetMapObject(new Vector3Int(x, y, z));
                        mapObject.obj = (GameObject)EditorGUILayout.ObjectField(mapObject.obj, typeof(GameObject), false);
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
        else
        {
            for (int x = 0; x < mapData.size.x; x++)
            {
                GUILayout.Label("X" + x + ":");
                for (int y = 0; y < mapData.size.y; y++)
                {
                    GUILayout.BeginHorizontal();
                    for (int z = 0; z < mapData.size.z; z++)
                    {
                        var mapObject = mapData.GetMapObject(new Vector3Int(x, y, z));
                        mapObject.obj = (GameObject)EditorGUILayout.ObjectField(mapObject.obj, typeof(GameObject), false);
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
    }

    void SaveMapData()
    {
        string dataPath = "Assets/Resources/MapData/";
    }
}