﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int numberOfCards;
    public List<GameObject> cards;
    bool endRound = false;
//    private static CardManager cardManager;

    void Start()
    {
        //       cardManager = FindObjectOfType<CardManager>();
        numberOfCards = cards.Count;
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetKeyDown(KeyCode.R) && endRound == false)
        { 
            endRound = true;
            RoundEnd();
        }
    }

    public DiscardPile discardPile;
    public Deck deck;
    GameObject cardToDestroy;

    IEnumerator Waiter(GameObject card)
    {
        yield return new WaitUntil(() => card.GetComponent<Card>().moving == false);
    }

    public void RoundEnd()
    {
        numberOfCards = cards.Count;
        float discardSize = discardPile.cards.Count;
        for (int i = 0; i < numberOfCards; i++)
        {
            Vector3 discardPosition = new Vector3(discardPile.transform.position.x, discardPile.transform.position.y + (0.2f*(i + discardSize)), discardPile.transform.position.z);
            GameObject currentCard = cards[0];
            StartCoroutine(Waiter(currentCard));
            currentCard.GetComponent<Card>().target = discardPosition;
            currentCard.GetComponent<Card>().moving = true;
            StartCoroutine(Waiter(currentCard));

            currentCard.transform.rotation = Quaternion.Euler(-180, 90, -90);
            currentCard.transform.parent = discardPile.gameObject.transform;


            discardPile.cards.Add(currentCard);
            cards.Remove(cards[0]);

        }
        if (deck.numberOfCards<5)
        {
            
            discardPile.ShuffleDiscard();
            deck.DrawDiscard();
        }
        deck.DrawHand();
        endRound = false;
    }


}
