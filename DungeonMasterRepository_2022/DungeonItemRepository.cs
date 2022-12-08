using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DungeonMasterDTO_2022;

namespace DungeonMasterRepository_2022
{
    public class DungeonItemRepository
    {
        private IConfigurationRoot _configuration;
        private DbContextOptionsBuilder<ApplicationDBContext> _optionsBuilder;

        public DungeonItemRepository()
        {
            BuildOptions();
        }

        private void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DungeonManager"));
        }

        public bool AddItem(Item itemToAdd)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                //determine if item exists
                Item existingItem = db.Items.FirstOrDefault(x => x.Name.ToLower() == itemToAdd.Name.ToLower());

                if (existingItem == null)
                {
                    // doesn't exist, add it
                    db.Items.Add(itemToAdd);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }

    }
}
