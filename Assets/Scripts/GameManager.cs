using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EndReceptionistStuff endReceptionistStuff = default;
    [SerializeField]
    private ReceptionistStuff receptionistStuff = default;

    // Start is called before the first frame update
    void Start()
    {
        int randomGood = Random.Range(0, 4);
        endReceptionistStuff.SetGoodReceptionist(randomGood);
        receptionistStuff.SetGoodDoor(randomGood);
        Debug.Log(randomGood);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        
    }
}
