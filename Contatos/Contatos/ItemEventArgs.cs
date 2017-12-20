using System;

namespace Contatos
{
    public class ItemEventArgs : EventArgs
    {
        public object Item { get; set; }

        public ItemEventArgs(object item)
        {
            this.Item = item;
        }
    }
}
