// See https://aka.ms/new-console-template for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Security;
using System.Xml.Serialization;

var rezEkipe = new Dictionary<string, (int Zabijeni, int Primljeni)>()
{
    { "PRVAHRV", (Zabijeni: 0, Primljeni: 0) },
    { "OSTALEPRVA", (Zabijeni: 0, Primljeni: 0) },
    { "DRUGAHRV", (Zabijeni: 0, Primljeni: 0) },
    { "OSTALEDRUGA", (Zabijeni: 0, Primljeni: 0) },
    { "TRECAHRV", (Zabijeni: 0, Primljeni: 0) },
    { "OSTALETRECA", (Zabijeni: 0, Primljeni: 0) }

};


var golRazlika = new Dictionary<string, int>()
{
    { "Hrvatska", 0},
    { "Kanada", 0},
    { "Maroko", 0},
    { "Belgija", 0}
};



var tablica = new Dictionary<string, int>()
{
    { "Hrvatska", 0},
    { "Kanada", 0}, 
    { "Maroko", 0},
    { "Belgija", 0}
};

var prvaPostava = new Dictionary<string, int>();

int brojUtakmica = 0;
var popisIgraca = new Dictionary<string, (string Position, int Rating)>()
{
    { "Luka Modric", ("MF", 88) },
    { "Marcelo Brozovic", ("MF", 86) },
    { "Mateo Kovacic", ("MF", 84) },
    { "Ivan Perisic", ("MF", 84) },
    { "Andrej Kramaric", ("FW", 82) },
    { "Ivan Rakitic", ("MF", 82) },
    { "Joško Gvardiol", ("DF", 81) },
    { "Mario Pasalic", ("MF", 81) },
    { "Lovro Majer", ("MF", 80) },
    { "Dominik Livakovic", ("GK", 80) },
    { "Ante Rebic", ("FW", 80) },
    { "Josip Brekalo", ("MF", 79) },
    { "Borna Sosa", ("DF", 78) },
    { "Duje Caleta-Car", ("DF", 78) },
    { "Nikola Vlasic", ("MF", 78) },
    { "Dejan Lovren", ("DF", 78) },
    { "Mislav Orsic", ("FW", 77) },
    { "Marko Livaja", ("FW", 77) },
    { "Domagoj Vida", ("DF", 76) },
    { "Ante Budimir", ("FW", 76) }

};






var naredba = 0;


