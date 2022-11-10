using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class unity_diceng : MonoBehaviour
{
    public static GameObject[] NPC_GEN;
    public static GameObject[] NPC_POS;
    
    [MenuItem("BlueMoon_test/script/Find missing scripts in project")]
    static void FindMssing()
    {
        string[] prefebpaths = AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefeb", System.StringComparison.OrdinalIgnoreCase)).ToArray();

        foreach (string path in prefebpaths)
        {
            GameObject prefeb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            foreach (Component comp in prefeb.GetComponentsInChildren<Component>())
            {
                if (comp == null)
                {
                    Debug.Log("Prefab found with missing script" + path, prefeb);
                    break;
                }
            }
        }
    }

    [MenuItem("BlueMoon_test/script/Find missing scripts in scene")]
    static void Findmissing1()
    {
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            foreach (Component comp in gameObject.GetComponentsInChildren<Component>())
            {
                if (comp == null)
                {
                    Debug.Log("found missing: " + gameObject.name, gameObject);
                    break;
                }
            }
        }
    }
    [MenuItem("BlueMoon_test/BLUEMOON_NPC_FAT_DRAGON_RED")]
    static void generate_NPC()
    {
        Instantiate(NPC_GEN[0], NPC_POS[Mathf.FloorToInt(Random.Range(0, 4))].transform.position, Quaternion.identity);
    }

}
