using System.Linq;
using System.Collections.Generic;

App.Run();

public class Controller
{
    public void Solve(IEnumerable<Piece> pieces)
    {
        var lt = pieces.First(p => p.IsLeftTopPiece());
        lt.SetPosition(0,0);
        int cx = 1;
        foreach (var item in pieces)
        {
            if (item.ConnectLeft(lt))
            {
                item.SetPosition(cx,0);
                cx++;
                lt = item;
            }
        }
    }
}