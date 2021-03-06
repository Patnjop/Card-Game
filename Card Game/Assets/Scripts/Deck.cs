﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<GameObject> cards;
    public int numberOfCards;
    int startingDeckSize = 20;
    int handSize = 5;
    public DiscardPile discardPile;


    void Start()
    {
        numberOfCards = cards.Count;
        numberOfCards = startingDeckSize;
        Vector3 deckPosition = this.transform.position;
        Vector3 cardPosition;
        for (int i = 0; i < numberOfCards; i++)
        {
            cardPosition = new Vector3(deckPosition.x, deckPosition.y, deckPosition.z);
            GameObject card = Instantiate(cards[i], cardPosition, Quaternion.identity).gameObject;
            card.GetComponent<CardMovement>().target = cardPosition;
            card.transform.rotation = (Quaternion.Euler(0, 90, 90));
            card.transform.parent = this.gameObject.transform;
            cards[i] = card;
        }
        StartCoroutine(DrawHand());
        //DrawHand();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCards = cards.Count;
    }

    public Hand hand;
    //Removes the top 5 cards from the deckand adds them to the hand(childing them to the hand object)
    public IEnumerator DrawHand()
    {
        while(isDrawing)
        {
            yield return new WaitForEndOfFrame();
        }
        int cardCount = cards.Count;
        for (int i = 0; i < handSize; i++)
        {
            Vector3 cardPosition = new Vector3(hand.transform.position.x + (i - 1), hand.transform.position.y, hand.transform.position.z);
            GameObject currentCard = cards[cardCount - i - 1];
            if (currentCard.GetComponent<CardMovement>().moving == false)
            {
                currentCard.GetComponent<CardMovement>().target = cardPosition;
                currentCard.GetComponent<CardMovement>().moving = true;
            }

            currentCard.GetComponent<CardMovement>().hand = true;
            currentCard.GetComponent<CardRotation>().hand = true;

            while (currentCard.GetComponent<CardMovement>().moving)
            {
                yield return new WaitForEndOfFrame();
            }

            currentCard.transform.parent = hand.gameObject.transform;

            hand.cards.Add(currentCard);
            cards.Remove(cards[cardCount - i-1]);
            numberOfCards--;
        }
    }

    public bool isDrawing = false;

    public IEnumerator DrawDiscard()
    {
        isDrawing = true;
        for (int i = 0; i < discardPile.cards.Count; i++)
        {
            Vector3 deckPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            GameObject currentCard = discardPile.cards[i];

            currentCard.GetComponent<CardMovement>().target = deckPosition;
            currentCard.GetComponent<CardMovement>().moving = true;
            currentCard.GetComponent<CardRotation>().hand = false;

            while (currentCard.GetComponent<CardMovement>().moving)
            {
                yield return new WaitForEndOfFrame();
            }
            //currentCard.transform.rotation = Quaternion.Euler(-180, 90, -90);
            currentCard.transform.parent = this.gameObject.transform;


            cards.Add(currentCard);
            numberOfCards++;
        }
        print("finished drawing discard");
        isDrawing = false;
        discardPile.cards.Clear();
    }
}
