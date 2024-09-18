using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.item_and_store
{
    public class itemdrop : MonoBehaviour
    {
        [SerializeField] private int possibleitemdropamount;
        [SerializeField] private itemdata[] possibledrop;
        private List<itemdata> droplist = new List<itemdata>(); 

        [SerializeField] private GameObject dropprefab;
        private int count = 0;
        public virtual void generateDrop()
        {
            for (int i = 0; i < possibledrop.Length; i++)
            {
                if (Random.Range(0, 100) <= possibledrop[i].dropchance)
                {
                    droplist.Add(possibledrop[i]);
                }
            }

            for (int i = 0;i < droplist.Count;i++)//如果用数量，可能超出索引
            {
                while (count <possibleitemdropamount && droplist.Count > 0)
                {
                    itemdata randomitem = droplist[Random.Range(0, droplist.Count - 1)];
                    droplist.Remove(randomitem);
                    dropitem(randomitem);
                    count++;
                }
            }


        }
        protected void dropitem(itemdata _itemdata)
        {
            GameObject newdrop = Instantiate(dropprefab, transform.position, Quaternion.identity);
            Vector2 randomvelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

            newdrop.GetComponent<itemobject>().setupitem(_itemdata, randomvelocity);
        }

    }
}