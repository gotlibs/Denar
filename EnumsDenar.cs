using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Denar
{
    public class EnumsDenar
    {
        #region PlaciloTip
        public sealed class PlaciloTip
        {
            private PlaciloTip()
            {

            }

            public static ListDictionary AllValues
            {
                get
                {
                    ListDictionary ar = new ListDictionary();
                    ar.Add(1, "V skupno");
                    ar.Add(2, "Kot osebni strošek");
                    ar.Add(3, "Partnerju (V skupno)");
                    ar.Add(4, "Partnerju (Kot osebni strošek)");
                    ar.Add(5, "Iz skupnega");
                    return (ar);
                }
            }
            public static int VSkupno { get { return 1; } }
            public static int KotOsebniStrosek { get { return 2; } }
            public static int PartnerjuVSkupno { get { return 3; } }
            public static int PartnerjuKotOsebniStrosek { get { return 4; } }
            public static int IzSkupnega { get { return 5; } }
        }
        #endregion

        #region Uporabnik
        public sealed class Uporabnik
        {
            private Uporabnik()
            {

            }

            public static ListDictionary AllValues
            {
                get
                {
                    ListDictionary ar = new ListDictionary();
                    ar.Add(1, "Simon Gotlib");
                    ar.Add(2, "Anita Franko");
                    return (ar);
                }
            }
            public static int SimonGotlib { get { return 1; } }
            public static int AnitaFranko { get { return 2; } }
        }
        #endregion

        #region OpombaOpis
        public sealed class OpombaOpis
        {
            private OpombaOpis()
            {

            }

            public static ListDictionary AllValues
            {
                get
                {
                    ListDictionary ar = new ListDictionary();
                    ar.Add(1, "Služba malica adacta");
                    ar.Add(13, "Služba podjetje prispevki");
                    ar.Add(14, "Služba podjetje dohodnina");
                    ar.Add(2, "Prevoz tankanje Petrol");
                    ar.Add(3, "Prevoz tankanje OMV");
                    ar.Add(4, "Trgovina Interspar");
                    ar.Add(5, "Trgovina Tuš");
                    ar.Add(6, "Trgovina Hofer");
                    ar.Add(7, "Trgovina Lidl");
                    ar.Add(8, "Trgovina Muller");
                    ar.Add(9, "Trgovina H&M");
                    ar.Add(10, "Strošek stanovanja");
                    ar.Add(11, "Strošek Mobitel Simon");
                    ar.Add(12, "Strošek Zavarovanje Simon");
                    return (ar);
                }
            }
            public static int SluzbaMalicaAdacta { get { return 1; } }
            public static int PetrolTankanjePrevoz { get { return 2; } }
            public static int OmwTankanjePrevoz { get { return 3; } }
            public static int IntersparTrgovina { get { return 4; } }
            public static int TusTrgovina { get { return 5; } }
            public static int HoferTrgovina { get { return 6; } }
            public static int LidlTrgovina { get { return 7; } }
            public static int MullerTrgovina { get { return 8; } }
            public static int HMTrgovina { get { return 9; } }
            public static int StroskiStanovanja { get { return 10; } }
            public static int Mobitel { get { return 11; } }
            public static int Zavarovanje { get { return 12; } }
            public static int PodjetjePrispevki { get { return 13; } }
            public static int PodjetjeDohodnina { get { return 14; } }
        }
        #endregion

        #region PageMode
        public enum PageMode
        {
            ReadOnly = 0,
            Insert = 1,
            Edit = 2
        } 
        #endregion

        #region RezultatShranjevanja
        public sealed class RezultatShranjevanja
        {
            private RezultatShranjevanja()
            {

            }

            public static ListDictionary AllValues
            {
                get
                {
                    ListDictionary ar = new ListDictionary();
                    ar.Add(0, "Napaka");
                    ar.Add(1, "Uspesno");
                    ar.Add(2, "Neuspesno");
                    return (ar);
                }
            }
            public static int Napaka { get { return 0; } }
            public static int Uspesno { get { return 1; } }
            public static int Neuspesno { get { return 2; } }
        }
        #endregion

        #region Znesek
        public sealed class Znesek
        {
            private Znesek()
            {

            }

            public static ListDictionary AllValues
            {
                get
                {
                    ListDictionary ar = new ListDictionary();
                    ar.Add(0, "Podjetje prispevki");
                    ar.Add(1, "Zavarovanje");
                    ar.Add(2, "Mobitel");
                    ar.Add(3, "SluzbaMalicaAdacta");
                    ar.Add(4, "Podjetje dohodnina");
                    return (ar);
                }
            }
            public static string PodjetjePrispevki { get { return "223,54"; } }
            public static string Zavarovanje { get { return "28,54"; } }
            public static string Mobitel { get { return "20,13"; } }
            public static string SluzbaMalicaAdacta { get { return "4,5"; } }
            public static string PodjetjeDohodnina { get { return "150"; } }
        }
        #endregion
    }
}