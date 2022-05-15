using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Mercenary",menuName = "mercenary/newMercenary")]
public class MercenaryTable : ScriptableObject
{
    public int cost;
    public int level;
    public GameObject mercenaryObject;
    public GameObject statue;
    public void levelToMercenary()
    {
        mercenaryObject.GetComponent<Parameter>().level = this.level;
    }
}
