using OrderApp.Models;

namespace OrderApp.Context
{
    public class ApplicationContextInitializer : IApplicationContextInitializer
    {
        private readonly ApplicationContext _db;

        public ApplicationContextInitializer(ApplicationContext db)
        {
            this._db = db;
        }

        public void Initialize()
        {
            if (!this._db.Providers.Any())
            {
                this._db.Providers.Add(new Provider() { Name = "Яндекс.Доставка" });
                this._db.Providers.Add(new Provider() { Name = "Amazon" });
                this._db.Providers.Add(new Provider() { Name = "Delivery" });
                this._db.Providers.Add(new Provider() { Name = "Почта России" });
                this._db.SaveChanges();
            }
        }
    }

    public interface IApplicationContextInitializer
    {
        void Initialize();
    }
}
