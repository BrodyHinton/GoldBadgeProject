using static System.Console;

public class Program_UI
{
    private DeliveryRepo _deliveryRepo;
    private bool isRunningUI;

    public Program_UI()
    {
        _deliveryRepo = new DeliveryRepo();
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        isRunningUI = true;
        while (isRunningUI)
        {
            Clear();
            WriteLine("== Warner Transit Federal ==\n" +
                  "Please Make a Selection:\n" +
                  "1. Place a Delivery\n" +
                  "2. View All EnRoute Deliveries\n" +
                  "3. View All Complete Deliveries \n" +
                  "4. Update Existing Deliveries\n" +
                  "5. Cancel Existing Deliveries\n" +
                  "6. Close Application\n");
                  string userInputMenuSelection = ReadLine();
            switch (userInputMenuSelection)
            {
                case "1":
                    AddADelivery();
                    break;
                case "2":
                    ViewAllEnRouteDeliveries();
                    break;
                case "3":
                    ViewAllCompletedDeliveries();
                    break;
                case "4":
                    UpdateAnExistingDelivery();
                    break;
                case "5":
                    CancelAnExistingDelivery();
                    break;
                case "6":
                    CloseApplication();
                    break;
                default:
                    WriteLine("Invalid Selection");
                    DSUtils.PressAnyKey();
                    break;
            }
        }
    }

    private void CloseApplication()
    {
        isRunningUI = false;
        DSUtils.isRunning = false;
        WriteLine("Closing Application");
        DSUtils.PressAnyKey();
    }

    private void ViewAllEnRouteDeliveries()
    {
        Clear();
        WriteLine("== EnRoute Deliveries ==\n");
        ShowEnRouteDeliveries();
        ReadKey();
    }

    private void ViewAllCompletedDeliveries()
    {
        Clear();
        WriteLine("== Completed Deliveries ==\n");
        ShowCompletedDeliveries();
        ReadKey();
    }

    private void ShowEnRouteDeliveries()
    {
        List<Delivery> enrouteddeliveries = _deliveryRepo.EnRouteDeliveries();
        if (enrouteddeliveries.Count > 0)
        {
            foreach (var Delivery in enrouteddeliveries)
            {
                DisplayDeliveryData(Delivery);
            }
        }
        else
        {
            WriteLine("There Are No EnRoute Deliveries at this time.");
        }
    }

    private void ShowCompletedDeliveries()
    {
        List<Delivery> completedddeliveries = _deliveryRepo.CompletedDeliveries();
        if (completedddeliveries.Count > 0)
        {
            foreach (var Delivery in completedddeliveries)
            {
                DisplayDeliveryData(Delivery);
            }
        }
        else
        {
            WriteLine("There are no completed deliveries at this time.");
        }
    }

     private void CancelAnExistingDelivery()
    {
        Delivery delivery = new Delivery();
        Clear();
        ShowEnlistedDeliveries();
        WriteLine("----------\n");
        try
        {
            WriteLine("Select delivery by Status.");
            string userInputDeliveryStatus = ReadLine();
            ValidateDeliveryInDatabase(userInputDeliveryStatus);
            WriteLine("Do you want to Cancel this Order? y/n?");
            string userInputDeleteDelivery = ReadLine();
            if (userInputDeleteDelivery == "Y".ToLower())
            {
                if (_deliveryRepo.DeleteDeliveryData(userInputDeliveryStatus) && (delivery.OrderStatus != "Complete"))
                {
                    WriteLine($" The Delivery with the Status: {userInputDeliveryStatus}, was Successfully Cancelled.");
                }
                else
                {
                    WriteLine($"The Delivery with the Status: {userInputDeliveryStatus}, was NOT Cancelled.");
                }
            }
        }
        catch
        {
            SomethingWentWrong();
        }

        ReadKey();
    }

