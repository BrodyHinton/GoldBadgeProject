using Xunit;
using System.Collections.Generic;
public class DeliveryTests
{
    private DeliveryRepo _deliveryRepo;
    private Delivery _deliveryA;
    private Delivery _deliveryB;
    private Delivery _deliveryC;
    private Delivery _deliveryD;

    public DeliveryTests()
    {
        _deliveryRepo = new DeliveryRepo();
        _deliveryA = new Delivery(
            "2022/11/23",
            "2022/12/28",
            "EnRoute",
            57,
            2,
            145);

        _deliveryB = new Delivery(
            "2022/05/12",
            "2022/05/17",
            "Completed",
            73,
            1,
            110);

        _deliveryC = new Delivery(
            "11/25",
            "11/28",
            "EnRoute",
            10,
            3,
            102);

        _deliveryD = new Delivery(
            "12/20",
            "12/23",
            "Scheduled",
            123,
            2,
            282);

        _deliveryRepo.AddDeliveryToDb(_deliveryA);
        _deliveryRepo.AddDeliveryToDb(_deliveryB);
        _deliveryRepo.AddDeliveryToDb(_deliveryC);
        _deliveryRepo.AddDeliveryToDb(_deliveryD);
    }

    [Fact]
    public void AddToDatabase_ShouldGetCorrectBoolean()
    {
        Delivery delivery = new Delivery();
        DeliveryRepo repository = new DeliveryRepo();

        bool addResult = repository.AddDeliveryToDb(delivery);

        Assert.True(addResult);
    }

    [Fact]
    public void Get_DatabaseInfo_Should_Return_CorrectCollection()
    {

        Delivery delivery = new Delivery();
        DeliveryRepo repository = new DeliveryRepo();

        repository.AddDeliveryToDb(delivery);

        List<Delivery> deliveries = repository.GetDeliveries();

        bool dbaseHasDeliveries = deliveries.Contains(delivery);

        Assert.True(dbaseHasDeliveries);
    }

    [Fact]
    public void GetOrderStatus_Should_Return_Correct_Delivery()
    {
        Delivery searchResult = _deliveryRepo.GetDeliveryByStatus("EnRoute");

        Assert.Equal(searchResult, _deliveryA);

    }

    [Fact] //update
    public void UpdateExistingData_Should_Return_True()
    {
        Delivery updatedData = new Delivery("2022/11/23",
        "2022/11/26",
        "Completed",
        60,
        4,
        155);

        bool updateResult = _deliveryRepo.UpdateExistingDeliveries("EnRoute", updatedData);

        Assert.True(updateResult);

        Delivery updatedDelivery = _deliveryRepo.GetDeliveryByStatus("Complete");
        System.Console.WriteLine(updatedData.ItemNumber);
    }

    [Fact]
    public void Delete_Existing_Delivery_Should_Return_True()
    {
        var delivery = _deliveryRepo.GetDeliveryByStatus("EnRoute");

        bool removeResult = _deliveryRepo.DeleteExistingContent(delivery);

        Assert.True(removeResult);
        Assert.Equal(3,_deliveryRepo.GetDeliveries().Count);
    }
}
