using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Laboratorio_7_ArbolesB_MarceloRosales.Models;
using Newtonsoft.Json;
namespace Laboratorio_7_ArbolesB_MarceloRosales.Controllers

{
    public class MedicamentosController : Controller
    {
        public static ArbolB ArbolInventario;
        public static ArbolB ArbolPedido;
        public static List<Compras> ListadeCompradores = new List<Compras>();
        public static List<Medicamento> ListaVacia = new List<Medicamento>();
 
  
        // GET: Medicamentos
        public static void dividir(int tamaño)
        {
            ArbolInventario = new ArbolB(tamaño);
            ArbolPedido = new ArbolB(tamaño);
            
            System.IO.StreamReader lector = new System.IO.StreamReader("C:/Users/Ronal/Desktop/Laboratorio4_MarceloRosales_RonaldOvalle_ArbolesB-master/Laboratorio_7_ArbolesB_MarceloRosales/MOCK_DATA .csv");


            Medicamento temp2 = new Medicamento();
            char[] caracteres = { '$', '/', ' ', '*', ',' };

            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                //Console.WriteLine(linea);
                var valores = linea.Split(',');
                ArbolInventario.insertar(temp2.crear(Convert.ToInt32(valores[0]), valores[1], valores[2], valores[3], Convert.ToDouble((valores[4].Trim(caracteres))), Convert.ToInt32(valores[5])));
            }
        }
        public ActionResult Index()
        {
           
            return View();
        }
        public static int verificacion = 0;
        public ActionResult Menu()
        {
            if (verificacion ==0)
            {
                verificacion += 1;
                return RedirectToAction("/DefinirTamaño");

            }
            else
            {


                return View();
            }
        }
        public ActionResult DefinirTamaño()
        {
            return View();
        }
        public ActionResult PedidoDeMedicamento()
        {

            return View();
        }
        public ActionResult MercanciaComprada(Compras Desplegar)
        {
            return View(Desplegar);
        }
        public ActionResult VentanaDeFalla()
        {
            return View();
        }
        public ActionResult Serializar()
        {
            // LLamar metodo serializar serializar(EnviarLIsta)
            List<Medicamento> serializacion = new List<Medicamento>();
            ArbolInventario.ArbolenVector(ref serializacion);
            var Direccion = Server.MapPath("~/InformeJson/ArbolJson.json");
            using (StreamWriter nodos = new StreamWriter(Direccion))
            {
                foreach(var item in serializacion)
                {
                    string nodo = JsonConvert.SerializeObject(item);
                    nodos.WriteLine(nodo);
                }
            }
                // LLamar metodo serializar serializar(EnviarLIsta)  enviar parametro de lista "serializacion" al metodo serializar

                return RedirectToAction("/Menu");
        }

        //Al momento de reabastecer el inventario se insertara de nuevo al arbol, antes de probar el metodo 
        // insertar el metodo de eliminar

        public ActionResult ReabastecerInventario()
        {
            bool vacio = !ListaVacia.Any();
            if (vacio) { return RedirectToAction("/Menu"); }//Si todos los medicamnentos existen entonces retorna al menu
            else
            {
                Medicamento tempo = new Medicamento();
                Random aleatorio = new Random();
                foreach (var item in ListaVacia)
                {
                    tempo = item; // crea una variable medicamento y asigna el valor de los que se quedaron sin existecia
                    tempo.Existencia = aleatorio.Next(1,16); // Asigna una nueva existenica
                    ArbolInventario.buscar(item.Nombre, item.Existencia,2); // Inserta al arbol
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RecibirParametrosArbol(int tamaño)
        {
            dividir(tamaño);
            return RedirectToAction("/Menu");
        }

        public ActionResult Compras(string Nombre, string Nit, string Farmaco, int Existencia)
        {
            
            Medicamento tempo = new Medicamento();
            Compras auxiliar = new Compras();
            ArbolInventario.buscarMedicamento(Farmaco, ref tempo);
            
            //Verifica disponibilidad y realiza la compra
            if (tempo.Nombre == Farmaco && tempo.Existencia>= Existencia)
            {
                auxiliar = auxiliar.llenar(Nombre, Nit, Farmaco, Existencia, tempo.Precio);
                ListadeCompradores.Add(auxiliar);//Agrega comprador a la lista de compradores
                tempo.Existencia = Existencia; //Agrega el valor de la existencia al inventario de pedido
                ArbolPedido.insertar(tempo);// Inserta al arbol de inventario de pedidos
                ArbolInventario.buscar(Nombre, Existencia,1); //Cambia el valor de la existenica en  el inventario inicial
                ArbolInventario.buscarMedicamento(Farmaco, ref tempo); //vuelve a tomar el valor de temporal en el arbol de inventario inicial
             
                //Si no hay existencia
                if (tempo.Existencia == 0) // comprueba si la existencia es cero
                {

                    ListaVacia.Add(tempo);
                 
                }


                return RedirectToAction("/MercanciaComprada", auxiliar);

            }
            else
            {
                return RedirectToAction("/VentanaDeFalla");
            }
        }
    }
}