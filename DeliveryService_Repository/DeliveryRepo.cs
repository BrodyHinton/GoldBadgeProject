public class DeliveryRepo
{
     private readonly List<Delivery> _deliveryDb = new List<Delivery>();
    private int _count;

     //Create
     public bool AddDeliveryToDb (Delivery delivery)
     {
        int startingCount = _deliveryDb.Count;
        _deliveryDb.Add(delivery);

        if (_deliveryDb.Count > startingCount)
        {
            return true;
        }
        else
        {
            return false;
        }
     }
    
    private void AssignCustomerID(Delivery delivery)
    {
        _count++;
        delivery.CustomerID = _count;
    }

     //Read

    public List<Delivery> GetDeliveries()
    {
        return _deliveryDb;
    }

    public Delivery GetDeliveryByStatus(string orderStatus)
    {
        foreach (var delivery in _deliveryDb)
        {
            if (delivery.OrderStatus == orderStatus)
            {
                return delivery;
            }
        }
        // or retun null
        return null;
    }

    //Update
    public bool UpdateExistingDeliveries(string orderStatus, Delivery updatedData)
    {
        Delivery deliveryinDb = GetDeliveryByStatus(orderStatus);

        if (deliveryinDb!= null)
        {
            deliveryinDb.OrderDate = updatedData.OrderDate;
            deliveryinDb.DeliveryDate = updatedData.DeliveryDate;
            deliveryinDb.OrderStatus = updatedData.OrderStatus;
            deliveryinDb.ItemNumber = updatedData.ItemNumber;
            deliveryinDb.ItemQuantity= updatedData.ItemQuantity;
            deliveryinDb.CustomerID= updatedData.CustomerID;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Delete
    public bool DeleteDeliveryData(string orderStatus)
    {
        Delivery deliveryInDb = GetDeliveryByStatus(orderStatus);
        return _deliveryDb.Remove(deliveryInDb);
    }

     public bool DeleteExistingContent(Delivery orderStatus)
    {
        bool deleteResult = _deliveryDb.Remove(orderStatus);
        return deleteResult;
    }
    public List<Delivery> EnRouteDeliveries()
    {
        List<Delivery> enroutedeliveries = new List<Delivery>();
        foreach (Delivery delivery in _deliveryDb)
        {
            if (delivery.OrderStatus == "EnRoute")
            {
                enroutedeliveries.Add(delivery);
            }
        }
        return enroutedeliveries;
    }

    public List<Delivery> CompletedDeliveries()
    {
        List<Delivery> completeddeliveries = new List<Delivery>();
        foreach (Delivery delivery in _deliveryDb)
        {
            if (delivery.OrderStatus == "Complete")
            {
                completeddeliveries.Add(delivery);
            }
        }
        return completeddeliveries;
    }
}