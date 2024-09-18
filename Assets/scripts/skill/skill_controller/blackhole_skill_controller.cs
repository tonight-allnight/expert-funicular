using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhole_skill_controller : MonoBehaviour
{
    [SerializeField] private GameObject hotkeyprefab;
    [SerializeField] private List<KeyCode> keycodelist;


    public float maxsize;
    public float growspeed;
    public float shrinkspeed;

    private bool cangrow = true;
    private bool canshrink;


    private bool cancreatehotket = true;

    private bool canattack;
    private int amountofattack = 4;
    private float clonecooldown = 2;
    private float clonetimer;
    private bool playercandisappear = true;

    private float blackholeduration;

    public bool playercanexitstate { get; private set; }

    private List<Transform> target = new List<Transform>() ;
    private List<GameObject> createkey = new List<GameObject>() ;

    public void setupblackhole(float _maxsize , float _growspeed,float _shrinkspeed , int _amountofattack , float _clonecooldown , float _blackholeduartion)
    {
        maxsize = _maxsize;
        growspeed = _growspeed;
        shrinkspeed = _shrinkspeed; 
        amountofattack  = _amountofattack;
        clonecooldown = _clonecooldown;
        blackholeduration = _blackholeduartion;
        if (skillmanager.instance.clone.crystalinsteaedofclone)
            playercandisappear = false;
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        clonetimer -= Time.deltaTime;
        blackholeduration -= Time.deltaTime;

        if(blackholeduration < 0)
        {
            blackholeduration = Mathf.Infinity;
            if(target.Count < 0)
            {
                releasecloneattack();
            }
            else
            {
                finishblackholeability();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            releasecloneattack();
        }

        cloneattacklogic();

        if (cangrow && !canshrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxsize, maxsize), growspeed * Time.deltaTime);
        }
        if (canshrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkspeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }


    }

    private void releasecloneattack()
    {
        if(target.Count <= 0)
        {
            return;
        }

        destoryhotkey();
        canattack = true;
        cancreatehotket = false;
        if (playercandisappear)
        {
            playercandisappear = false;
            playermanager.instance.player.fx.maketransparent(true);
        }
    }

    private void cloneattacklogic()
    {
        if (clonetimer < 0 && canattack && amountofattack > 0)
        {
            clonetimer = clonecooldown;

            int randomindex = Random.Range(0, target.Count);
            float facing = playermanager.instance.player.facingdir;

            float xoffset;//我这里根据面朝方向控制偏移量
            if (facing == 1)
            {
                xoffset = -2;
            }
            else
            {
                xoffset = 2;
            }

            if (skillmanager.instance.clone.crystalinsteaedofclone)
            {
                skillmanager.instance.crystal.createclonecrystal();
                skillmanager.instance.crystal.currentcrystalchooserandomtarget();
            }
            else
            {
                skillmanager.instance.clone.createclone(target[randomindex], facing, new Vector3(xoffset, 0));

            }

            amountofattack--;

            if (amountofattack <= 0)
            {
                //finishblackholeability();
                Invoke("finishblackholeability" , 1f);
            }
        }
    }

    private void finishblackholeability()
    {
        destoryhotkey();
        playercanexitstate = true;
        canshrink = true;
        canattack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<enemy>() != null)
        {
            collision.GetComponent<enemy>().Freezetimer(true);
            createhotkey(collision);
            //newhotkey.GetComponent<bkhotkey_controller>().setuphotkey();
            //target.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<enemy>() != null)
        {
            collision.GetComponent<enemy>().Freezetimer(false);
        }
    }

    private void destoryhotkey()
    {
        if(createkey.Count <= 0)
        {
            return;
        }
        for(int i = 0; i < createkey.Count; i++)
        {
            Destroy(createkey[i]);
        }
    }

    private void createhotkey(Collider2D collision)
    {
        if(keycodelist.Count <= 0)
        {
            Debug.LogWarning("无健");
            return;
        }
        if (!cancreatehotket)
            return;

        GameObject newhotkey = Instantiate(hotkeyprefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createkey.Add(newhotkey);
        
        KeyCode choosenkey = keycodelist[Random.Range(0, keycodelist.Count)];
        keycodelist.Remove(choosenkey);
        bkhotkey_controller newhotkeyscript = newhotkey.GetComponent<bkhotkey_controller>();
        newhotkeyscript.setuphotkey(choosenkey, collision.transform, this);
    }

    public void addenemytolist(Transform _enemytransform) => target.Add(_enemytransform);
}
