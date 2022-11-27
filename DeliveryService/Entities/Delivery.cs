public class Delivery
    {

       public Delivery () 
       {

       }

        public Delivery (string orderDate, string deliveryDate, string orderStatus, int itemNumber, int itemQuantity, int customerID)
        {
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
            OrderStatus = orderStatus;
            ItemNumber = itemNumber;
            ItemQuantity = itemQuantity;
            CustomerID = customerID;
        }
        
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        public int ItemNumber { get; set; }
        public int ItemQuantity { get; set; }
        public int CustomerID { get; set; }

        public override string ToString()
        {
            string str = $"Order Date {OrderDate}\n" +
                         $"Delivery Date {DeliveryDate}\n" +
                         $"Order Status {OrderStatus}\n" +
                         $"Item Number {ItemNumber}\n" +
                         $"Item Quantity {ItemQuantity}\n" +
                         $"Customer ID {CustomerID.ToString()}\n" +
                         "====================================\n";
            return str;
        }
    }