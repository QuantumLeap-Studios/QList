using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QList
{
    public class QListSerializer
    {
        public string Serialize(QListObject obj)
        {
            StringBuilder sb = new StringBuilder(1024);  
            SerializeObject(obj, sb);
            return sb.ToString();
        }

        private void SerializeObject(QListObject obj, StringBuilder sb)
        {
            sb.Append('{');

            foreach (var kvp in obj.Properties)
            {
                sb.Append(kvp.Key);
                sb.Append("=");
                SerializeValue(kvp.Value, sb);
                sb.Append(';');
            }

            sb.Append('}');
        }

        private void SerializeValue(object value, StringBuilder sb)
        {
            if (value is QListObject nestedObj)
            {
                SerializeObject(nestedObj, sb);
            }
            else if (value is IList list)
            {
                sb.Append('[');
                for (int i = 0; i < list.Count; i++)
                {
                    SerializeValue(list[i], sb);
                    if (i < list.Count - 1)
                        sb.Append(',');
                }
                sb.Append(']');
            }
            else if (value is string str)
            {
                sb.Append('"').Append(str).Append('"');
            }
            else if (value is bool b)
            {
                sb.Append(b ? "true" : "false");
            }
            else
            {
                sb.Append(value.ToString());
            }
        }
    }
}
