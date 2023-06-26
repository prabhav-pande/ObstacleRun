using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class localPlayerCheck : MonoBehaviourPun
{
    private bool isMine;
    private Camera m_Camera;
    private PhotonView myPhotonView;
    ParticleSystem exp;
    Transform myPLayer;

    

    int totalHealth = 10;
    int currentHealth;

    public Text healthText;
    public GameObject particle;
    private void Start()
    {
        currentHealth = totalHealth;
        isMine = false;
        m_Camera = Camera.main;
        myPhotonView = GetComponent<PhotonView>();
        exp = particle.GetComponent<ParticleSystem>();
        myPLayer = GetComponent<Transform>();
        //healthText = GetComponent<Text>();
        if (!myPhotonView.IsMine)
        {
            //Destroy(m_Camera);
            m_Camera.enabled = false;
        }
    }
    void Update()
    {

        if (photonView.IsMine)
        {
            isMine = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Trap")
        {
            PhotonView.Instantiate(exp, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
            gameObject.GetComponent<PhotonView>().RPC("RefreshHealth", RpcTarget.All);
            gameObject.GetComponent<PhotonView>().RPC("TP", RpcTarget.All);
            //PhotonView.Destroy(particle);
        }
    }
    [PunRPC]
    void RefreshHealth()
    {
        currentHealth--;
        Debug.Log("Refreshing Health "+currentHealth);
        healthText.text = "Number Of Lives: " + currentHealth;
        Debug.Log("Health Text "  + healthText.text);
    }
    public bool getIsMine()
    {
        return isMine;
    }
    [PunRPC]
    void TP()
    {
        //cc.enabled = false;
        myPLayer.position = new Vector3(0f, 0f, 0f);
        transform.position = myPLayer.position;

        if (currentHealth == 0)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        // cc.enabled = true;
    }
}
