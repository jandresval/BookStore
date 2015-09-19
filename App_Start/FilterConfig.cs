using System.Web;
using System.Web.Mvc;

namespace BookStore
{
    public class FilterConfig
    {
        /// <summary>
        /// Define the filters you could do to any request.
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}