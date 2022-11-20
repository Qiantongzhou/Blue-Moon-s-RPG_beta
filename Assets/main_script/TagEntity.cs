using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class TagEntity : MonoBehaviour
{
    private TagCompiler cp;

    public GameTag[] tags;

    public CreatureTag creatureTags;
    void Awake()
    {
        print("1");
        cp = GameObject.Find("TagCompiler").GetComponent<TagCompiler>();
        cp.AddressingTags(gameObject, tags);
        
    }

    public GameTag[] GetGameTags()
    {
        return tags;
    }

    public GameTag[] UpdateTags(GameTag NewTag)
    {
        GameTag[] NewTags = new GameTag[1];
        NewTags[0] = NewTag;

        return UpdateTags(NewTags);
    }
    public GameTag[] UpdateTags(GameTag[] NewTags)
    {
        GameTag[] ResultTags = new GameTag[tags.Length + NewTags.Length];
        tags.CopyTo(ResultTags,0);
        NewTags.CopyTo(ResultTags, tags.Length);
        tags = ResultTags;
        return GetGameTags();
    }


    public void RearrangeScript()
    {
        cp.AddressingTags(gameObject, tags);
    }

    public void UpdateAttr()
    {
        cp.AddressingAttr(gameObject, creatureTags);
    }
}
