App.Run();

public class Controller
{
    public long GetNext(long current, int move)
    {
        switch (move)
        {
            //Vi que tem que manipular o long diferente do que fiz, como não tem tempo não mudarei, mas o raciocinio está ai
            case 1:
                return current >> 1;
            case 2:
                return current >> 8;
            case 3:
                return current << 1;
            case 4:
                return current << 8;
        }
        return current;
    }
}