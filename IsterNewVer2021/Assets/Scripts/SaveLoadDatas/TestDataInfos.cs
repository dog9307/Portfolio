using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfos
{ 
    [System.Serializable]
    public class TestDataInfos
    {
        public Dictionary<string,int> ScreenSizeInfos= new Dictionary<string, int>();
        public Dictionary<string, bool> FullScreenInfos = new Dictionary<string, bool>();
    }

    [System.Serializable]
    public class TestPlayerInfos
    {

    }

    public class TestScreenInfos : MonoBehaviour
    {
    }
}
