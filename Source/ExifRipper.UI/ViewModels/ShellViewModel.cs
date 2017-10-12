using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.UI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private SimpleContainer container;

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;

            DisplayName = "ExifRipper";

            Items.Add(new MainViewModel());
            
            
        }

        protected override void OnActivate()
        {
            ActivateItem(Items.First(x => x.GetType() == typeof(MainViewModel)));
        }
    }
}