    private void UpdateAnExistingDelivery()
    {
        Clear();
        ShowEnlistedDeliveries();
        WriteLine("----------\n");
        try
        {
            WriteLine("Select delivery by Status.");
            string userInputDeliveryStatus = ReadLine();
            Delivery deliveryInDb = GetDeliveryDataFromDb(userInputDeliveryStatus);
            bool isValidated = ValidateDeliveryInDatabase(deliveryInDb.OrderStatus);

            if (isValidated)
            {
                WriteLine("Do you want to Update this Delivery? y/n?");
                string userInputDeleteDelivery = ReadLine();
                if (userInputDeleteDelivery == "Y".ToLower())
                {
                    Delivery updatedDeliveryData = InitialDeliveryCreationSetup();

                    if (_deliveryRepo.UpdateExistingDeliveries(deliveryInDb.OrderStatus, updatedDeliveryData))
                    {
                        WriteLine($" The Delivery {updatedDeliveryData.OrderStatus}, was Successfully Updated.");
                    }
                    else
                    {
                        WriteLine($"The Delivery {updatedDeliveryData.OrderStatus}, was NOT Updated.");
                    }
                }
                else
                {
                    WriteLine("Returning to Delivery Menu.");
                }
            }
        }
        catch
        {
            SomethingWentWrong();
        }
        ReadKey();
    }

    private void SomethingWentWrong()
    {
        WriteLine("Something went wrong.\n" +
                       "Please try again\n" +
                       "Returning to Main Menu.");
    }

     private bool ValidateDeliveryInDatabase(string userInputDeliveryStatus)
    {
        Delivery delivery = GetDeliveryDataFromDb(userInputDeliveryStatus);
        if (delivery != null)
        {
            Clear();
            DisplayDeliveryData(delivery);
            return true;
        }
        else
        {
            WriteLine($"The Delivery with the Status: {userInputDeliveryStatus} doesn't Exist!");
            return false;
        }
    }

     private Delivery GetDeliveryDataFromDb(string userInputDeliveryStatus)
    {
        return _deliveryRepo.GetDeliveryByStatus(userInputDeliveryStatus);
    }

    private void ShowEnlistedDeliveries()
    {
        Clear();
        WriteLine("== Delivery Listing ==");
        List<Delivery> deliveriesInDb = _deliveryRepo.GetDeliveries();
        ValidateDeliveryDatabaseData(deliveriesInDb);
    }

    private void ValidateDeliveryDatabaseData(List<Delivery> deliveriesInDb)
    {
        if (deliveriesInDb.Count > 0)
        {
            Clear();
            foreach (var delivery in deliveriesInDb)
            {
                DisplayDeliveryData(delivery);
            }
        }
        else
        {
            WriteLine("There are no Deliveries in the Database.");
        }
    }

    private void DisplayDeliveryData(Delivery delivery)
    {
        WriteLine(delivery);
    }

    private void AddADelivery()
    {
        Clear();
        try
        {
            Delivery delivery = InitialDeliveryCreationSetup();
            if (_deliveryRepo.AddDeliveryToDb(delivery))
            {
                WriteLine($"Successfully Added {delivery.ItemNumber} to the Database!");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch
        {

            SomethingWentWrong();
        }
        ReadKey();
    }

    private Delivery InitialDeliveryCreationSetup()
    {
        Delivery delivery = new Delivery();

        WriteLine("== Add Delivery Menu ==");

        WriteLine("What is the Current Date?");
        delivery.OrderDate = ReadLine();

        WriteLine("What is 3 business days from now?");
        delivery.DeliveryDate = ReadLine();

        WriteLine ("What is the number of the Item you want to purchase?");
        delivery.ItemNumber = int.Parse(ReadLine());

        WriteLine ("How many would you like to purchase?");
        delivery.ItemQuantity = int.Parse(ReadLine());   

        WriteLine("Would you like to Schedule this order for later or Order now?\n" +
        "1. Schedule for Later\n" +
        "2. Order Now");

        string userInputDeliveryStatus = ReadLine();

        switch(userInputDeliveryStatus)
        {
            case "1":
             userInputDeliveryStatus = "1";
             delivery.OrderStatus = "Scheduled";
             break;

            case "2":
             userInputDeliveryStatus = "2";
             delivery.OrderStatus = "EnRoute";
             break;

            default:
                WriteLine("Invalid Selection");
                DSUtils.PressAnyKey();
                break;
        }
    return delivery;
    }
}
