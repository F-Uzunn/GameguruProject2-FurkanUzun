using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<GameObject> collectableParticleObjectList;
    public List<GameObject> comboParticleObjectList;

    public ParticleSystem goldParticle;
    public ParticleSystem starParticle;
    public ParticleSystem diamondParticle;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnParticlePlay, OnParticlePlay);
        EventManager.AddHandler(GameEvent.OnPerfectTiming, OnPerfectTiming);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnParticlePlay, OnParticlePlay);
        EventManager.RemoveHandler(GameEvent.OnPerfectTiming, OnPerfectTiming);
    }

    private void OnPerfectTiming(object val1,object val2)
    {
        OnParticlePlay("Combo", MovingCube.CurrentCube.transform.position);
    }

    private void OnParticlePlay(object particleName, object position)
    {
        string particle = (string)particleName;
        Vector3 pos = (Vector3)position;
        ParticleSystem particleObj;
        switch (particle)
        {
            case "Star":
                particleObj = Instantiate(starParticle, pos, Quaternion.identity);
                Destroy(particleObj.gameObject, 2f);
                particleObj.Play();
                break;
            case "Diamond":
                particleObj = Instantiate(diamondParticle, pos, Quaternion.identity);
                Destroy(particleObj.gameObject, 2f);
                particleObj.Play();
                break;
            case "Gold":
                particleObj = Instantiate(goldParticle, pos, Quaternion.identity);
                Destroy(particleObj.gameObject, 2f);
                particleObj.Play();
                break;
            case "Combo":
                particleObj = Instantiate(comboParticleObjectList[UnityEngine.Random.Range(0, comboParticleObjectList.Count)].GetComponent<ParticleSystem>(), pos, Quaternion.Euler(-90,0,0));
                particleObj.transform.position += new Vector3(0, 0.5f, -1.5f);
                //Destroy(particleObj.gameObject, 2f);
                particleObj.Play();
                break;
            default:
                break;
        }
    }

    public GameObject OnGetParticleObject()
    {
        GameObject particleObj = collectableParticleObjectList[UnityEngine.Random.Range(0, collectableParticleObjectList.Count)];
        return particleObj;
    }
}
