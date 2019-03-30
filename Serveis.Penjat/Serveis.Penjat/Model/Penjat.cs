using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat.Model
{
    public class Penjat
    {
        string _paraulaBase;
        int _intents;
        const int _maximIntents = 7;
        string _paraula;

        public Penjat(string paraula)
        {
            _paraulaBase = paraula;
        }

        public void Restart()
        {
            _intents = 0;
            for (int i = 0; i < _paraulaBase.Length; i++)
            {
                _paraula += '_';
            }
        }

        public bool ComprovarLletra(char lletra)
        {
            string nouTrobat = "";

            if (!Finalitzat)
            {
                if (ParaulaBase.Contains(lletra))
                {
                    for (int i = 0; i < _paraula.Length; i++)
                    {
                        if (ParaulaBase[i] == lletra)
                        {
                            nouTrobat += lletra;
                        }
                        else nouTrobat += _paraula[i];
                    }

                    _paraula = nouTrobat;
                }
                else
                {
                    _intents++;
                }
                return ParaulaBase.Contains(lletra);
            }
            else return false;

        }

        #region Propietats
        public bool Finalitzat
        {
            get
            {
                return _paraula == _paraulaBase || _intents == _maximIntents;
            }
        }

        public string ParaulaBase { get => _paraulaBase; set => _paraulaBase = value; }
        public int Intents { get => _intents; set => _intents = value; }
        public int MaximIntents { get => _maximIntents; }
        public string Paraula { get => _paraula; set => _paraula = value; }
        #endregion
    }
}
