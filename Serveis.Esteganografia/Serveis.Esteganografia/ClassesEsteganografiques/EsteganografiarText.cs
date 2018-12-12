using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Esteganografia.ClassesEsteganografiques
{
    public class EsteganografiarText
    {

        private static short[] BITS_PER_DEFECTE = { 5, 6, 7, 13, 14, 15, 22, 23 };
        private System.Drawing.Bitmap imatge;
        private string text;
        private short[] bitsMissatge;

        public EsteganografiarText(Bitmap btm, string text, short[] bits)
        {
            imatge = btm;
            this.text = text;
            bitsMissatge = bits;
        }
        public EsteganografiarText(Bitmap btm) : this(btm, "", BITS_PER_DEFECTE) { }
        public EsteganografiarText(Bitmap btm, string txt) : this(btm, txt, BITS_PER_DEFECTE) { }
        public EsteganografiarText() : this(null, "", BITS_PER_DEFECTE) { }

        public Bitmap Esteganografiar(Bitmap btm, string txt)
        {
            Bitmap novaImat = new Bitmap(btm.Width, btm.Height);
            short[] lletra;

            int y = 0;
            int x = 0;
            Color color;
            short[] pixel;

            // Coger letra que corresponda
            while ((x + (y * btm.Width)) < txt.Length && y < btm.Height) {
                while ((x + (y * btm.Width)) < txt.Length && x < btm.Width)
                {
                    lletra = Utils.ConvertTo.ConvertirABinari(txt[x + (y * btm.Height)]);
                    pixel = AjuntarColors(btm.GetPixel(x, y));

                    pixel = SubstituirBits(pixel, lletra);

                    color = PasarColor(pixel);

                    novaImat.SetPixel(x, y, color);
                    x++;
                }
                if (x + (y * btm.Width) < txt.Length)
                {
                    x = 0;
                    y++;

                }
            }

            for (; y < novaImat.Height; y++)
            {
                for (; x < novaImat.Width; x++)
                {
                    novaImat.SetPixel(x, y, btm.GetPixel(x,y));
                }   
            }

            
            // pasar lettra a bits[]
            // Coger cada pixel de la imag
            // substituir cada bit que corresponda i mantener el resto
            // gurdar la imagen resultatnte.

            return novaImat;
        }

        private Color PasarColor(short[] pixel)
        {
            
            short[] r = new short[8];
            short[] g = new short[8];
            short[] b = new short[8];

            for (int i = 0; i < 8; i++) {
                r[i] = pixel[i];
                g[i] = pixel[i+8];
                b[i] = pixel[i+16];

            }
            int dR = Utils.ConvertTo.ConvertirADecimal(r);
            int dG = Utils.ConvertTo.ConvertirADecimal(g);
            int dB = Utils.ConvertTo.ConvertirADecimal(b);

            Color color = Color.FromArgb(dR, dG, dB);

            return color;
        }

        private short[] SubstituirBits(short[] pixel, short[] lletra)
        {
            bool trobat = false;
            int e = 0;
            for (int i = 0; i < pixel.Length; i++) {

                while (!trobat && e < bitsMissatge.Length) {

                    if (bitsMissatge[e]==i)
                    {
                        trobat = true;
                        pixel[i] = lletra[e];
                        
                    }
                    
                    e++;
                    
                }
                e = 0;
                trobat = false;

            }

            return pixel;
        }

        /// <summary>
        /// Converteix els 3 valors de color de un pixel a una array de 24 shorts, un valor per cada bit.
        /// </summary>
        /// <param name="c">"Color" (pixel)</param>
        /// <returns>Bits que conformen un pixel</returns>
        public short[] AjuntarColors(Color c) {

            short[] colorR = new short[8];
            short[] colorG = new short[8];
            short[] colorB = new short[8];
            short[] ajuntar = new short[24];

            colorR = Utils.ConvertTo.ConvertirABinari(c.R);
            colorG = Utils.ConvertTo.ConvertirABinari(c.G);
            colorB = Utils.ConvertTo.ConvertirABinari(c.B);
           

            for (int i = 0; i < 8; i++)
            {
                ajuntar[i] = colorR[i];
                ajuntar[i + 8] = colorG[i];
                ajuntar[i + 16] = colorB[i];
            }
            
            return ajuntar;

        }

        public String Desesteganografiar(Bitmap btm)
        {

            String txt = "";

            for (int y = 0; y < btm.Height; y++)
            {
                for (int x = 0; x < btm.Width; x++)
                {
                    txt += (ObtenirLletra(btm, x, y));
                }
                //txt += "\n";
            }


            return txt;
        }

        private char ObtenirLletra(Bitmap imatgeEstegano, int x, int y)
        {

            // Obtenir la primera lletra del missatfe ocult

            var pixel = imatgeEstegano.GetPixel(x, y);

            // Passar a binari el pixel.r;
            short[] r = Utils.ConvertTo.ConvertirABinari(pixel.R);

            // Passar a binari el pixel. G;
            short[] g = Utils.ConvertTo.ConvertirABinari(pixel.G);

            // Passar a binari el pixel.B;
            short[] b = Utils.ConvertTo.ConvertirABinari(pixel.B);

            // Ajuntar els tres pixels
            short[] ajuntar = new short[24];

            for (int i = 0; i < 8; i++)
            {
                ajuntar[i] = r[i];
                ajuntar[i + 8] = g[i];
                ajuntar[i + 16] = b[i];

            }

            short[] resultat = new short[8];

            for (int i = 0; i < resultat.Length; i++)
            {
                resultat[i] = ajuntar[bitsMissatge[i]];

            }

            // Passar cada posicio dins l'arrai resultat.

            // Pasar resultat a decimal i convertir a char. 
            int result = Utils.ConvertTo.ConvertirADecimal(resultat);

            return Convert.ToChar(result);

        }

        #region PROPERTIES

        /// <summary>
        /// Imatge que volem tractar
        /// </summary>
        public Bitmap Imatge
        {
            get { return imatge; }
            set { imatge = value; }

        }

        /// <summary>
        /// Text del missatge
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        ///  Bits on es guarda la informació de cada lletra del misatge
        /// </summary>
        public short[] BitsMissatge
        {
            get { return bitsMissatge; }
            set { bitsMissatge = value; }
        }

        #endregion

    }

}
