using CloveceNezlobSe;

const int games = 10000000;

var p1 = new Player("Vojta");
var p2 = new Player("Marek");
var p3 = new Player("Michal");
var p4 = new Player("David");

var winCounter = new Dictionary<Player, int> { { p1, 0 }, { p2, 0 }, { p3, 0}, { p4, 0} };

for (int i = 0; i < games; i++)
{
    winCounter[new Board(40, p1, p2, p3, p4).Play()]++;
}

foreach (var v in winCounter)
{
    Console.WriteLine($"{v.Key}: {v.Value}");
}