using System;
using System.Linq;

namespace PracticeChallenge2
{

    class Program
    {
        static void Main(string[] args)
        {
            Deck newDeck = new Deck();
            //Hand hand = Load();
            Hand hand = new Hand();
            hand.fillHand(Load());
            int option = Menu();
            while (option != 5)
            {

                if (option <= 0 || option >= 6)
                {
                    Console.WriteLine("Incorrect selection");
                    option = Menu();
                }
                else if (option == 1)
                {
                    newDeck.fillDeck();
                    Console.WriteLine("Deck created");
                    option = Menu();
                }
                else if (option == 2)
                {

                    newDeck.Shuffle();
                    Console.WriteLine("Deck shuffled");
                    option = Menu();
                }
                else if (option == 3)
                {
                    hand.fillHand(newDeck);
                    Console.WriteLine("Hand selected");
                    option = Menu();
                }
                else if (option == 4)
                {
                    Console.WriteLine("Totol score of hand: " + hand.PointsValue());
                    option = Menu();
                }
            }
            if (option == 5)
            {
                Console.WriteLine("Exiting, Hand saved");
                Save(hand);
            }


            /*
            foreach (Card card in hand.getHand())
            {
                Console.WriteLine(card.name + " of " + card.suit + ", " + card.points + " points");
            }
            foreach (Card card in newDeck.getDeck())
            {
                Console.WriteLine(card.name + " of " + card.suit + ", " + card.points + " points");
            }
            Console.WriteLine(hand.PointsValue());
            */

        }

        public static int Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1. Create New Deck");
            Console.WriteLine("");
            Console.WriteLine("2. Shuffle Deck");
            Console.WriteLine("3. Deal New Hand");
            Console.WriteLine("");
            Console.WriteLine("4. Display Hand Total Points");
            Console.WriteLine("5. Exit");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("");
            Console.Write("Select an option: ");
            string input = Console.ReadLine();
            return int.Parse(input);

        }

        public static Card[] Load()
        {
            Card[] hand = new Card[5];
            if (File.Exists("./handData.csv"))
            {
                string[] lines = File.ReadAllLines("./handData.csv");
                int i = 0;
                foreach (string line in lines)
                {
                    string[] values = line.Split(",");
                    Card card = new Card(values[0], values[1], int.Parse(values[2]));
                    hand[i] = card;
                    i++;
                }
            }
            return hand;

        }

        public static void Save(Hand hand)
        {
            using (StreamWriter writer = new StreamWriter("./handData.csv"))
            {
                foreach (Card card in hand.getHand())
                {
                    writer.WriteLine(card.name + "," + card.suit + "," + card.points);
                }
            }
        }
    }

    public class Card
    {
        public string name { get; set; }
        public string suit { get; set; }
        public int points { get; set; }
        public Card(string name, string suit, int points)
        {
            this.name = name;
            this.suit = suit;
            this.points = points;
        }
    }

    public class Deck
    {
        Card[] deck = new Card[52];
        public string[] nameslist = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        public string[] suitslist = new string[] { "Hearts", "Diamonds", "Spades", "Clubs" };
        public void fillDeck()
        {

            int pos = 0;
            foreach (string suit in suitslist)
            {
                int val = 0;
                int poi = 1;
                foreach (string name in nameslist)
                {
                    if (poi > 10) poi = 10;
                    deck[pos] = new Card(name, suit, poi);
                    val++;
                    pos++;
                    poi = val + 1;
                }
            }
        }

        public Card[] Shuffle()
        {
            Random rnd = new Random();
            Card[] MyRandomArray = deck.OrderBy(x => rnd.Next()).ToArray();
            deck = MyRandomArray;
            return deck;
        }


        public Card getDeckCard(int pos)
        {
            return deck[pos];
        }

        public Card[] getDeck()
        {
            return deck;
        }

    }

    public class Hand
    {
        Card[] cards = new Card[5];

        public void fillHand(Deck deckCard)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = deckCard.getDeckCard(i);
            }
        }
        public void fillHand(Card[] hand)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = hand[i];
            }
        }
        public Card getHandCard(int pos)
        {
            return cards[pos];
        }
        public Card[] getHand()
        {
            return cards;
        }

        public int PointsValue()
        {
            int total = 0;
            foreach (Card card in cards)
            {
                total += card.points;
            }
            return total;
        }
    }


}

