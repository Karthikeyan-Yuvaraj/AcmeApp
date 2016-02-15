using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region Constructors
        public Product()
        {
            Console.WriteLine("Product instance created");
            //this.ProductVendor = new Vendor();
            this.MinimumPrice = .96m;
        }

        public Product(int productId, string productName, string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine("Product instance has a name: " + ProductName);
        }
        #endregion

        #region Properties
        private DateTime? _availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return _availabilityDate; }
            set { _availabilityDate = value; }
        }

        private string productName;

        public string ProductName
        {
            get
            {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }

            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }


        //Old way to implement lazy loading
        //private Vendor _productVendor;

        //public Vendor ProductVendor
        //{
        //    get
        //    {
        //        if (_productVendor == null)
        //        {
        //            _productVendor = new Vendor();
        //        }
        //        return _productVendor;
        //    }
        //    set { _productVendor = value; }
        //}

            //new way to do lazy loading
        private readonly Lazy<Vendor> _productVendor = new Lazy<Vendor>(() => new Vendor());

        public Vendor ProductVendor
        {
            get { return _productVendor.Value; }
        }

        public string ValidationMessage { get; private set; }

        public string Category { get; set; }

        public int SequenceNumber { get; set; } = 1;




        #endregion

        public string SayHello()
        {
//            var vendor = new Vendor();
//            vendor.SendWelcomeEmail("Message from Products");

            var emailService = new EmailService();
            emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");
            LoggingService.LogAction("saying hello");
            return "Hello " + ProductName + " (" + ProductId + "): " + Description + " Available on: " + (AvailabilityDate.HasValue ? AvailabilityDate.Value.ToShortDateString() : null);
        }

        public override string ToString()
        {
            return this.ProductName + this.productId;
        }
    }
}
