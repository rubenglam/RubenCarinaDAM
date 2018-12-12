using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Esteganografia.ClassesEsteganografiques
{
    public class EsteganografiarFitxer
    {
        private static short[] BITS_PER_DEFECTE = { 5, 6, 7, 13, 14, 15, 22, 23 };
        private System.Drawing.Bitmap imatge;
        private static short[] fitxer;
        private short[] bitsFitxer;

        public EsteganografiarFitxer(Bitmap btm, short[] bFitxer) {
            imatge = btm;
            bitsFitxer = bFitxer;

        }
        public EsteganografiarFitxer() : this(null, BITS_PER_DEFECTE) { }

        public Bitmap Esteganografiar(Bitmap btm, short[] fitxer)
        {

            Bitmap novaImat = new Bitmap(btm.Width, btm.Height);

            int y = 0;
            int x = 0;
            Color color;
            short[] pixel;
            int indexFitxer = 0; // index de la array que conte la informacio del fitxer
            int indexBitsFitxer = 0; // index de la array que conte quins son els index de cada pixel que subtituirem
            bool trobat = false; // boolea que controla si ja hem trobat l'index del pixel dintre de la array bitsFitxer
            bool finalFitxer = false;

            // ****** Falta: controlar que el fitxer hi quepiga dintre la imatge ((fitxer.lenght / 8) < (imatge.hight * imatge.width))
            // Falta: afegir el tamany del fitxer als 4 primer pixels: 
            // s'hauria de convertir el tamany a una array de 16 bits i substituir els 8 primers en el primer pixel i els 8 seguents al segon pixel

            int sizeFile = fitxer.Length;
            short[] sizeBinari = GetBinariLength(fitxer.Length);


            for (int i = 0; i < 4; i++)
            {

                pixel = AjuntarColors(btm.GetPixel(x, y));

                for (int indexPixel = 0; indexPixel < pixel.Length; indexPixel++)
                {

                    while (!trobat && indexBitsFitxer < pixel.Length)
                    {

                        if (bitsFitxer[indexPixel] == indexBitsFitxer)
                        {
                            trobat = true;
                            pixel[bitsFitxer[indexPixel]] = sizeBinari[indexPixel + (i * 8)];
                        }
                        indexBitsFitxer++;

                    }

                    trobat = false;

                }

                // Afegim el pixel a la imatge nova
                color = PasarColor(pixel);
                novaImat.SetPixel(x, y, color);

                //Aumentem el la x per pasar al seguent pixel 
                x++;
                indexBitsFitxer = 0;

            }

            // Recorrem la imatge de dalt a vaix... 
            while (y < btm.Height && !finalFitxer)
            {   //... i de esquerra a dreta
                while (x < btm.Width && !finalFitxer)
                {

                    pixel = AjuntarColors(btm.GetPixel(x, y));

                    for (int indexPixel = 0; indexPixel < 8 && !finalFitxer; indexPixel++)
                    {

                        while (!trobat && indexBitsFitxer < pixel.Length && !finalFitxer)
                        {

                            if (bitsFitxer[indexPixel] == indexBitsFitxer)
                            {
                                trobat = true;
                                pixel[bitsFitxer[indexPixel]] = Utils.ConvertTo.ConvertirABinari(fitxer[indexFitxer])[indexPixel];
                                if (indexFitxer == fitxer.Length - 1) finalFitxer = true;

                            }

                            indexBitsFitxer++;

                        }

                        trobat = false;

                    }

                    // Afegim el pixel a la imatge nova
                    color = PasarColor(pixel);
                    novaImat.SetPixel(x, y, color);

                    //Aumentem el la x per pasar al seguent pixel 
                    x++;
                    indexFitxer++;
                    indexBitsFitxer = 0;

                }

                // si no hem arrivat al final del fitxer, posem a 0 la x (hem tractat tota la fila)
                // i aumentem la fila (y)
                if (!finalFitxer)
                {
                    x = 0;
                    y++;

                }

            }

            // acavem de recorrer tota la imatge i guardem els pixels tal cual
            for (; y < novaImat.Height; y++)
            {
                for (; x < novaImat.Width; x++)
                {
                    novaImat.SetPixel(x, y, btm.GetPixel(x, y));
                }
            }

            return novaImat;
        }

        public int[] Desesteganografiar(Bitmap bitmap)
        {

            int y = 0;
            int x = 0;
            short[] pixel;
            int indexFitxer = 0; // index de la array que conte la informacio del fitxer
            int indexBitsFitxer = 0; // index de la array que conte quins son els index de cada pixel que subtituirem
            bool trobat = false; // boolea que controla si ja hem trobat l'index del pixel dintre de la array bitsFitxer
            bool finalFitxer = false;
            int[] resultat;

            int sizeFile;
            string sizeBinariText = "";

            for (int i = 0; i < 4; i++)
            {

                pixel = AjuntarColors(bitmap.GetPixel(x, y));

                for (int indexPixel = 0; indexPixel < pixel.Length; indexPixel++)
                {

                    while (!trobat && indexBitsFitxer < pixel.Length)
                    {

                        if (bitsFitxer[indexPixel] == indexBitsFitxer)
                        {
                            trobat = true;
                            sizeBinariText += pixel[bitsFitxer[indexPixel]];
                        }
                        indexBitsFitxer++;

                    }

                    trobat = false;

                }

                x++;
                indexBitsFitxer = 0;

            }

            sizeFile = GetDecimalLength(sizeBinariText);
            resultat = new int[sizeFile];

            if (sizeFile > (bitmap.Height * bitmap.Width) + 4) throw new Exception("El fitxer és molt gran, no pot estar en aquesta imatge");

            short[] valorActual = new short[8];

            while (y < bitmap.Height && !finalFitxer)
            {
                while (x < bitmap.Width && !finalFitxer)
                {

                    pixel = AjuntarColors(bitmap.GetPixel(x, y));

                    for (int indexPixel = 0; indexPixel < 8 && !finalFitxer; indexPixel++)
                    {

                        while (!trobat && indexBitsFitxer < pixel.Length && !finalFitxer)
                        {

                            if (bitsFitxer[indexPixel] == indexBitsFitxer)
                            {
                                trobat = true;
                                valorActual[indexPixel] = pixel[bitsFitxer[indexPixel]];
                                if (indexFitxer == sizeFile - 1) finalFitxer = true;

                            }

                            indexBitsFitxer++;

                        }

                        trobat = false;

                    }

                    resultat[indexFitxer] = Utils.ConvertTo.ConvertirADecimal(valorActual);
                    x++;
                    indexFitxer++;
                    indexBitsFitxer = 0;
                    valorActual = new short[8];

                }

                if (!finalFitxer)
                {
                    x = 0;
                    y++;

                }

            }

            return resultat;

        }

        private int GetDecimalLength(string input)
        {
            char[] array = input.ToCharArray();
            // Invertido pues los valores van incrementandose de derecha a izquierda: 16-8-4-2-1
            Array.Reverse(array);
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '1')
                {
                    // Usamos la potencia de 2, según la posición
                    sum += (int)Math.Pow(2, i);
                }
            }
            return sum;
        }

        private short[] GetBinariLength(int fileLength)
        {

            int fileLengthInternal = fileLength;
            short[] resultat = new short[32];
            int posicioActual = 31;

            while (fileLengthInternal > 0)
            {
                if (fileLengthInternal % 2 == 0)
                {
                    resultat[posicioActual] = 0;
                }
                else
                {
                    resultat[posicioActual] = 1;
                }
                fileLengthInternal = (int)(fileLengthInternal / 2);
                posicioActual--;
            }

            for (int i = posicioActual; i >= 0; i--) resultat[posicioActual] = 0;

            return resultat;

        }

        public short[] AjuntarColors(Color c)
        {

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
        private Color PasarColor(short[] pixel)
        {

            short[] r = new short[8];
            short[] g = new short[8];
            short[] b = new short[8];

            for (int i = 0; i < 8; i++)
            {
                r[i] = pixel[i];
                g[i] = pixel[i + 8];
                b[i] = pixel[i + 16];

            }
            int dR = Utils.ConvertTo.ConvertirADecimal(r);
            int dG = Utils.ConvertTo.ConvertirADecimal(g);
            int dB = Utils.ConvertTo.ConvertirADecimal(b);

            Color color = Color.FromArgb(dR, dG, dB);

            return color;
        }



    }
}
