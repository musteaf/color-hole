using System.Collections.Generic;
using System.IO;
using Script.Generator.Model;
using UnityEngine;

namespace Script.Core
{
    public static class ResourceSaveAndLoad
    {
        public static Levels DeSerializeLevels(string path)
        {
            var levelsTextAsset = Resources.Load<TextAsset>(path);
            var levels =  JsonUtility.FromJson<Levels>(levelsTextAsset.text);
            if(levels.levelList == null)
                levels.levelList = new List<Level>();
            return levels;
        }
        
        public static void SerializeLevel(Level level, string path)
        {
            var levels = DeSerializeLevels(path);
            levels.levelList.Add(level);
            var levelsJson = JsonUtility.ToJson(levels);
            File.WriteAllText(Application.dataPath + "/Resources/"+ path + ".json", levelsJson);
        }
    }
}