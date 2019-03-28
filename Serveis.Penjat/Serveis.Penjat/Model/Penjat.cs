using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat.Model
{
    public class Penjat
    {
        string paraula;
        int intents;
        int maximIntents;
        string trobat;

        public Penjat(string paraula, int maximIntents)
        {
            this.paraula = paraula;
            this.intents = 0;
            this.maximIntents = maximIntents;
            for (int i=0; i < paraula.Length; i++)
            {
                trobat += '_';
            }
        }

        public bool ComprovarLletra(char lletra)
        {
            string nouTrobat = "";

            if (!Finalitzat)
            {
                if (Paraula.Contains(lletra))
                {
                    for (int i = 0; i < paraula.Length; i++)
                    {
                        if (paraula[i] == lletra)
                        {
                            nouTrobat += lletra;
                        }
                        else nouTrobat += trobat[i];
                    }

                    trobat = nouTrobat;
                }
                else
                {
                    intents++;
                }
                return Paraula.Contains(lletra);
            }
            else return false;

        }

        #region Propietats
        public bool Finalitzat
        {
            get
            {
                return trobat == paraula || intents == maximIntents;
            }
        }


       
        public string Paraula { get => paraula; set => paraula = value; }
        public int Intents { get => intents; set => intents = value; }
        public int MaximIntents { get => maximIntents; set => maximIntents = value; }
        public string Trobat { get => trobat; set => trobat = value; }
        #endregion
    }
}
