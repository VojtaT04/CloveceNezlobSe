using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
    internal class Piece
    {
        public static Dictionary<Player, int> PieceNumbers = new(4);
        public readonly Player Owner;
        public readonly string Name;
        public Piece(Player owner)
        {
            Owner = owner;
            if (PieceNumbers.ContainsKey(owner)) PieceNumbers[owner]++;
            else PieceNumbers.Add(owner, 1);
            Name = owner.Nick+PieceNumbers[owner].ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
