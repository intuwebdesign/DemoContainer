using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using IpInfo;
using Newtonsoft.Json;

namespace Web.Model.ShoppingCartModel
{
    public class DisplayProductsViewModel
    {
        public IEnumerable<ProductsViewModel> DisplayProducts { get; set; }
    }

    public class ProductsViewModel
    {
        public ProductsViewModel(string productId, string productTitle, string productDescription, string productImage, double productPrice, double productCulturePrice, string currencyFormat)
        {
            ProductId               = productId;
            ProductTitle            = productTitle;
            ProductDescription      = productDescription;
            ProductImage            = productImage;
            ProductPrice            = productPrice;
            ProductCulturePrice     = productCulturePrice;
            CurrencyFormat          = currencyFormat;
        }

        public ProductsViewModel(string productId, string productTitle, string productDescription, string productImage, double productPrice)
        {
            ProductId               = productId;
            ProductTitle            = productTitle;
            ProductDescription      = productDescription;
            ProductImage            = productImage;
            ProductPrice            = productPrice;
        }

        public string ProductId             { get; }
        public string ProductTitle          { get; }
        public string ProductDescription    { get; }
        public string ProductImage          { get; }
        public string CurrencyFormat        { get; }
        public double ProductPrice          { get; }
        public double ProductCulturePrice   { get; }

    }

    public class ListOfProductsDataModel
    {
        public IEnumerable<ProductsViewModel> HardCodeListOfProducts()
        {
            var client          = new HttpClient();
            var api             = new IpInfoApi("YOUR API Key", client);//You will need to add your own API key here, would really be stored in the CMS or web.config
            var response        = api.GetCurrentInformationAsync().Result;
            string country      = response.Country;

            string imagePath = "/Images/ShoppingCartImages/";

            //This would be from the database or CMS
            List<ProductsViewModel> productModel = new List<ProductsViewModel>
            {
                new ProductsViewModel("1", "Phone One", "Description 1", $"{imagePath}PhoneOne.jpg", 100.99),
                new ProductsViewModel("2", "Phone Two", "Description 2", $"{imagePath}PhoneTwo.jpg", 290.99),
                new ProductsViewModel("3", "Phone Three", "Description 3", $"{imagePath}PhoneThree.jpg", 256.99),
                new ProductsViewModel("4", "Phone Four", "Description 4", $"{imagePath}PhoneFour.jpg", 100.00)
            };

            //Now add the culture price
            string currencyApiKey = "YOUR API Key";//You will need to add your own API key here, would really be stored in the CMS or web.config

            var currencyClient = new HttpClient();

            List<ProductsViewModel> productModelWithCultureCurrency = new List<ProductsViewModel>();

            foreach (var culturePrice in productModel)
            {
                double defaultPrice = culturePrice.ProductPrice;
                string displayCultureCurrency;
                CurrencyApi result;
                string url;

                switch (country)
                {
                    case "FRA":
                        url = $"https://v6.exchangerate-api.com/v6/{currencyApiKey}/pair/GBP/EUR/{defaultPrice}";
                        displayCultureCurrency = currencyClient.GetStringAsync($"{url}").Result;

                        result = JsonConvert.DeserializeObject<CurrencyApi>(displayCultureCurrency);
                        if (result != null)
                        {
                            double displayCulturePrice = result.ConversionResult;
                            productModelWithCultureCurrency.Add(new ProductsViewModel(culturePrice.ProductId,culturePrice.ProductTitle,culturePrice.ProductDescription,culturePrice.ProductImage,culturePrice.ProductPrice, displayCulturePrice, "France"));
                        }
                        else
                        {
                            productModelWithCultureCurrency.Add(new ProductsViewModel(culturePrice.ProductId, culturePrice.ProductTitle, culturePrice.ProductDescription, culturePrice.ProductImage, culturePrice.ProductPrice, defaultPrice,"United Kingdom"));
                        }
                        break;
                    case "USA":
                        url = $"https://v6.exchangerate-api.com/v6/{currencyApiKey}/pair/GBP/USD/{defaultPrice}";
                        displayCultureCurrency = currencyClient.GetStringAsync($"{url}").Result;

                        result = JsonConvert.DeserializeObject<CurrencyApi>(displayCultureCurrency);
                        if (result != null)
                        {
                            double displayCulturePrice = result.ConversionResult;
                            productModelWithCultureCurrency.Add(new ProductsViewModel(culturePrice.ProductId, culturePrice.ProductTitle, culturePrice.ProductDescription, culturePrice.ProductImage, culturePrice.ProductPrice, displayCulturePrice,"United States"));
                        }
                        else
                        {
                            productModelWithCultureCurrency.Add(new ProductsViewModel(culturePrice.ProductId, culturePrice.ProductTitle, culturePrice.ProductDescription, culturePrice.ProductImage, culturePrice.ProductPrice, defaultPrice,"United Kingdom"));
                        }
                        break;
                    default:
                        productModelWithCultureCurrency.Add(new ProductsViewModel(culturePrice.ProductId, culturePrice.ProductTitle, culturePrice.ProductDescription, culturePrice.ProductImage, culturePrice.ProductPrice, culturePrice.ProductPrice, "United Kingdom"));
                        break;
                }
            }

            return productModelWithCultureCurrency;
        }
    }

