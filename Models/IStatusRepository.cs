namespace mechatro_ecommerce.Models
{
	public interface IStatusRepository
	{
		Task<List<Status>> StatusSelect();
		bool StatusInsert(Status status);
		Task<Status> StatusDetails(int? id);
		bool StatusUpdate(Status status);
		bool StatusDelete(int id);

	}
}
