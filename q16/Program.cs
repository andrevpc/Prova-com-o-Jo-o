using System.IO;
using System.Linq;
using System;
using static System.Console;
using System.Collections.Generic;

var days = getDays();
var bikes = getSharings();

// Funcoes.media_dia(bikes);
Funcoes.condicoes(bikes,days);

IEnumerable<DayInfo> getDays()
{
    StreamReader reader = new StreamReader("dayInfo.csv");
    reader.ReadLine();

    while (!reader.EndOfStream)
    {
        var data = reader.ReadLine().Split(',');
        DayInfo info = new DayInfo();

        info.Day = int.Parse(data[0]);
        info.Season = int.Parse(data[1]);
        info.IsWorkingDay = int.Parse(data[2]) == 1;
        info.Weather = int.Parse(data[3]);
        info.Temp = float.Parse(data[4].Replace('.', ','));

        yield return info;
    }

    reader.Close();
}

IEnumerable<BikeSharing> getSharings()
{
    StreamReader reader = new StreamReader("bikeSharing.csv");
    reader.ReadLine();

    while (!reader.EndOfStream)
    {
        var data = reader.ReadLine().Split(',');
        BikeSharing info = new BikeSharing();

        info.Day = int.Parse(data[0]);
        info.Casual = int.Parse(data[1]);
        info.Registered = int.Parse(data[2]);

        yield return info;
    }

    reader.Close();
}

public static class Funcoes
{
    public static void media_dia(IEnumerable<BikeSharing> bicicletas)
    {
        // 1. Qual a média de alugueis de bicicletas em todo período? Sempre 
        // considere os aluguéis casuais junto aos registrados.
        Console.WriteLine($"A média por dia é: {(bicicletas.Sum(a => a.Casual) + bicicletas.Sum(a => a.Registered))/bicicletas.Count()} alugueis de bicicleta por dia");
    }
    public static void aumento(IEnumerable<BikeSharing> bicicletas)
    {
        // 2. A empresa parece ter crescido, ou seja, aumentado os alugueis de 
        // cicletas ao longo do tempo? Utilize as médias a cada 30 dias para 
        // analisar isso. Dica: Você pode resolver isso com um GroupBy com 
        // uma divisão.
        for(int i = 1, j = 0; i < bicicletas.Count()/30; i++, j++)
        {
            Console.WriteLine($"No mês {i} média = {(bicicletas.Where(d =>30*j < d.Day && d.Day <= 30*i).Sum(b => b.Casual) + bicicletas.Where(d =>30*j < d.Day && d.Day <= 30*i).Sum(b => b.Registered))/30}");
        }

    }
    
    public static void media_aluguel (IEnumerable<BikeSharing> bicicletas, IEnumerable<DayInfo> dias)
    {
        // 4. Qual a média de aluguel de bicicletas nos dias de trabalho? E nos dias
        // que não se trabalha?
        var relacionamento = bicicletas.Join(dias,
            b => b.Day,
            d => d.Day,
            (b,d) => new 
            {
                b.Day,
                d.IsWorkingDay,
                b.Casual,
                b.Registered
            }
            );
        var trab = (relacionamento.Where(d => d.IsWorkingDay).Sum(d => d.Casual) + relacionamento.Where(d => d.IsWorkingDay).Sum(d => d.Registered))/relacionamento.Where(d => d.IsWorkingDay).Count();
        var notrab = (relacionamento.Where(d => !d.IsWorkingDay).Sum(d => d.Casual) + relacionamento.Where(d => !d.IsWorkingDay).Sum(d => d.Registered))/relacionamento.Where(d => !d.IsWorkingDay).Count();

        Console.WriteLine($"Media do aluguel de dias trabalhados: {trab}\nMedia de aluguel de dias nao trabalhados: {notrab}");

    }


    public static void condicoes (IEnumerable<BikeSharing> bicicletas, IEnumerable<DayInfo> dias)
    {
        // 3. Como a estação, condições de tempo e temperatura impactam nos 
        // resultados? Responda para os três casos separadamente.
        var relacionamento = bicicletas.Join(dias,
            b => b.Day,
            d => d.Day,
            (b,d) => new 
            {
                d.Season,
                alugueis = b.Casual + b.Registered
            }
            ).GroupBy(d => d.Season)
                .Select(d => new {
                    d.Key,
                    media = d.Average(d => d.alugueis)
                });

        foreach (var item in relacionamento)
        {
            Console.WriteLine($"Estação {item.Key} = {item.media}");
        }

        Console.WriteLine("-");


        var relacionamentoTempo = bicicletas.Join(dias,
            b => b.Day,
            d => d.Day,
            (b,d) => new 
            {
                d.Weather,
                alugueis = b.Casual + b.Registered
            }
            ).GroupBy(d => d.Weather)
                .Select(d => new {
                    d.Key,
                    media = d.Average(d => d.alugueis)
                });

        foreach (var item in relacionamentoTempo)
        {
            Console.WriteLine($"Clima {item.Key} = {item.media}");
        }



        Console.WriteLine("-");


        var relacionamentoClima = bicicletas.Join(dias,
            b => b.Day,
            d => d.Day,
            (b,d) => new 
            {
                d.Temp,
                alugueis = b.Casual + b.Registered
            }
            ).GroupBy(d => d.Temp)
                .Select(d => new {
                    d.Key,
                    media = d.Average(d => d.alugueis)
                });

        foreach (var item in relacionamentoClima)
        {
            Console.WriteLine($"Temperatura {item.Key} = {item.media}");
        }
           
    }

}


// 5. Quais são os picos, tanto de alta quanto de baixa para o aluguel de 
// bicicletas e quais eram as condições (dia de trabalho, condições do 
// tempo, etc) nesses dias

public class DayInfo
{
    public int Day { get; set; }
    public int Season { get; set; }
    public bool IsWorkingDay { get; set; }
    public int Weather { get; set; }
    public float Temp { get; set; }
}

public class BikeSharing
{
    public int Day { get; set; }
    public int Casual { get; set; }
    public int Registered { get; set; }
}