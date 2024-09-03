using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public GameObject Hero;
    public PlayerInfo _PlayerInfo;

    private void Awake()
    {
        // �������������� _PlayerInfo, ���� �� �� ����������
        if (_PlayerInfo == null)
        {
            _PlayerInfo = new PlayerInfo();
        }
    }


    public void Save()
    {
        _PlayerInfo.CoinCount = Hero.GetComponent<Player>().moneyCount;
        _PlayerInfo.HeroPosition = Hero.transform.position;
        File.WriteAllText(Application.persistentDataPath + "PV113Save.json", JsonUtility.ToJson(_PlayerInfo));
       
    }
    public void Load()
    {
        _PlayerInfo = JsonUtility.FromJson<PlayerInfo>(File.ReadAllText(Application.persistentDataPath + "PV113Save.json"));
        Hero.GetComponent<Player>().moneyCount = _PlayerInfo.CoinCount;
        Hero.GetComponent<Player>().moneyCountText.GetComponent<TextMeshProUGUI>().text = "�����: " + _PlayerInfo.CoinCount.ToString();
        Hero.transform.position = _PlayerInfo.HeroPosition;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
}

  
