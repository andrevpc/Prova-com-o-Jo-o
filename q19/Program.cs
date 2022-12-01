using System;

Vetor v = (1, 2);
Vetor u = (4, 3);
Vetor r = (5, 5);

Console.WriteLine($"{v} + {u} = {r}?");

if (v + u == r)
    Console.WriteLine("Sim!");
else Console.WriteLine("Não! (mas devia)");


public class Vetor
{
    public int x { get; set; }
    public int y { get; set; }

    public Vetor(int[] dados)
    {
        this.x = dados[0];
        this.y = dados[1];
    }

    public override string ToString() => $"{this.x}, {this.y}";

    public static implicit operator Vetor((int, int) tupla)
        => new Vetor(new int[] {
            tupla.Item1, 
            tupla.Item2
        });

    public static implicit operator Vetor((int, int, int) tupla)
        => new Vetor(new int[] {
            tupla.Item1, 
            tupla.Item2,
            tupla.Item3
        });

    public static Vetor operator +(Vetor v, Vetor u)
        {
            var nx = v.x+u.x;
            var ny = v.y+u.y;
            return (nx, ny);
        }

    public static bool operator ==(Vetor v, Vetor u)
        {
            if(v.x == u.x && v.y == u.y)
                return true;
            else
                return false;
        }

    public static bool operator !=(Vetor v, Vetor u)
        => !(v == u);
}
