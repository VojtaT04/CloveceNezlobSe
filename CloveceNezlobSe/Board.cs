using System.Text;

namespace CloveceNezlobSe
{
    internal class Board
    {
        public readonly List<Player> Players = new(4);
        public readonly Dictionary<Player, int> spawnFields = new(4);
        public readonly int _size;
        private Random rnd = new Random();
        public Piece PieceToMove;
        private int currentPlayerIndex;
        private int dieRoll;
        private bool _inProgress;
        public bool InProgress
        {
            get
            {
                return !Players.Any(pl => pl.Pieces.All(pi => Squares[pi] == 0));
            }
            set { _inProgress = value; }
        }

        public Dictionary<Piece, int> Squares = new();
        // Square -1 = not yet in play
        // Square 0 = already in finish

        public Board(int size, Player p1, Player p2, Player p3, Player p4)
        {
            // Make symmetric and sufficiently large
            size -= size % 4;
            if (size < 8) size = 8;
            _size = size;

            // Add players to the game, set their piece spawn points
            Players.Add(p1);
            Players.Add(p2);
            Players.Add(p3);
            Players.Add(p4);
            for (int i = 0; i < 4; i++)
            {
                spawnFields.Add(Players[i], i * size / 4 + 1);
            }

            // Add players' pieces to the game
            foreach (var player in Players)
            {
                foreach (var piece in player.Pieces)
                {
                    Squares.Add(piece, -1);
                }
            }

            // Start the game
            InProgress = true;
            currentPlayerIndex = rnd.Next(Players.Count);
        }

        public Player Play()
        {
            Player currentPlayer;
            while (InProgress)
            {
                currentPlayer = Players[currentPlayerIndex%Players.Count];
                dieRoll = rnd.Next(6) + 1;
                PieceToMove = currentPlayer.Move(dieRoll, Squares);
                EvaluateBoardState(dieRoll, PieceToMove);
                //Console.WriteLine(this.ToString());
                if (dieRoll == 6) currentPlayerIndex --;
                currentPlayerIndex++;
            }
            return Players.FirstOrDefault(pl => pl.Pieces.All(pi => Squares[pi] == 0));
        }

        private void EvaluateBoardState(int roll, Piece piece)
        {
            if (Squares[piece] == 0) return; // Finished pieces
            if (Squares[piece] == -1 && roll == 6)
            {
                if (Squares.ContainsValue(spawnFields[piece.Owner])) Squares[Squares.First(x => x.Value == spawnFields[piece.Owner]).Key] = -1; // Kicking
                Squares[piece] = spawnFields[piece.Owner];
                return;
            }
            else if (Squares[piece] == -1) return;// Spawning
            
            
            var temp = int.Parse(Squares[piece].ToString());
            for (int i = 0; i < roll; i++)
            {
                if (Squares.ContainsValue(Squares[piece] + 1) && i == roll-1) Squares[Squares.First(x => x.Value == Squares[piece] + 1).Key] = -1; // Kicking
                Squares[piece]++;
                if (Squares[piece] > _size) Squares[piece] = 1;
                if (Squares[piece] == spawnFields[piece.Owner])
                {
                    Squares[piece] = 0;
                    return;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Players[currentPlayerIndex % Players.Count].Name} rolled {dieRoll} and picked {PieceToMove}");
            foreach (var square in Squares)
            {
                sb.AppendLine($"{square.Key.Name}: {(square.Value)}");
            }
            return sb.ToString();
        }
    }
}
