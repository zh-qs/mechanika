using System;
using System.Collections.Generic;
using System.Text;

namespace MechanikaInterface
{
    static class Constants
    {
        public const int DefaultZoomIndex = 5;
    }
    static class Strings
    {
        public const string Version = "0.0.0.1";

        public const string WrongOpt = "Niepoprawna opcja";
        public const string WrongArg = "Niepoprawny argument";
        public const string WrongCom = "Niepoprawne polecenie";
        public const string WrongPar = "Błędne zadanie: uklad nie daje się wyznaczyć w klasyczny sposób.\n(być może jestem za głupi do jego obliczenia)";
        public const string Help = "Komendy:\n - bel x1 y1 x2 y2\n - kra x1 y1 x2 y2 [x3 y3 ...]\n - pod (pp/pnp/utw) x y\n - prz x y\n - obc pun x y vx vy\n - obc cia x1 y1 x2 y2 vx vy\n - obc mom x y v\n - zm (b/f/a) skala\n - cli [pod/prz/obc]\n - cal\n - q\n - ?\n - ??";
        public const string About = "Mechanika v" + Version + ". (C) 2020 种签时.\nNarzędzie do obliczania belek, kratownic i układów złożonych.\nPrzydatne w celu sprawdzenia wyników i układania zadań.\nW przyszłości planowane dodanie funkcji obliczania linii wpływu.\nNa problemy nie odpowiadaj złością, tylko zgłoś mailem na adres mbapp@protonmail.ch.\nProszę o poprawną polszczyznę!";
        public const string BrakBelek = "Punkt nie znajduje się na żadnej z belek";
        //public const string UklChw = "układ chwiejny";
        //public const string UlkStatNwz = "układ statycznie niewyznaczalny";
        public const string PSPBelNos = "Podaj numery belek/prętów, które mają być dołączone zewnętrznie (puste, jeśli nic):";

        public const string belkaCommand = "bel";
        public const string podporaCommand = "pod";
        public const string przegubCommand = "prz";
        public const string obcCommand = "obc";
        public const string podPPArg = "pp";
        public const string podPNPArg = "pnp";
        public const string podUTWArg = "utw";
        public const string obcPktArg = "pun";
        public const string obcCiagArg = "cia";
        public const string obcMomArg = "mom";
        public const string calCommand = "cal";
        public const string clearCommand = "cli";
        public const string zoomCommand = "zm";
        public const string quitCommand = "q";
        public const string helpCommand = "?";
        public const string aboutCommand = "??";
        public const string kratCommand = "kra";
        public const string undoCommand = "cof";

        public const string belkaOper = "Dodanie belki";
        public const string kratOper = "Dodanie układu prętów";
        //public const string podporaOper = "Dodanie belki";
        public const string przegubOper = "Dodanie przegubu";
        public const string obcPktOper = "Dodanie obciążenia punktowego";
        public const string obcCiaOper = "Dodanie obciążenia ciągłego";
        public const string obcMomSOper = "Dodanie momentu skupionego";
        public const string obcMomCOper = "Dodanie momentu ciągłego";
        public const string podPPOper = "Dodanie podpory przegubowej przesuwnej";
        public const string podPNPOper = "Dodanie podpory przegubowej nieprzesuwnej";
        public const string podUTWOper = "Dodanie utwierdzenia";
        public const string cliOper = "Wyczyszczenie układu";
    }
}
