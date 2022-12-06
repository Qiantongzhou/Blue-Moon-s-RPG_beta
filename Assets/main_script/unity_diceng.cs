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
    
    //[MenuItem("BlueMoon_test/script/Find missing scripts in project")]
    //static void FindMssing()
    //{
    //    string[] prefebpaths = AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefeb", System.StringComparison.OrdinalIgnoreCase)).ToArray();

    //    foreach (string path in prefebpaths)
    //    {
    //        GameObject prefeb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    //        foreach (Component comp in prefeb.GetComponentsInChildren<Component>())
    //        {
    //            if (comp == null)
    //            {
    //                Debug.Log("Prefab found with missing script" + path, prefeb);
    //                break;
    //            }
    //        }
    //    }
    //}

    //[MenuItem("BlueMoon_test/script/Find missing scripts in scene")]
    //static void Findmissing1()
    //{
    //    foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
    //    {
    //        foreach (Component comp in gameObject.GetComponentsInChildren<Component>())
    //        {
    //            if (comp == null)
    //            {
    //                Debug.Log("found missing: " + gameObject.name, gameObject);
    //                break;
    //            }
    //        }
    //    }
    //}
    //[MenuItem("BlueMoon_test/BLUEMOON_NPC_FAT_DRAGON_RED")]
    //static void generate_NPC()
    //{
    //    Instantiate(NPC_GEN[0], NPC_POS[Mathf.FloorToInt(Random.Range(0, 4))].transform.position, Quaternion.identity);
    //}
    //[MenuItem("BlueMoon_test/BLUEMOON_NPC_Arachnid")]
    //static void generate_NPC1()
    //{
    //    Instantiate(NPC_GEN[1], NPC_POS[Mathf.FloorToInt(Random.Range(0, 4))].transform.position, Quaternion.identity);
    //}
    //[MenuItem("BlueMoon_test/BLUEMOON_NPC_Rhino")]
    //static void generate_NPC2()
    //{
    //    Instantiate(NPC_GEN[2], NPC_POS[Mathf.FloorToInt(Random.Range(0, 4))].transform.position, Quaternion.identity);
    //}
    //[MenuItem("BlueMoon_test/cheat/player_add_attack_10")]
    //static void addattack()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.attackdamage += 10;
    //}
    //[MenuItem("BlueMoon_test/cheat/player_add_health_10")]
    //static void addhealth()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.healthpoint += 10;
    //}
    //[MenuItem("BlueMoon_test/cheat/player_add_health_regen_2")]
    //static void addhealthregen()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.healthregen += 2;
    //}


    ////houqi 
    //[MenuItem("BlueMoon_test/cheat/player_add_attack_100")]
    //static void addattack100()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.attackdamage += 100;
    //}
    //[MenuItem("BlueMoon_test/cheat/player_add_health_100")]
    //static void addhealth100()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.healthpoint += 100;
    //}
    //[MenuItem("BlueMoon_test/cheat/player_add_health_regen_20")]
    //static void addhealthregen20()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.healthregen += 20;
    //}
    //[MenuItem("BlueMoon_test/cheat/player_add_attackspeed")]
    //static void addattackspeed()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.aplayer.attr.attackspeed +=1;
    //}
    //[MenuItem("BlueMoon_test/enemylvlup1")]
    //static void enemymultiper()
    //{
    //    DamageCalculator.multiPerEnemy += 1;
    //}

    //[MenuItem("BlueMoon_test/projectiletest/random")]
    //static void randomprojectile()
    //{
    //    character x = GameObject.FindWithTag("Player").GetComponent<character>();
    //    x.setprojectile(Random.Range(0, 15));
    //}

}
