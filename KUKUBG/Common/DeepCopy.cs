using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KUKUBG.Common
{
    [Serializable]
    public static class DeepCopy
    {
        private static object lockObj = new object();
        public static T Copy<T>(T obj)
        {
            lock (lockObj)
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    stream.Position = 0;

                    return (T)formatter.Deserialize(stream);
                }
            }
        }
    }
}
