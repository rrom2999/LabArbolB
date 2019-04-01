using System.Web;
using System.Web.Mvc;

namespace Laboratorio_7_ArbolesB_MarceloRosales
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
