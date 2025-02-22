﻿using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        #region Constructors
        public Product()
        {
            //string[] colorOptions = {"Red", "Espresso", "Write", "Navy" };

            //var brownIndex = Array.IndexOf(colorOptions, "Espresso");

            //colorOptions.SetValue("Blue", 3);

            //for (int i = 0; i < colorOptions.Length; i++)
            //{
            //    colorOptions[i] = colorOptions[i].ToLower();
            //}

            //foreach (var color in colorOptions)
            //{
            //    Console.WriteLine($"the color is {color}");
            //}

            //var colorOptions = new List<string>();

            var colorOptions = new List<string>() { "red", "Espresso","White", "Navy"};
            //colorOptions.Add("Red");
            //colorOptions.Add("Espresso");
            //colorOptions.Add("Write");
            //colorOptions.Add("Navy");

            colorOptions.Insert(2, "Purple");
            colorOptions.Remove("white");
            Console.WriteLine(colorOptions);


        }
        public Product(int productId,
                        string productName,
                        string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
        }
        #endregion

        #region Properties
        public DateTime? AvailabilityDate { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }

        private string productName;
        public string ProductName
        {
            get {
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

        private Vendor productVendor;
        public Vendor ProductVendor
        {
            get {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        public string ValidationMessage { get; private set; }

        #endregion

        /// <summary>
        /// Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up the cost.</param>
        /// <returns></returns>
        public OperationResult<decimal> CalculateSuggestedPrice(decimal markupPercent)
        {
            var message = "";

            if(markupPercent <= 0 )
            {
                message = "Invalid markup percentage";
            }
            else if (markupPercent < 10)
            {
                message = "below recommended markup percentage";
            }

            var value = this.Cost + (this.Cost * markupPercent / 100);

            var result = new OperationResult<decimal>(value, message);

            return result;

        } 


        public override string ToString()
        {
            return this.ProductName + " (" + this.ProductId + ")";
        }
    }
}
