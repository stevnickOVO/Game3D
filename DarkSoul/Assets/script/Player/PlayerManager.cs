using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private int moneyPay=1;
    [SerializeField] private MercenaryTable[] MercenaryList;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private GameObject startBuildPoint;
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private GameObject AttackUI;
    [SerializeField] private GameObject BuildUI;
    private GameObject player;
    public GameObject currBuild;
    public static PlayerManager playerManagerInstance;
    public int totleMoney;
    private Rigidbody rigidbody;
    private int currIndex;
    float moneyTime;
    private void Awake()
    {
        totleMoney = 200;
        moneyText.text = totleMoney.ToString();
        playerManagerInstance = this;
        rigidbody = GetComponent<Rigidbody>();
        player = camera.Follow.gameObject;
    }
    private void Update()
    {
        timePlusMoney();

        buildTime();
        moneyText.text = totleMoney.ToString();
    }
    public void getMoney(int money)
    {
        totleMoney += money;
    }
    public void timePlusMoney()
    {
        if (gameManager.gameManagerInstance.isFight)
        {
            moneyTime += Time.deltaTime;
            if (moneyTime >= 1)
            {
                totleMoney += moneyPay;
                moneyTime = 0;
            }
        }
    }
    public void buildTime()
    {
        if (currBuild != null)
        {
            cameraControllor();
            AttackUI.SetActive(false);
            BuildUI.SetActive(true);

            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;

            rigidbody.MovePosition(transform.position+new Vector3(horizontal, 0, vertical)*speed*Time.deltaTime);

            currBuild.transform.position = transform.position;
        }
    }
    public void buildBTN(int index)
    {
        if (MercenaryList[index].cost <= totleMoney)
        {
            if (currBuild != null)
            {
                Destroy(currBuild.gameObject);
            }
            transform.position = startBuildPoint.transform.position;
            currBuild = Instantiate(MercenaryList[index].statue, transform.position, transform.rotation);

            currIndex = index;
        }
    }
    public void cameraControllor()
    {
        

        camera.Follow = currBuild.transform;
        camera.LookAt = currBuild.transform;
    }
    public void buildBTNSure()
    {
        Destroy(currBuild.gameObject);
        currBuild = null;

        totleMoney -= MercenaryList[currIndex].cost;
        Instantiate(MercenaryList[currIndex].mercenaryObject, transform.position, transform.rotation);

        camera.Follow = player.transform;
        camera.LookAt = player.transform;

        AttackUI.SetActive(true);
        BuildUI.SetActive(false);
    }
    public void buildBTNexit()
    {
        Destroy(currBuild.gameObject);
        currBuild = null;

        camera.Follow = player.transform;
        camera.LookAt = player.transform;

        AttackUI.SetActive(true);
        BuildUI.SetActive(false);
    }
}
