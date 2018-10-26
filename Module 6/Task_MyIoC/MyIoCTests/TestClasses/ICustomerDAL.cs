using MyIoC.Attributes;

namespace MyIoCTests.TestClasses
{
	public interface ICustomerDAL
	{

	}

    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {

    }
}