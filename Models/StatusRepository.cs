
using Microsoft.EntityFrameworkCore;

namespace mechatro_ecommerce.Models
{
	public class StatusRepository : IStatusRepository
	{
		MechatroContext context = new MechatroContext();
		public  bool StatusDelete(int id)
		{
			try
			{
				using (MechatroContext context = new MechatroContext())
				{
					Status status = context.Statuses.FirstOrDefault(c => c.StatusID == id);
					status.IsActive = false;

					context.SaveChanges();
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<Status> StatusDetails(int? id)
		{
			Status? status = await context.Statuses.FirstOrDefaultAsync(S => S.StatusID == id);
			return status;
		}

		public  bool StatusInsert(Status status)
		{
			try
			{
				using (MechatroContext context = new MechatroContext())
				{
					context.Add(status);
					context.SaveChanges();
					return true;
				}
			}
			catch (Exception)
			{

				return false;
			}

		}
		public async Task<List<Status>> StatusSelect()
		{
			List<Status> statuses = await context.Statuses.ToListAsync();
			return statuses;
		}
		public  bool StatusUpdate(Status status)
		{
			try
			{
				using (MechatroContext context = new MechatroContext())
				{
					context.Update(status);
					context.SaveChanges();
					return true;
				}
			}
			catch (Exception)
			{

				return false;
			}
		}
	}
}
