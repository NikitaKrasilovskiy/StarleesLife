using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSmileButton : MonoBehaviour
{
    [SerializeField] private GameObject[] _fxSmile;
    private int _spawnSmileFxIndex;

    public void SpawnSmileFx()
    {
        _fxSmile[_spawnSmileFxIndex].SetActive(true);
        _spawnSmileFxIndex++;
        _fxSmile[_spawnSmileFxIndex].SetActive(true);
        _spawnSmileFxIndex++;

        if (_spawnSmileFxIndex == 14)
        {
            _spawnSmileFxIndex = 0;
        }
    }

    public void HideSmiles()
    {
        for (int i = 0; i < _fxSmile.Length; i++)
        {
            _fxSmile[i].SetActive(false);
        }
    }
}
