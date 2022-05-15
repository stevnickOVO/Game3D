using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulidBTNContor : MonoBehaviour
{
    [SerializeField] GameObject BTNFather;

    private bool isBTMon=false;
    public void BTNon()
    {
        isBTMon = !isBTMon;
        BTNFather.SetActive(isBTMon);
        animatorControllor();
    }
    public void animatorControllor()
    {
        Animator animator;
        int Count= BTNFather.transform.childCount;

        for (int a = 0; a < Count; a++)
        {
            animator = BTNFather.transform.GetChild(a).GetComponent<Animator>();
            animator.SetBool("BTNMove", isBTMon);
        }
    }
}