    public class CurrencyApi
    {
        [JsonProperty("result")]
        public string Result                    { get; set; }
        [JsonProperty("documentation")]
        public string Documentation             { get; set; }
        [JsonProperty("terms_of_use")]
        public string TermsOfUse                { get; set; }
        [JsonProperty("time_last_update_unix")]
        public int TimeLastUpdateUnix           { get; set; }
        [JsonProperty("time_last_update_utc")]
        public string TimeLastUpdateUtc         { get; set; }
        [JsonProperty("time_next_update_unix")]
        public int TimeNextUpdateUnix           { get; set; }
        [JsonProperty("time_next_update_utc")]
        public string TimeNextUpdateUtc         { get; set; }
        [JsonProperty("base_code")]
        public string BaseCode                  { get; set; }
        [JsonProperty("target_code")]
        public string TargetCode                { get; set; }
        [JsonProperty("conversion_rate")]
        public double ConversionRate            { get; set; }
        [JsonProperty("conversion_result")]
        public double ConversionResult          { get; set; }
    }

    public class CustomerShippingDetailsViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string CustomerFirstName         { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string CustomerLastName          { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]
        public string CustomerAddress           { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City Required")]
        public string CustomerCity              { get; set; }
        [Display(Name = "County")]
        [Required(ErrorMessage = "County Required")]
        public string CustomerCounty            { get; set; }
        [Display(Name = "Post-Code")]
        [Required(ErrorMessage = "Post-Code Required")]
        public string CustomerPostCode          { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Required")]
        public string CustomerCountry           { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        public string CustomerPhoneNumber       { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required")]
        public string CustomerEmail             { get; set; }
        [Display(Name = "Name on card")]
        [Required(ErrorMessage = "Name on card required")]
        public string CustomerNameOnCard        { get; set; }
        [Display(Name = "Card Number")]
        [Required(ErrorMessage = "Card Number Required")]
        public string CustomerCardNumber        { get; set; }
        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Expiry Date Requied")]
        public string CustomerCardExpiryDate    { get; set; }
        [Display(Name = "CVV Number")]
        [Required(ErrorMessage = "CVV Number Required")]
        public string CustomerCardCvvNumber     { get; set; }
        public string ProductIds                { get; set; }
    }

    public class ProductIds
    {
        public ProductIds(string productTitle, double productPrice)
        {
            ProductTitle    = productTitle;
            ProductPrice    = productPrice;
        }

        public ProductIds(){}

        public string Id            { get; set; }
        public string ProductTitle  { get; set; }
        public double ProductPrice  { get; set; }
    }
}