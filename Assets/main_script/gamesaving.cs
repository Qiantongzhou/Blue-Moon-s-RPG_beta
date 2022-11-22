using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

class datamodel
{
    public string gems { get; set; }
    public string key2 { get; set; }
    public string key3 { get; set; }
    public datamodel(string gems, string key2, string key3)
    {
        this.gems = gems;
        this.key2 = key2;
        this.key3 = key3;
    }
}
public class gamesaving : MonoBehaviour
{
    // Start is called before the first frame update
    datamodel m;
    string jsonpath = "GameSaving.json";
    void Start()
    {

       
       
       
     if (!File.Exists(jsonpath))
        {
            
            using (var st = new StreamWriter(jsonpath, true))
            {
                st.WriteLine(JsonConvert.SerializeObject(new datamodel("0","0","0")).ToString());
                st.Close();
            }
        }
        if (File.Exists(jsonpath))
        {
            StreamReader r = new StreamReader("GameSaving.json");
            string jsonString = r.ReadToEnd();
            m = JsonConvert.DeserializeObject<datamodel>(jsonString);
        }
        else
        {
            print("gamesaving system error");
        }



    }


    public string getGameSavingGems()
    {
        return m.gems;
    }
    public void setGameSavingGems(int gems)
    {
        m.gems=gems.ToString();
        using (var st = new StreamWriter(jsonpath, true))
        {
            st.WriteLine(JsonConvert.SerializeObject(m).ToString());
            st.Close();
        }
    }
}
