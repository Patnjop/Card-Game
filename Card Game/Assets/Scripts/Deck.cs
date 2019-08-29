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
        DrawHand();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCards = cards.Count;
        //if(numberOfCards==0)
        //{
        //    discardPile.ShuffleDiscard();
        //    DrawDiscard();
        //    numberOfCards = cards.Count;

        //}
    }

    public Hand hand;
    //Removes the top 5 cards from the deckand adds them to the hand(childing them to the hand object)
    public void DrawHand()
    {
        for (int c = 0; c < handSize; c++)
        {
            hand.cards.Add(cards[0]);
            cards.Remove(cards[0]);
            numberOfCards--;
        }
    }

    public void DrawDiscard()
    {
        cards.AddRange(discardPile.discardPile);
//        Debug.Log(discardPile.discardPile.Count);
//        Debug.Log(cards.Count);
        discardPile.discardPile.Clear();
    }
}
