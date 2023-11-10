using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TenDrone.Controllers;
using TenDrone.Models;
using TenDrone.Resources;
using TenDrone.Services;
namespace TenDrone.Test;

public class DroneServiceTests
{
    private readonly IDroneService _droneService;
    public DroneServiceTests(IDroneService droneService)
    {
        _droneService = droneService;
    }
    [Fact]
    public void GetDrones_ReturnsDrones()
    {
        var flag = true;
        try
        {
            _droneService.RegisterDrone(new RegisterDroneRequest { BatteryLevel = 100, SerialNumber = "Drone001", Weight = 502, DroneModel = DroneModel.Lightweight });
        }
        catch(InvalidOperationException ex)
        {
            flag = false;
        }
        Assert.False(flag, "Cannnot be high to 500");
    }
    /*
    [Fact]
    public void GetDroneBySerialNumber_ValidSerialNumber_ReturnsDrone()
    {
        // Arrange
        var droneServiceMock = new Mock<IDroneService>();
        droneServiceMock.Setup(service => service.GetDroneBySerialNumber("TestDrone")).Returns(new Drone { SerialNumber = "TestDrone" });

        var controller = new DroneController(droneServiceMock.Object);

        // Act
        var result = controller.GetDroneBySerialNumber("TestDrone");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var drone = Assert.IsType<Drone>(okResult.Value);
        Assert.Equal("TestDrone", drone.SerialNumber);
    }

    [Fact]
    public void LoadDrone_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var droneServiceMock = new Mock<IDroneService>();
        droneServiceMock.Setup(service => service.LoadDrone(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>()));

        var controller = new DroneController(droneServiceMock.Object);
        var request = new LoadDroneRequest { SerialNumber = "TestDrone", Weight = 400, BatteryLevel = 80 };

        // Act
        var result = controller.LoadDrone(request);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void UpdateDrone_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var droneServiceMock = new Mock<IDroneService>();
        droneServiceMock.Setup(service => service.UpdateDrone(It.IsAny<string>(), It.IsAny<UpdateDroneRequest>()));

        var controller = new DroneController(droneServiceMock.Object);
        var request = new UpdateDroneRequest { Model = DroneModel.Middleweight, WeightLimit = 450, BatteryCapacity = 90 };

        // Act
        var result = controller.UpdateDrone("TestDrone", request);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void DeleteDrone_ValidSerialNumber_ReturnsOkResult()
    {
        // Arrange
        var droneServiceMock = new Mock<IDroneService>();
        droneServiceMock.Setup(service => service.DeleteDrone("TestDrone"));

        var controller = new DroneController(droneServiceMock.Object);

        // Act
        var result = controller.DeleteDrone("TestDrone");

        // Assert
        Assert.IsType<OkResult>(result);
    }*/
}