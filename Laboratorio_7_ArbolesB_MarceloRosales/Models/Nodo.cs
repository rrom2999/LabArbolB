using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_7_ArbolesB_MarceloRosales.Models
{
    public class Nodo
    {
        public Medicamento[] Llaves; //areglo de llaves
        public int Grado;  //Grado minoimo (El numero de llaves)
        public Nodo[] Hijos; //Areglo con los hijos
        public int NumerodeLlaves;  //Numero de llaves
        public bool Hoja; //Es verdadero si el nodo es una hoja

        public Nodo(int _t, bool _leaf)
        {
            Grado = _t;
            Hoja = _leaf;
            Llaves = new Medicamento[2 * Grado - 1];
            Hijos = new Nodo[2 * Grado];
            NumerodeLlaves = 0;

        }
        public void insertarVacio(Medicamento k)
        {
            int i = NumerodeLlaves - 1;
            if (Hoja == true)
            {
                while (i >= 0 && string.Compare(Llaves[i].Nombre, k.Nombre) > 0)
                {
                    Llaves[i + 1] = Llaves[i];
                    i--;
                }
                Llaves[i + 1] = k;
                NumerodeLlaves = NumerodeLlaves + 1;
            }
            else
            {
                while (i >= 0 && string.Compare(Llaves[i].Nombre, k.Nombre) > 0)
                    i--;

                if (Hijos[i + 1].NumerodeLlaves == 2 * Grado - 1)
                {
                    DividirHijo(i + 1, Hijos[i + 1]);

                    if (string.Compare(Llaves[i + 1].Nombre, k.Nombre) < 0) { i++; }

                }
                Hijos[i + 1].insertarVacio(k);

            }
        }

        public void DividirHijo(int i, Nodo temp)
        {
            Nodo aux = new Nodo(temp.Grado, temp.Hoja);
            aux.NumerodeLlaves = Grado - 1;

            for (int j = 0; j < Grado - 1; j++)
            {
                aux.Llaves[j] = temp.Llaves[j + Grado];

            }

            if (temp.Hoja == false)
            {
                for (int j = 0; j < Grado; j++)
                {
                    aux.Hijos[j] = temp.Hijos[j + Grado];
                }
            }

            temp.NumerodeLlaves = Grado - 1;


            for (int j = NumerodeLlaves; j >= i + 1; j--)
            {
                Hijos[j + 1] = Hijos[j];
            }

            Hijos[i + 1] = aux;

            for (int j = NumerodeLlaves - 1; j >= i; j--)
                Llaves[j + 1] = Llaves[j];

            Llaves[i] = temp.Llaves[Grado - 1];

            NumerodeLlaves = NumerodeLlaves + 1;
        }

       
        public void buscar(string nombre, int cambio, int prueba)
        {
            int i = 0;
            for (i = 0; i < NumerodeLlaves; i++)
            {
                if (Hoja == false)
                    Hijos[i].buscar(nombre, cambio, prueba);
                if (nombre == Llaves[i].Nombre)
                {
                    if (prueba == 1)//Si es uno resta inventarios por una compra
                    {

                        Llaves[i].Existencia -= cambio;
                    }
                    if(prueba == 2)// Si es dos agrega un nuevo stock al inventario
                    {
                        Llaves[i].Existencia = cambio;
                    }

                }
            }
            if (Hoja == false)
                Hijos[i].buscar(nombre, cambio, prueba);
        }
        public void retornarMed(string nombre, ref Medicamento tempo)
        {
            int i = 0;
            for (i = 0; i < NumerodeLlaves; i++)
            {
                if (Hoja == false)
                    Hijos[i].retornarMed(nombre, ref tempo);
                if (nombre == Llaves[i].Nombre)
                {
                    tempo = Llaves[i];

                }
            }
            if (Hoja == false)
                Hijos[i].retornarMed(nombre, ref  tempo);

        }
        public void ArbolAVector(ref List<Medicamento> tempo)
        {
            int i = 0;
            for (i = 0; i < NumerodeLlaves; i++)
            {
                if (Hoja == false)
                    Hijos[i].ArbolAVector(ref tempo);

                if (Llaves[i].Existencia != 0)
                {
                    tempo.Add(Llaves[i]);
                }

            }
            if (Hoja == false)
                Hijos[i].ArbolAVector(ref  tempo);

        }

    }
}