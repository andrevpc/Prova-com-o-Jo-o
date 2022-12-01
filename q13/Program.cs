using System;

double[] array = new double[]
{
    8.4, 8.6, 8.4, 7.0, 7.2, 10.0, 7.2, 9.4, 9.8
};

Console.WriteLine(mediaEspecial(array));

double mediaEspecial(double[] array)
{
    Array.Sort(array);
    if (array.Count() >= 4)
        return (array[array.Count()-1] + array[array.Count()-2] + array[0] + array[1]) / 4;
    else
        return (array.Sum()/array.Length);
}