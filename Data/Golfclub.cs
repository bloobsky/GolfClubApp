using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;


namespace GolfClubApp.Data
{
    public interface IGolfRep
    {
        Task<IEnumerable<Golfer>> GetAllGolfers();
        Task<Golfer> AddGolfer(Golfer golfer);
        Task<Golfer> GetGolfer(int id);
        Task<Golfer> ChangeGolfersData(Golfer golfer);
        Task<IEnumerable<Tee>> GetAllTees();
        Task<Tee> GetTee(int id);
        Task<TeeBooking> GetTeeBooking(int id);
        Task<TeeBooking> SaveTeeBooking(TeeBooking booking);

    }

    public class GolfRepository : IGolfRep
    {
        private Context _repo;

        public string DatabaseFilepath => _repo.DatabasePathPointer;

        public GolfRepository()
        {
            _repo = new Context();
            if (_repo.Database.CanConnect()) return;

            _repo.Database.EnsureCreated();
            CreateTees();
        }

        private void CreateTees()
        {
            _repo.Tees.Add(new Tee { Name = "Tee One" });
            _repo.Tees.Add(new Tee { Name = "Tee Two" });
            _repo.Tees.Add(new Tee { Name = "Tee Three" });
            _repo.Tees.Add(new Tee { Name = "Tee Four" });
            _repo.Tees.Add(new Tee { Name = "Tee Five" });
            _repo.Tees.Add(new Tee { Name = "Tee Six" });
            _repo.SaveChanges();
            _repo.ChangeTracker.Clear();
        }

        public async Task<IEnumerable<Golfer>> GetAllGolfers() =>
            await _repo.Golfers.AsNoTracking().ToListAsync();


        public async Task<Golfer> AddGolfer(Golfer golfer)
        {
            _repo.Golfers.Add(golfer);
            await _repo.SaveChangesAsync();
            _repo.ChangeTracker.Clear();
            return golfer;
        }

        public async Task<Golfer> GetGolfer(int id) =>
            await _repo.Golfers.AsNoTracking().FirstAsync(x => x.Id == id);


        public async Task<Golfer> ChangeGolfersData(Golfer golfer)
        {
            _repo.Entry(golfer).State = EntityState.Modified;
            await _repo.SaveChangesAsync();
            _repo.ChangeTracker.Clear();
            return golfer;
        }

        public async Task<IEnumerable<Tee>> GetAllTees() =>
            await _repo.Tees.AsNoTracking().ToListAsync();


        public async Task<Tee> GetTee(int id) =>
            await _repo.Tees.AsNoTracking()
                .Include(tee => tee.Bookings)
                .ThenInclude(booking => booking.Golfers)
                .FirstAsync(x => x.Id == id);

        public async Task<TeeBooking> GetTeeBooking(int id) =>
            await _repo.TeeBookings
                .Include(x => x.Golfers)
                .AsNoTracking()
                .FirstAsync(x => x.Id == id);

        public async Task<TeeBooking> SaveTeeBooking(TeeBooking booking)
        {
            if (booking.BookedTee != null)
                _repo.Tees.Attach(booking.BookedTee);

            if (booking.Golfers != null)
                booking.Golfers.Select(golfer => _repo.Golfers.Attach(golfer));

            if (booking.Id > 0)
            {
                _repo.TeeBookings.Update(booking);
            }
            else
            {
                _repo.TeeBookings.Add(booking);
            }

            await _repo.SaveChangesAsync();
            _repo.ChangeTracker.Clear();
            return booking;
        }
    }
    public static class TeeBookingNamesToString
    {
        public static string NamesToString(this TeeBooking booking)
        {
            string result = booking.Golfers.Any() == true ? booking.Golfers.First().ToString() : string.Empty;
            for (int i = 1; i < booking.Golfers.Count; i++)
            {
                result = result + "," + booking.Golfers[i].ToString();
            }

            return result;
        }
    }
}
