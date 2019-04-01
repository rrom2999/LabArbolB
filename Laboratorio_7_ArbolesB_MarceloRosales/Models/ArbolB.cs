using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_7_ArbolesB_MarceloRosales.Models
{
    public class ArbolB
    {
        Nodo raiz;
        int t;

        public ArbolB(int t1)
        {
            raiz = null;
            t = t1;
        }
        public bool comprobarRaiz()
        {
            if(raiz == null)
            {
                return true;
            }
            else{
               return false;
            }
        }

        public void insertar(Medicamento k)
        {
            if (raiz == null)
            {
                raiz = new Nodo(t, true);
                raiz.Llaves[0] = k;
                raiz.NumerodeLlaves = 1;
            }
            else
            {
                if (raiz.NumerodeLlaves == 2 * t - 1)
                {
                    Nodo temp = new Nodo(t, false);
                    temp.Hijos[0] = raiz;
                    temp.DividirHijo(0, raiz);

                    int i = 0;
                    if (string.Compare(temp.Llaves[0].Nombre, k.Nombre) < 0)
                        i++;

                    temp.Hijos[i].insertarVacio(k);

                    raiz = temp;
                }
                else
                {
                    raiz.insertarVacio(k);

                }
            }
        }
      
        public void buscar(string nombre, int cambio, int prueba)
        {

            if (raiz != null)
            {
                raiz.buscar(nombre, cambio, prueba);
            }

        }
        public void buscarMedicamento(string nombre, ref Medicamento tempo)
        {

            if (raiz != null)
            {
                raiz.retornarMed(nombre, ref tempo);
            }

        }
        public void ArbolenVector(ref List<Medicamento> tempo)
        {

            if (raiz != null)
            {
                raiz.ArbolAVector(ref tempo);
            }

        }

    }
}