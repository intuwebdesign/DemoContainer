using System.Collections.Generic;

namespace Web.Model.DynamicNavigationModel
{
    public class DisplayBootstrapNavigtaionViewModel
    {
        public List<BootstrapNavigtaionViewModel> DisplayNavigation { get; set; }
    }
    public class BootstrapNavigtaionViewModel
    {
        public BootstrapNavigtaionViewModel(string manufacturer, string carType,string model, string pageUrl)
        {
            Manufacturer    = manufacturer;
            CarType         = carType;
            Model           = model;
            PageUrl         = pageUrl;
        }
        public string Manufacturer  { get; }
        public string CarType       { get;}
        public string Model         { get;}
        public string PageUrl       { get;}
    }
}
