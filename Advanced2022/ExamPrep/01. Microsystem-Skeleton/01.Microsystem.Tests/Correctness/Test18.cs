﻿using NUnit.Framework;
using System.Linq;
using _01.Microsystem;

public class Test18
{
    [TestCase]
    public void GetAllWithScreenSize_Should_Return_Correct_Count()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
        var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = 4;
        var actual = microsystems.GetAllWithScreenSize(15.6).Count();

        //Assert

        Assert.AreEqual(expected, actual);
    }
}
