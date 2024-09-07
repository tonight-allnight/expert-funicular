using System;
using UnityEngine;

public enum Swordtype
{
    Regular,
    Bounce,
    Piercce,
    Spin
} 
public class sword_skill : skill
{
    public Swordtype swordtype = Swordtype.Regular;

    [Header("bounce info")]
    [SerializeField] private int bounceamount;
    [SerializeField] private float bouncegravity;
    [SerializeField] private float bouncespeed;

    [Header("pierce info")]
    [SerializeField] private int pierceamount;
    [SerializeField] private float piercegravity;

    [Header("spin info")]
    [SerializeField] private float hitcooldown = .35f;
    [SerializeField] private float maxtraveldistance = 7;
    [SerializeField] private float spinduration = 2;
    [SerializeField] private float spingravity;


    [Header("sword info")]
    [SerializeField] private GameObject swordprefab;
    [SerializeField] private Vector2 launchforce;
    [SerializeField] private float swordgrivity;
    [SerializeField] private float freezetimeduration;
    [SerializeField] private float returnspeed;
    private Vector2 finaldir;

    [Header("dot info")]
    [SerializeField] private int numberdots;
    [SerializeField] private float spacebetweendots;
    [SerializeField] private GameObject dotprefab;
    [SerializeField] private Transform dotsparents;

    private GameObject[] dots;

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            finaldir = new Vector2(aimdirection().normalized.x * launchforce.x, aimdirection().normalized.y * launchforce.y);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = dotsposition(i * spacebetweendots);
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        generatedots();
        setupgravity();
    }

    private void setupgravity()
    {
        switch (swordtype)
        {
            case Swordtype.Bounce:
                swordgrivity = bouncegravity;
                break;
            case Swordtype.Piercce:
                swordgrivity = piercegravity;
                break;
            case Swordtype.Spin: 
                swordgrivity = spingravity;
                break;
            default:
                break;
        }
    }

    public void createsword()
    {
        GameObject newsword = Instantiate(swordprefab, player.transform.position, player.transform.rotation);
        sword_skill_controller newswordscript = newsword.GetComponent<sword_skill_controller>();

        switch (swordtype)
        {
            case Swordtype.Bounce:
                newswordscript.setupbounce(true, bounceamount , bouncespeed);
                break;
            case Swordtype.Piercce:
                newswordscript.setuppierce(pierceamount);
                break;
            case Swordtype.Spin:
                newswordscript.setupspin(true , maxtraveldistance, spinduration , hitcooldown);
                break;
            default:
                break;
        }

        newswordscript.setupsword(finaldir, swordgrivity, player , freezetimeduration, returnspeed);
        player.assignnewsword(newsword);
        Dotsactive(false);
    }
    #region Ãé×¼º¯Êý
    public Vector2 aimdirection()
    {
        Vector2 playerposition = player.transform.position;
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseposition - playerposition;

        return direction;
    }
    public void Dotsactive(bool _isactive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isactive);
        }
    }
    private void generatedots()
    {
        dots = new GameObject[numberdots];
        for (int i = 0; i < numberdots; i++)
        {
            dots[i] = Instantiate(dotprefab, player.transform.position, Quaternion.identity, dotsparents);
            dots[i].SetActive(false);
        }
    }
    private Vector2 dotsposition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            aimdirection().normalized.x * launchforce.x,
            aimdirection().normalized.y * launchforce.y) * t + .5f * (Physics2D.gravity * swordgrivity) * (t * t);
        return position;
    }
    #endregion

}