do
{
    
    Console.Clear();
    Console.WriteLine("---------------IZBORNIK--------------- \n");

    Console.WriteLine("1 - Odradi trening");
    Console.WriteLine("2 - Odigraj utakmicu");
    Console.WriteLine("3 - Statistika");
    Console.WriteLine("4 - Kontrola igraca");
    Console.WriteLine("0 - Izlaz iz aplikacije");

    Console.Write("\nUnesi broj naredbe: ");

    //----------------------------------------------------------------------------------------------  unos start


    bool ispravan = int.TryParse(Console.ReadLine(), out naredba);
    Console.WriteLine("");
    if (!ispravan)
    {
        Console.Clear();
        Console.WriteLine("Krivi unos!!\n");

        Console.WriteLine("1 - Odradi trening");
        Console.WriteLine("2 - Odigraj utakmicu");
        Console.WriteLine("3 - Statistika");
        Console.WriteLine("4 - Kontrola igraca");
        Console.WriteLine("0 - Izlaz iz aplikacije");

        Console.Write("\nPonovo unesi broj naredbe: ");
        naredba = 100;
        continue;
    }
    //---------------------------------------------------------------------------------------------- unos end


    //---------------------------------------------------------------------------------------------- SWITCH START


    switch (naredba)
    {

        default:
            //---------------------------------------------------------------------------------------------- defaut start
            Console.Clear();
            Console.WriteLine("Krivi unos!!\n");

            Console.WriteLine("1 - Odradi trening");
            Console.WriteLine("2 - Odigraj utakmicu");
            Console.WriteLine("3 - Statistika");
            Console.WriteLine("4 - Kontrola igraca");
            Console.WriteLine("0 - Izlaz iz aplikacije");

            Console.Write("\nPonovo unesi broj naredbe: ");
            break;
        //---------------------------------------------------------------------------------------------- default end
        case 1:
            //---------------------------------------------------------------------------------------------- case TRENING start
            Console.Clear();
            Console.WriteLine("---------------TRENING--------------- \n");

            Console.WriteLine("Stari rating: ");
            foreach (var item in popisIgraca)
                Console.WriteLine($"{item.Key} - {item.Value.Rating}");

            foreach (var item in popisIgraca)
            {
                Random rnd = new Random();
                popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + rnd.Next(-5, 6));

            }

            foreach (var item in popisIgraca)
            {
                if (item.Value.Rating > 100)
                    popisIgraca[item.Key] = (item.Value.Position, 100);
            }

            foreach (var item in popisIgraca)
            {
                if (item.Value.Rating < 0)
                    popisIgraca[item.Key] = (item.Value.Position, 0);
            }

            Console.WriteLine("\nNovi rating: ");
            foreach (var item in popisIgraca)
                Console.WriteLine($"{item.Key} - {item.Value.Rating}");



            var konacno = 10;
            do
            {

                Console.WriteLine("\nOdaberi:");
                Console.WriteLine("1 - Povratak na izbornik");
                Console.WriteLine("0 - Izlazak iz aplikacije");


                string odabirCase1 = Console.ReadLine();
                if (odabirCase1 == "0")
                {
                    konacno = 0;
                    break;
                }
                else if (odabirCase1 == "1")
                {
                    konacno = 1;
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nKrivi unos!!");
                    continue;
                }


            } while (true);

            if (konacno == 0)
                return;
            else
            {
                Console.WriteLine("\n\n");
                continue;
            }

        //---------------------------------------------------------------------------------------------- case TRENING end
        case 2:
            //---------------------------------------------------------------------------------------------- case ODIGRAJ UTAKMICU start
            
             
            Console.Clear();
            Console.WriteLine("---------------ODIGRAJ UTAKMICU--------------- \n");
            prvaPostava.Clear(); 
            int brGK = 0;
            int brDF = 0;
            int brMF = 0;
            int brFW = 0;
 


            foreach (var item in popisIgraca.OrderByDescending(Key => Key.Value.Rating))
            {
                if(!prvaPostava.ContainsKey(item.Key))
                { 
                if (item.Value.Position == "GK")
                {
                    brGK++;
                    if (brGK <= 1)
                        prvaPostava.Add(item.Key, 0);
                }
                if (item.Value.Position == "DF")
                {
                    brDF++;
                    if (brDF <= 4)
                        prvaPostava.Add(item.Key, 0);
                }
                if (item.Value.Position == "MF")
                {
                    brMF++;

                    if (brMF <= 3)
                        prvaPostava.Add(item.Key, 0);
                }
                if (item.Value.Position == "FW")
                {
                    brFW++;
                    if (brFW <= 3)
                        prvaPostava.Add(item.Key, 0);
                }
                }
                
            }

            var odabirIgra = "";
            if (prvaPostava.Count() < 11)
            {
                Console.WriteLine("U postavi nema dovljno igraca");
                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-----Postava-----\n");
                foreach (var item in prvaPostava)
                {
                    Console.WriteLine($"{item.Key}");

                }
                Console.WriteLine("\n-----------------\n");



                do
                {
                    Console.WriteLine("Odaberi:");
                    Console.WriteLine("1 - Igraj!");
                    Console.WriteLine("0 - Povratak");
                    odabirIgra = Console.ReadLine();
                } while (odabirIgra != "1" && odabirIgra != "0");
            }

            if (brojUtakmica >= 3)
            {
                Console.Clear();
                Console.WriteLine("Odigran je maksimalan broj utakmica");
                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                Console.ReadLine();
                continue;
            }
                

            if (odabirIgra == "1")
            {
                //-------------------------------------------------------------------------------------------- prva igra start
                Console.Clear();
                Console.WriteLine("------------- !!UTAKMICA!! ------------- \n");
                brojUtakmica++;

                Console.WriteLine("BELGIJA - KANADA");
                Console.WriteLine("HRVATSKA - MAROKO\n");

                Console.WriteLine("Stisni bilo koji znak za rezultate...");
                Console.ReadLine();

                Random rnd = new Random();

                var belgijaGolovi = rnd.Next(0, 6);
                var hrvatskaGolovi = rnd.Next(0, 6);
                var marokoGolovi = rnd.Next(0, 6);
                var kanadaGolovi = rnd.Next(0, 6);

                golRazlika["Belgija"] += belgijaGolovi;
                golRazlika["Belgija"] -= kanadaGolovi;

                golRazlika["Hrvatska"] += hrvatskaGolovi;
                golRazlika["Hrvatska"] -= marokoGolovi;

                golRazlika["Maroko"] += marokoGolovi;
                golRazlika["Maroko"] -= hrvatskaGolovi;

                golRazlika["Kanada"] -= belgijaGolovi;
                golRazlika["Kanada"] += kanadaGolovi;

                if (belgijaGolovi > kanadaGolovi)
                {
                    tablica["Belgija"] += 3;
               
                }
                else if (belgijaGolovi < kanadaGolovi)
                {
              
                    tablica["Kanada"] += 3;
                }
                else
                {
                    tablica["Belgija"] += 1;
                    tablica["Kanada"] += 1;
                }

                if (hrvatskaGolovi > marokoGolovi)
                {
                    tablica["Hrvatska"] += 3;
  
                }
                else if (hrvatskaGolovi < marokoGolovi)
                {
         
                    tablica["Maroko"] += 3;
                }
                else
                {
                    tablica["Hrvatska"] += 1;
                    tablica["Maroko"] += 1;
                }



              
                if (hrvatskaGolovi > marokoGolovi)
                    foreach (var item in popisIgraca)
                    {
                        popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + 2);
                        if (item.Value.Rating > 100)
                            popisIgraca[item.Key] = (item.Value.Position, 100);
                    }
                if (hrvatskaGolovi < marokoGolovi)
                    foreach (var item in popisIgraca)
                    {
                        popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating - 2);
                        if (item.Value.Rating < 1)
                            popisIgraca[item.Key] = (item.Value.Position, 1);
                    }


                Console.WriteLine($"\nBELGIJA {belgijaGolovi} - {kanadaGolovi} KANADA ");
                Console.WriteLine($"HRVATSKA {hrvatskaGolovi} - {marokoGolovi} MAROKO ");

                foreach (var item in rezEkipe)
                {
                    if (item.Key == "PRVAHRV") 
                        rezEkipe[item.Key] = (item.Value.Zabijeni + hrvatskaGolovi, item.Value.Primljeni + marokoGolovi);

                    if (item.Key == "OSTALEPRVA")
                        rezEkipe[item.Key] = (item.Value.Zabijeni + belgijaGolovi, item.Value.Primljeni + kanadaGolovi);
                }
                        

                int brgolova = 0;

                List<string> keyList = new List<string>(prvaPostava.Keys);

                for (int i = 0; i < hrvatskaGolovi; i++)
                {
                    Random rand = new Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];

                    prvaPostava[randomKey] += 1;
                    brgolova++;

                }

                Console.WriteLine("\nStisni bilo koji znak za strijelce...");
                Console.ReadLine();
                Console.WriteLine("");

                if (brgolova == 0)
                    Console.WriteLine("Hrvatska je zabila 0 golova");


                foreach (var item in prvaPostava)
                {
                    if (item.Value > 0)
                        Console.WriteLine($"{item.Key} - {item.Value}");
                }

                foreach (var item in popisIgraca)
                {
                    if (prvaPostava.ContainsKey(item.Key))
                    {
                        if (prvaPostava[item.Key] > 0)
                            popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + 5);
                        if (item.Value.Rating > 100)
                            popisIgraca[item.Key] = (item.Value.Position, 100);
                    }

                }



                var odabirDrugaIgra = "";

                do
                {
                    Console.WriteLine("\nOdaberi:");
                    Console.WriteLine("1 - Sljedeca utakmica");
                    Console.WriteLine("0 - Izlaz na izbornik");
                    odabirDrugaIgra = Console.ReadLine();
                } while (odabirDrugaIgra != "1" && odabirDrugaIgra != "0");
                //-------------------------------------------------------------------------------------------- prva igra end
                //-------------------------------------------------------------------------------------------- druga igra start
               
                if (odabirDrugaIgra == "1")
                {

                    if (brojUtakmica >= 3)
                    {
                        Console.Clear();
                        Console.WriteLine("Odigran je maksimalan broj utakmica");
                        Console.WriteLine("\nPovratak(unesi bilo sto)...");
                        Console.ReadLine();
                        continue;
                    }

                    brojUtakmica++;

                    var trenutniStrijelci = new Dictionary<string, int>(prvaPostava);
                    foreach (var item in trenutniStrijelci)
                    {
                        trenutniStrijelci[item.Key] = 0;
                    }

                    Console.Clear();
                    Console.WriteLine("------------- !!UTAKMICA!! ------------- \n");

                    Console.WriteLine("BELGIJA - HRVATSKA");
                    Console.WriteLine("KANADA - MAROKO\n");

                    Console.WriteLine("Stisni bilo koji znak za rezultate...");
                    Console.ReadLine();

                    rnd = new Random();

                    belgijaGolovi = rnd.Next(0, 6);
                    hrvatskaGolovi = rnd.Next(0, 6);
                    marokoGolovi = rnd.Next(0, 6);
                    kanadaGolovi = rnd.Next(0, 6);

                    golRazlika["Belgija"] += belgijaGolovi;
                    golRazlika["Belgija"] -= hrvatskaGolovi;

                    golRazlika["Hrvatska"] += hrvatskaGolovi;
                    golRazlika["Hrvatska"] -= belgijaGolovi;

                    golRazlika["Maroko"] += marokoGolovi;
                    golRazlika["Maroko"] -= kanadaGolovi;

                    golRazlika["Kanada"] += kanadaGolovi;
                    golRazlika["Kanada"] -= marokoGolovi;

                    if (belgijaGolovi > hrvatskaGolovi)
                {
                        tablica["Belgija"] += 3;
                    }
                else if (belgijaGolovi < hrvatskaGolovi)
                    {
                        tablica["Hrvatska"] += 3;
                    }
                    else
                    {
                        tablica["Belgija"] += 1;
                        tablica["Hrvatska"] += 1;
                    }

                    if (kanadaGolovi > marokoGolovi)
                    {
                        tablica["Kanada"] += 3;
                    }
                    else if (kanadaGolovi < marokoGolovi)
                    {
                        tablica["Maroko"] += 3;
                    }
                    else
                    {
                        tablica["Kanada"] += 1;
                        tablica["Maroko"] += 1;
                    }

                    if (hrvatskaGolovi > belgijaGolovi)
                        foreach (var item in popisIgraca)
                        {
                            popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + 2);
                            if (item.Value.Rating > 100)
                                popisIgraca[item.Key] = (item.Value.Position, 100);
                        }
                    if (hrvatskaGolovi < belgijaGolovi)
                        foreach (var item in popisIgraca)
                        {
                            popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating - 2);
                            if (item.Value.Rating < 1)
                                popisIgraca[item.Key] = (item.Value.Position, 1);
                        }

                    Console.WriteLine($"\nBELGIJA {belgijaGolovi} - {hrvatskaGolovi} HRVATSKA");
                    Console.WriteLine($"KANADA {kanadaGolovi}  - {marokoGolovi} MAROKO ");

                    foreach (var item in rezEkipe)
                    {
                        if (item.Key == "DRUGAHRV")
                            rezEkipe[item.Key] = (item.Value.Zabijeni + hrvatskaGolovi, item.Value.Primljeni + belgijaGolovi);

                        if (item.Key == "OSTALEDRUGA")
                            rezEkipe[item.Key] = (item.Value.Zabijeni + kanadaGolovi, item.Value.Primljeni + marokoGolovi);
                    }


                    brgolova = 0;

                    keyList = new List<string>(prvaPostava.Keys);

                    for (int i = 0; i < hrvatskaGolovi; i++)
                    {
                        Random rand = new Random();
                        string randomKey = keyList[rand.Next(keyList.Count)];

                        prvaPostava[randomKey] += 1;
                        trenutniStrijelci[randomKey] += 1;
                        brgolova++;

                    }

                    Console.WriteLine("\nStisni bilo koji znak za ukupni broj golova stijelaca...");
                    Console.ReadLine();
                    Console.WriteLine("");

                    if (brgolova == 0)
                        Console.WriteLine("Hrvatska je zabila 0 golova");


                    foreach (var item in trenutniStrijelci)
                    {
                        if (item.Value > 0)
                            Console.WriteLine($"{item.Key} - {item.Value}");
                    }

                    foreach (var item in popisIgraca)
                    {
                        if (trenutniStrijelci.ContainsKey(item.Key))
                        {
                            if (trenutniStrijelci[item.Key] > 0)
                                popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + 5);
                            if (item.Value.Rating > 100)
                                popisIgraca[item.Key] = (item.Value.Position, 100);
                        }

                    }


                    do
                    {
                        Console.WriteLine("\nOdaberi:");
                        Console.WriteLine("1 - Sljedeca utakmica");
                        Console.WriteLine("0 - Izlaz na izbornik");
                        odabirDrugaIgra = Console.ReadLine();
                    } while (odabirDrugaIgra != "1" && odabirDrugaIgra != "0");
                    //-------------------------------------------------------------------------------------------- treca igra start
                    if (odabirDrugaIgra == "1")
                    {
                        if (brojUtakmica >= 3)
                        {
                            Console.Clear();
                            Console.WriteLine("Odigran je maksimalan broj utakmica");
                            Console.WriteLine("\nPovratak(unesi bilo sto)...");
                            Console.ReadLine();
                            continue;
                        }

                        brojUtakmica++;
                        trenutniStrijelci = new Dictionary<string, int>(prvaPostava);
                        foreach (var item in trenutniStrijelci)
                        {
                            trenutniStrijelci[item.Key] = 0;
                        }

                        Console.Clear();
                        Console.WriteLine("------------- !!TRECA UTAKMICA!! ------------- \n");

                        Console.WriteLine("BELGIJA - MAROKO");
                        Console.WriteLine("HRVATSKA - KANADA\n");

                        Console.WriteLine("Stisni bilo koji znak za rezultate...");
                        Console.ReadLine();

                        rnd = new Random();

                        belgijaGolovi = rnd.Next(0, 6);
                        hrvatskaGolovi = rnd.Next(0, 6);
                        marokoGolovi = rnd.Next(0, 6);
                        kanadaGolovi = rnd.Next(0, 6);

                        golRazlika["Belgija"] += belgijaGolovi;
                        golRazlika["Belgija"] -=  marokoGolovi;

                        golRazlika["Hrvatska"] += hrvatskaGolovi;
                        golRazlika["Hrvatska"] -= kanadaGolovi;

                        golRazlika["Maroko"] += marokoGolovi;
                        golRazlika["Maroko"] -= belgijaGolovi;

                        golRazlika["Kanada"] += kanadaGolovi;
                        golRazlika["Kanada"] -= hrvatskaGolovi;

                        if (belgijaGolovi > marokoGolovi)
                        {
                            tablica["Belgija"] += 3;
                        }
                        else if (belgijaGolovi < marokoGolovi)
                        {
                            tablica["Maroko"] += 3;
                        }
                        else
                        {
                            tablica["Belgija"] += 1;
                            tablica["Maroko"] += 1;
                        }

                        if (kanadaGolovi > hrvatskaGolovi)
                        {
                            tablica["Kanada"] += 3;
                        }
                        else if (kanadaGolovi < hrvatskaGolovi)
                        {
                            tablica["Hrvatska"] += 3;
                        }
                        else
                        {
                            tablica["Kanada"] += 1;
                            tablica["Hrvatska"] += 1;
                        }

                        if (hrvatskaGolovi > kanadaGolovi)
                            foreach (var item in popisIgraca)
                            {
                                popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + 2);
                                if (item.Value.Rating > 100)
                                    popisIgraca[item.Key] = (item.Value.Position, 100);
                            }
                        if (hrvatskaGolovi < kanadaGolovi)
                            foreach (var item in popisIgraca)
                            {
                                popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating - 2);
                                if (item.Value.Rating < 1)
                                    popisIgraca[item.Key] = (item.Value.Position, 1);
                            }

                        Console.WriteLine($"\nBELGIJA {belgijaGolovi} - {marokoGolovi} MAROKO");
                        Console.WriteLine($"HRVATSKA {hrvatskaGolovi} - {kanadaGolovi} KANADA");


                        foreach (var item in rezEkipe)
                        {
                            if (item.Key == "TRECAHRV")
                                rezEkipe[item.Key] = (item.Value.Zabijeni + hrvatskaGolovi, item.Value.Primljeni + kanadaGolovi);

                            if (item.Key == "OSTALETRECA")
                                rezEkipe[item.Key] = (item.Value.Zabijeni + belgijaGolovi, item.Value.Primljeni + marokoGolovi);
                        }


                        brgolova = 0;

                        keyList = new List<string>(prvaPostava.Keys);

                        for (int i = 0; i < hrvatskaGolovi; i++)
                        {
                            Random rand = new Random();
                            string randomKey = keyList[rand.Next(keyList.Count)];

                            prvaPostava[randomKey] += 1;
                            trenutniStrijelci[randomKey] += 1;
                            brgolova++;

                        }

                        Console.WriteLine("\nStisni bilo koji znak za ukupni broj golova stijelaca...");
                        Console.ReadLine();
                        Console.WriteLine("");

                        if (brgolova == 0)
                            Console.WriteLine("Hrvatska je zabila 0 golova");


                        foreach (var item in trenutniStrijelci)
                        {
                            if (item.Value > 0)
                                Console.WriteLine($"{item.Key} - {item.Value}");
                        }

                        foreach (var item in popisIgraca)
                        {
                            if (trenutniStrijelci.ContainsKey(item.Key))
                            {
                                if (trenutniStrijelci[item.Key] > 0)
                                    popisIgraca[item.Key] = (item.Value.Position, item.Value.Rating + 5);
                                if (item.Value.Rating > 100)
                                    popisIgraca[item.Key] = (item.Value.Position, 100);
                            }

                        }

                        Console.WriteLine("\nStisni bilo koji znak za izlazak...");
                        Console.ReadLine();
                        break;


                    }
                    //-------------------------------------------------------------------------------------------- treca igra end
                    else
                        continue;
                }
            
            //-------------------------------------------------------------------------------------------- druga igra start
            else
            {
                continue;
            }
                
            }
            else
                break;


                    
        case 3:
            //---------------------------------------------------------------------------------------------- case STATISTIKA start 

            var ispisSvihIliNe = "as";

            do
            {

                Console.Clear();
                Console.WriteLine("---------------STATISTIKA--------------- \n");
                Console.WriteLine("1 - Ispis svih igraca");
                Console.WriteLine("0 - Povratak na izbornik");

                ispisSvihIliNe = Console.ReadLine();
                var odabirCase3 = "";

                if (ispisSvihIliNe == "1")
                {


                    do
                    {
                        Console.Clear();
                        Console.WriteLine("---------------STATISTIKA--------------- ");
                        Console.WriteLine("------------ISPIS SVIH IGRACA----------- \n");

                        Console.WriteLine("1 - Ispis onako kako su spremljeni");
                        Console.WriteLine("2 - Ispis po rating uzlazno");
                        Console.WriteLine("3 - Ispis po ratingu silazno");
                        Console.WriteLine("4 - Ispis igrača po imenu i prezimenu (ispis pozicije i ratinga) ");
                        Console.WriteLine("5 - Ispis igrača po ratingu (ako ih je više ispisati sve)");
                        Console.WriteLine("6 - Ispis igrača po poziciji (ako ih je više ispisati sve)");
                        Console.WriteLine("7 - Ispis trenutnih prvih 11 igrača (na pozicijama odabrati igrače s najboljim ratingom)");
                        Console.WriteLine("8 - Ispis strijelaca i koliko golova imaju");
                        Console.WriteLine("9 - Ispis svih rezultata ekipe");
                        Console.WriteLine("10 - Ispis rezultat svih ekipa");
                        Console.WriteLine("11 - Ispis tablice grupe (mjesto na tablici, ime ekipe, broj bodova, gol razlika)");
                        Console.WriteLine("12 - Povratak");
                        Console.WriteLine("0 - Izlaz iz aplikacije");

                        Console.Write("\nUnesi broj naredbe: ");


                        odabirCase3 = Console.ReadLine();

                        konacno = 1;
                        //---------------------------------------------------------------------------------------------- SWITCH case STATISTIKA start



                        switch (odabirCase3)
                        {

                            default:
                                Console.WriteLine("Unesi ponovo: ");
                                break;
                            case "1":

                                //------------------------------------------------------------------------------------------------- Ispis case 1 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("------------Ispis onako kako su spremljeni-----------\n");

                                foreach (var item in popisIgraca)
                                    Console.WriteLine($"{item.Key} - {item.Value.Position} - {item.Value.Rating}");

                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 1 end
                                break;
                            case "2":
                                //------------------------------------------------------------------------------------------------- Ispis case 2 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("--------------Ispis po rating uzlazno---------------\n");

                                foreach (var item in popisIgraca.OrderBy(Key => Key.Value.Rating))
                                    Console.WriteLine($"{item.Key}");
                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 2 end
                                break;
                            case "3":
                                //------------------------------------------------------------------------------------------------- Ispis case 3 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("--------------Ispis po rating silazno---------------\n");

                                foreach (var item in popisIgraca.OrderByDescending(Key => Key.Value.Rating))
                                    Console.WriteLine($"{item.Key}");
                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 3 end
                                break;
                            case "4":
                                //------------------------------------------------------------------------------------------------- Ispis case 4 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("---------Ispis igrača po imenu i prezimenu----------\n");

                                foreach (var item in popisIgraca.OrderBy(Key => Key.Key))
                                    Console.WriteLine($"{item.Key} - {item.Value.Position} - {item.Value.Rating} ");
                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 4 end
                                break;
                            case "5":
                                //------------------------------------------------------------------------------------------------- Ispis case 5 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("--------------Ispis igrača po ratingu---------------\n");

                                foreach (var item in popisIgraca.OrderBy(Key => Key.Value.Rating))
                                    Console.WriteLine($"{item.Key} - {item.Value.Position} - {item.Value.Rating}");
                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 5 end
                                break;
                            case "6":
                                //------------------------------------------------------------------------------------------------- Ispis case 6 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("--------------Ispis igrača po poziciji--------------\n");

                                foreach (var item in popisIgraca.OrderBy(Key => Key.Value.Position))
                                    Console.WriteLine($"{item.Key} - {item.Value.Position} - {item.Value.Rating}");
                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 6 end
                                break;
                            case "7":
                                //------------------------------------------------------------------------------------------------- Ispis case 7 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("---Ispis trenutnih prvih 11 igrača(na pozicijama)---\n");

                                 brGK = 0;
                                 brDF = 0;
                                 brMF = 0;
                                 brFW = 0;



                                foreach (var item in popisIgraca.OrderByDescending(Key => Key.Value.Rating))
                                {
                                    if (!prvaPostava.ContainsKey(item.Key))
                                    {
                                        if (item.Value.Position == "GK")
                                        {
                                            brGK++;
                                            if (brGK <= 1)
                                                prvaPostava.Add(item.Key, 0);
                                        }
                                        if (item.Value.Position == "DF")
                                        {
                                            brDF++;
                                            if (brDF <= 4)
                                                prvaPostava.Add(item.Key, 0);
                                        }
                                        if (item.Value.Position == "MF")
                                        {
                                            brMF++;

                                            if (brMF <= 3)
                                                prvaPostava.Add(item.Key, 0);
                                        }
                                        if (item.Value.Position == "FW")
                                        {
                                            brFW++;
                                            if (brFW <= 3)
                                                prvaPostava.Add(item.Key, 0);
                                        }
                                    }

                                }

                       
                                if (prvaPostava.Count() < 11)
                                {
                                    Console.WriteLine("U postavi nema dovljno igraca");
                                    Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("-----Postava-----\n");
                                    foreach (var item in prvaPostava)
                                    {
                                        Console.WriteLine($"{item.Key}");

                                    }
                                    Console.WriteLine("\n-----------------\n");
                                }

                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";

                                //------------------------------------------------------------------------------------------------- Ispis case 7 end
                                break;
                            case "8":
                                //------------------------------------------------------------------------------------------------- Ispis case 8 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("-------Ispis strijelaca i koliko golova imaju-------\n");

                                if(brojUtakmica==0)
                                    Console.WriteLine("Nema strijelaca");

                                foreach (var item in prvaPostava.OrderByDescending(Key => Key.Value))
                                    if (item.Value > 0)
                                    Console.WriteLine($"{item.Key} - {item.Value}");

                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";




                                //------------------------------------------------------------------------------------------------- Ispis case 8 end
                                break;
                            case "9":
                                //------------------------------------------------------------------------------------------------- Ispis case 9 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("-------------Ispis svih rezultata ekipe-------------\n");

                                foreach (var item in rezEkipe)
                                {
                                    if (item.Key == "PRVAHRV")
                                        Console.WriteLine($"HRVATSKA {item.Value.Zabijeni} - {item.Value.Primljeni} MAROKO");
                                        

                                    if (item.Key == "DRUGAHRV")
                                        Console.WriteLine($"HRVATSKA {item.Value.Zabijeni} - {item.Value.Primljeni} BELGIJA");

                                    if (item.Key == "TRECAHRV")
                                        Console.WriteLine($"HRVATSKA {item.Value.Zabijeni} - {item.Value.Primljeni} KANADA");


                                }

                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 9 start

                                break;
                            case "10":
                                //------------------------------------------------------------------------------------------------- Ispis case 10 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("----------------Ispis svih rezultata----------------\n");

                                foreach (var item in rezEkipe)
                                {
                                    if (item.Key == "PRVAHRV")
                                        Console.WriteLine($"HRVATSKA {item.Value.Zabijeni} - {item.Value.Primljeni} MAROKO");

                                    if (item.Key == "OSTALEPRVA")
                                        Console.WriteLine($"BELGIJA {item.Value.Zabijeni} - {item.Value.Primljeni} KANADA");

                                    if (item.Key == "DRUGAHRV")
                                        Console.WriteLine($"HRVATSKA {item.Value.Zabijeni} - {item.Value.Primljeni} BELGIJA");

                                    if (item.Key == "OSTALEDRUGA")
                                        Console.WriteLine($"KANADA {item.Value.Zabijeni} - {item.Value.Primljeni} MAROKO");

                                    if (item.Key == "TRECAHRV")
                                        Console.WriteLine($"HRVATSKA {item.Value.Zabijeni} - {item.Value.Primljeni} KANADA");

                                    if (item.Key == "OSTALETRECA")
                                        Console.WriteLine($"BELGIJA {item.Value.Zabijeni} - {item.Value.Primljeni} MAROKO");

                                }

                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 10 start

                                break;
                            case "11":
                                //------------------------------------------------------------------------------------------------- Ispis case 11 start
                                Console.Clear();
                                Console.WriteLine("---------------------STATISTIKA--------------------- ");
                                Console.WriteLine("-----------------ISPIS SVIH IGRACA------------------");
                                Console.WriteLine("----------------Ispis svih rezultata----------------\n");

                                int i = 1;

                                foreach (var item in tablica.OrderByDescending(Key => Key.Value))
                                {
                                    
                                    Console.WriteLine($"{i}. {item.Key} -> bodovi: {item.Value} -> gol razlika: {golRazlika[item.Key]}");
                                    i++;
                                }

                                Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                Console.ReadLine();
                                odabirCase3 = "";
                                //------------------------------------------------------------------------------------------------- Ispis case 11 start

                                break;
                            case "12":
                                ispisSvihIliNe = "";
                                konacno = 1;
                                break;
                            case "0":
                                konacno = 0;
                                break;

                        }



                        //---------------------------------------------------------------------------------------------- SWITCH case STATISTIKA end
                        if (konacno == 0)
                            return;
                        else if (konacno == 1)
                            continue;



                    } while (odabirCase3 != "0" && odabirCase3 != "1" && odabirCase3 != "2" && odabirCase3 != "3" && odabirCase3 != "4" && odabirCase3 != "5" && odabirCase3 != "6" && odabirCase3 != "7" && odabirCase3 != "8" && odabirCase3 != "9" && odabirCase3 != "10" && odabirCase3 != "11" && odabirCase3 != "12");
                }
                else
                    continue;
            } while (ispisSvihIliNe != "0" && ispisSvihIliNe != "1");

            //---------------------------------------------------------------------------------------------- case STATISTIKA end
            break;
        case 4:
            //---------------------------------------------------------------------------------------------- case KONTROLA IGRACA start



            string odabirCase4 = "asd";
            konacno = 100;
            //---------------------------------------------------------------------------------------------- SWITCH case KONTROLA IGRACA start
            do
            {
                Console.Clear();
                Console.WriteLine("---------------KONTROLA IGRACA----------------\n");
                Console.WriteLine("Odaberi: ");
                Console.WriteLine("1 - Unos novog igrača");
                Console.WriteLine("2 - Brisanje igrača");
                Console.WriteLine("3 - Uređivanje igrača");
                Console.WriteLine("4 - Povratak");
                Console.WriteLine("0 - Izlaz iz aplikacije");
                odabirCase4 = Console.ReadLine();


                switch (odabirCase4)
                {

                    default:
                        Console.WriteLine("\nKrivi unos!!\n");

                        break;
                    case "1":
                        // -------------------------------------------------------------------------------------- unos novog igraca case start
                        Console.WriteLine("\n--------------UNOS NOVOG IGRACA--------------- \n");

                        if (popisIgraca.Count() > 26)
                        {
                            Console.WriteLine("\nMaksimalan broj igraca u ekipi je 26!\n");
                            Console.WriteLine("Unesi bilo koji znak za povratak: ");
                            string unesiBiloSta = Console.ReadLine();
                            Console.WriteLine("");

                            odabirCase4 = "Ponovo udi";
                            continue;
                        }

                        do
                        {

                            Console.WriteLine("Unesi ime i prezime novog igraca:");
                            var imeNovogIgraca = Console.ReadLine();


                            var pozicijaNovogIgraca = "";
                            do
                            {
                                Console.WriteLine("\nUnesi poziciju novog igraca (GK, DF, MF ili FW):");
                                pozicijaNovogIgraca = Console.ReadLine();
                            } while (pozicijaNovogIgraca != "GK" && pozicijaNovogIgraca != "DF" && pozicijaNovogIgraca != "MF" && pozicijaNovogIgraca != "FW");


                            var ratingNovogIgraca = 0;

                            bool provjera = false;
                            do
                            {
                                Console.WriteLine("\nUnesi rating novog igraca (1-100):");

                                provjera = int.TryParse(Console.ReadLine(), out ratingNovogIgraca);

                            } while (provjera != true);

                            if (ratingNovogIgraca > 100)
                                ratingNovogIgraca = 100;
                            else if (ratingNovogIgraca <= 0)
                                ratingNovogIgraca = 0;




                            popisIgraca.Add(imeNovogIgraca, (pozicijaNovogIgraca, ratingNovogIgraca));

                            var odabirNoviIgrac = "";

                            do
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Odaberi:");
                                Console.WriteLine("1 - unos jos jednog igrača");
                                Console.WriteLine("2 - Povratak");

                                odabirNoviIgrac = Console.ReadLine();

                            } while (odabirNoviIgrac != "1" && odabirNoviIgrac != "2");

                            if (odabirNoviIgrac == "1")
                            {
                                Console.WriteLine("-------------NOVI IGRAČ-------------");
                                continue;
                            }
                            else
                            {
                                odabirCase4 = "5";
                                break;

                            }
                        } while (true);




                        break;
                    // -------------------------------------------------------------------------------------- unos novog igraca case end
                    case "2":
                        // -------------------------------------------------------------------------------------- brisanje igraca case start
                        
                       

                        string brisanjeIgracaKey = "";

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("---------------KONTROLA IGRACA----------------");
                            Console.WriteLine("------------BRISANJE IGRACA IGRACA------------ \n");
                            Console.WriteLine("Unesi ime i prezime igrača kojeg zeliš obrisati (Ime Prezime, pr. Luka Modric): ");

                            brisanjeIgracaKey = Console.ReadLine();

                            Console.WriteLine("");
                            var odabirBrisanje1 = "";
                            var odabirBrisanje2 = "";


                            if (!popisIgraca.ContainsKey(brisanjeIgracaKey)) {
                                do
                                {
                                    Console.WriteLine("Igrac nije dio ekipe - probaj ponovo: ");
                               Console.WriteLine("1 - Ponovno ucitavanje");
                               Console.WriteLine("0 - Izlazak iz brisanja");

                                odabirBrisanje1 = Console.ReadLine();
                                } while (odabirBrisanje1 != "1" && odabirBrisanje1 != "0");

                                if (odabirBrisanje1 == "1")
                                    continue;
                                else {
                                    odabirCase4 = "10";
                                    break;
}
                            }
                            else {
                                popisIgraca.Remove(brisanjeIgracaKey);
                                Console.WriteLine("USPJESNO IZBRISAN!!\n");
                                do
                                {
                                    Console.WriteLine("Odaberi:");
                                    Console.WriteLine("1 - Brisanje jos jednog igraca:");
                                    Console.WriteLine("0 - Povratak:");
                                    odabirBrisanje2 = Console.ReadLine();
                                } while (odabirBrisanje2 != "1" && odabirBrisanje2 != "0");
                                }

                            if (odabirBrisanje2 == "1")
                                continue;
                            else
                                break;


                        } while (!popisIgraca.ContainsKey(brisanjeIgracaKey));





                        break;
                    // -------------------------------------------------------------------------------------- brisanje igraca case end
                    case "3":
                        // -------------------------------------------------------------------------------------- uredivanje igraca case start        

                        var uredivanjeIgracaOdabir = "";

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("---------------KONTROLA IGRACA----------------");
                            Console.WriteLine("--------------UREDIVANJE IGRACA--------------- \n");
                            Console.WriteLine("1 - Uredi ime i prezime igraca");
                            Console.WriteLine("2 - Uredi poziciju igraca (GK, DF, MF ili FW");
                            Console.WriteLine("3 - Uredi rating igraca (od 1 do 100)");
                            Console.WriteLine("0 - Povratak");

                            uredivanjeIgracaOdabir = Console.ReadLine();

                            if (uredivanjeIgracaOdabir == "0")
                            {
                                odabirCase4 = "";
                                break;
                            }

                            switch (uredivanjeIgracaOdabir)
                            {

                                case "1":
                                    var staroImeIgraca = "";
                                    Console.Clear();
                                    Console.WriteLine("---------------KONTROLA IGRACA----------------");
                                    Console.WriteLine("--------------UREDIVANJE IGRACA--------------- ");
                                    Console.WriteLine("----------UREDI IME I PREZIME IGRACA---------- \n");

                                    do
                                    {
                                        Console.WriteLine("Unesi staro ime i prezime igraca: ");
                                        staroImeIgraca = Console.ReadLine();

                                    } while (!popisIgraca.ContainsKey(staroImeIgraca));

                                    Console.WriteLine("\nUnesi novo ime i prezime igraca: ");
                                    var novoImeIgraca = Console.ReadLine();

                                    popisIgraca[novoImeIgraca] = popisIgraca[staroImeIgraca];
                                    popisIgraca.Remove(staroImeIgraca);

                                    Console.WriteLine("\nIME SPREMLJENO!!");
                                    Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                    Console.ReadLine();
                                    uredivanjeIgracaOdabir = "5";

                                    break;

                                case "2":
                                    // ------------------------------------------------------------------------------------------ Uredivanje poz start
                                    Console.Clear();
                                    Console.WriteLine("---------------KONTROLA IGRACA----------------");
                                    Console.WriteLine("--------------UREDIVANJE IGRACA--------------- ");
                                    Console.WriteLine("------------UREDI POZICIJU IGRACA------------- \n");

                                    do
                                    {
                                        Console.WriteLine("Unesi ime i prezime igraca: ");
                                        staroImeIgraca = Console.ReadLine();

                                    } while (!popisIgraca.ContainsKey(staroImeIgraca));

                                    Console.WriteLine("\nUnesi novu poziciju igraca igraca(GK, DF, MF i FW): ");
                                    var novaPozicijaIgraca = Console.ReadLine();

                                    do
                                    {
                                        Console.WriteLine("Ispravno unesi novu poziciju igraca igraca (GK, DF, MF i FW): ");
                                        novaPozicijaIgraca = Console.ReadLine();

                                    } while (novaPozicijaIgraca != "GK" && novaPozicijaIgraca != "DF" && novaPozicijaIgraca != "MF" && novaPozicijaIgraca != "FW");


                                    foreach (var item in popisIgraca)
                                    {
                                        if (item.Key == staroImeIgraca)
                                            Console.WriteLine($"Stari -> {item.Key} - {item.Value.Position} - {item.Value.Rating}");
                                    }

                                    foreach (var item in popisIgraca)
                                    {
                                        if (item.Key == staroImeIgraca)
                                            popisIgraca[staroImeIgraca] = (novaPozicijaIgraca, item.Value.Rating);
                                    }

                                    foreach (var item in popisIgraca)
                                    {
                                        if (item.Key == staroImeIgraca)
                                            Console.WriteLine($"Novi  -> {item.Key} - {item.Value.Position} - {item.Value.Rating}");
                                    }

                                    // ------------------------------------------------------------------------------------------ Uredivanje poz end



                                    Console.WriteLine("\nPOZICIJA SPREMLJENA!!\n");
                                    Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                    Console.ReadLine();
                                    uredivanjeIgracaOdabir = "5";

                                    break;

                                case "3":
                                    // ------------------------------------------------------------------------------------------ Uredivanje ratinga start
                                    Console.Clear();
                                    Console.WriteLine("---------------KONTROLA IGRACA----------------");
                                    Console.WriteLine("--------------UREDIVANJE IGRACA--------------- ");
                                    Console.WriteLine("-------------UREDI RATING IGRACA-------------- \n");

                                    do
                                    {
                                        Console.WriteLine("Unesi ime i prezime igraca: ");
                                        staroImeIgraca = Console.ReadLine();

                                    } while (!popisIgraca.ContainsKey(staroImeIgraca));





                                    var noviRatingIgraca = 0;

                                    bool provjera = false;
                                    do
                                    {
                                        Console.WriteLine("\nUnesi novi rating igraca (1-100):");

                                        provjera = int.TryParse(Console.ReadLine(), out noviRatingIgraca);

                                    } while (provjera != true);

                                    if (noviRatingIgraca > 100)
                                        noviRatingIgraca = 100;
                                    else if (noviRatingIgraca <= 0)
                                        noviRatingIgraca = 0;



                                    foreach (var item in popisIgraca)
                                    {
                                        if (item.Key == staroImeIgraca)
                                            Console.WriteLine($"\nStari -> {item.Key} - {item.Value.Position} - {item.Value.Rating}");
                                    }

                                    foreach (var item in popisIgraca)
                                    {
                                        if (item.Key == staroImeIgraca)
                                            popisIgraca[staroImeIgraca] = (item.Value.Position, noviRatingIgraca);
                                    }

                                    foreach (var item in popisIgraca)
                                    {
                                        if (item.Key == staroImeIgraca)
                                            Console.WriteLine($"Novi  -> {item.Key} - {item.Value.Position} - {item.Value.Rating}");
                                    }





                                    Console.WriteLine("\nRATING SPREMLJEN!!\n");
                                    Console.WriteLine("\nPovratak(unesi bilo sto)...");
                                    Console.ReadLine();
                                    uredivanjeIgracaOdabir = "5";
                                    // ------------------------------------------------------------------------------------------ Uredivanje ratinga end
                                    break;
                            }



                        } while (uredivanjeIgracaOdabir != "1" && uredivanjeIgracaOdabir != "2" && uredivanjeIgracaOdabir != "3" && uredivanjeIgracaOdabir != "0");

                        break;
                    // -------------------------------------------------------------------------------------- uredivanje igraca case end
                    case "4":
                        konacno = 1;
                        break;
                    case "0":
                        konacno = 0;
                        break;



                }
                //---------------------------------------------------------------------------------------------- SWITCH case KONTROLA IGRACA end
            } while (odabirCase4 != "4" && odabirCase4 != "3" && odabirCase4 != "2" && odabirCase4 != "1" && odabirCase4 != "0");
            if (konacno == 0)
                return;
            else if (konacno == 1)
                continue;


            //---------------------------------------------------------------------------------------------- case KONTROLA IGRACA end
            break;
        case 0:
            return;

            //---------------------------------------------------------------------------------------------- SWITCH END



    }
} while (naredba != 0);

