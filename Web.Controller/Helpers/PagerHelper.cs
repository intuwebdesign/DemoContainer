using System;
using System.Collections.Generic;

namespace Web.Controller.Helpers
{
    public static class ListOfBeverages
    {
        public static List<ListBeveragesViewModel> TopAlcoholicDrinksUk()
        {
            List<ListBeveragesViewModel> lstBeers = new List<ListBeveragesViewModel>
            {
                new ListBeveragesViewModel("Peroni", "beer"),
                new ListBeveragesViewModel("Guinness", "beer"),
                new ListBeveragesViewModel("Bulmers", "cider"),
                new ListBeveragesViewModel("Stella Artois", "beer"),
                new ListBeveragesViewModel("Magners", "cider"),
                new ListBeveragesViewModel("Heineken", "beer"),
                new ListBeveragesViewModel("Amstel", "beer"),
                new ListBeveragesViewModel("Corona", "beer"),
                new ListBeveragesViewModel("Becks", "beer"),
                new ListBeveragesViewModel("Budweiser", "beer"),
                new ListBeveragesViewModel("Kopparberg", "cider"),
                new ListBeveragesViewModel("San Miguel", "beer"),
                new ListBeveragesViewModel("Tetley's Brewery", "beer"),
                new ListBeveragesViewModel("Kronenburg 1664", "beer"),
                new ListBeveragesViewModel("Strongbow", "cider"),
                new ListBeveragesViewModel("Carlsberg", "beer"),
                new ListBeveragesViewModel("Hobgoblin", "cider"),
                new ListBeveragesViewModel("Sol", "beer"),
                new ListBeveragesViewModel("Thatchers Gold", "cider"),
                new ListBeveragesViewModel("Pepsi Max", "soft drink")
            };


            return lstBeers;

        }
    }

    public class DisplayPagerViewModel
    {
        public IEnumerable<ListBeveragesViewModel> DisplaySearchResults { get; set; }
        public PagerHelper.Pagination Pager                             { get; set; }
        public int NumberOfResults                                      { get; set; }
    }

    public class ListBeveragesViewModel
    {
        public ListBeveragesViewModel(string beverage, string type)
        {
            Beverage    = beverage;
            Type        = type;
        }

        public string Beverage { get; set; }
        public string Type { get; set; }
    }
    public static class PagerHelper
    {

        public class Pagination
        {
            public Pagination(int totalItems, int? page, int pageSize = 10)
            {
                var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);

                if (page > totalPages || page < 0)
                {
                    page = 1;
                }
                var currentPage     = page ?? 1;
                var startPage       = currentPage - 5;
                var endPage         = currentPage + 4;

                if (startPage <= 0)
                {
                    endPage -= (startPage - 1);
                    startPage = 1;
                }
                if (endPage > totalPages)
                {
                    endPage = totalPages;
                    if (endPage > 10)
                    {
                        startPage = endPage - 9;
                    }
                }

                StartPage       = startPage;
                CurrentPage     = currentPage;
                PageSize        = pageSize;
                TotalItems      = totalItems;
                TotalPages      = totalPages;
                EndPage         = endPage;
            }

            public int StartPage        { get; }
            public int CurrentPage      { get; }
            public int PageSize         { get; }
            public int TotalItems       { get; }
            public int TotalPages       { get; }
            public int EndPage          { get; }
        }
    }
}
