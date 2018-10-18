using RetailerApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RetailerApp
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FromTemplate { get; set; }
        public DataTemplate ToTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((MessageText)item).Status.ToUpper().Equals("SENT") ? FromTemplate : ToTemplate;
        }
    }
}
