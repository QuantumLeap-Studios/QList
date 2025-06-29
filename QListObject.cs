using System.Collections.Generic;

namespace QList
{
    public class QListObject
    {
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public object this[string key]
        {
            get => Properties.ContainsKey(key) ? Properties[key] : null;
            set => Properties[key] = value;
        }
    }
}
