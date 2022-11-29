using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
    internal class Player
    {
        public readonly string Name;
        public readonly string Nick;
        public readonly List<Piece> Pieces = new(4);
        private Random rnd = new Random();

        public Player(string name, string nick = "", int pieces = 4)
        {
            Name = name;
            if (nick != "") Nick = nick;
            else Nick = Name[0..3];
            for (int i = 0; i < pieces; i++)
            {
                Pieces.Add(new Piece(this));
            }
        }

        internal Piece Move(int squares, Dictionary<Piece, int> state)
        {
            var myPieces = new Dictionary<Piece, int>();
            foreach (var piece in state.Keys)
            {
                if (piece.Owner == this && state[piece] != 0) myPieces.Add(piece, state[piece]);
            }
            return myPieces.ElementAt(rnd.Next(myPieces.Count)).Key;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
