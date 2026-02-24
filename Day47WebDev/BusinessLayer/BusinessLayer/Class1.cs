using DAL;

namespace BusinessLayer
{
    public class BLReverse
    {
        public string ReverseString()
        {
            DLString obj = new DLString();

            string x = obj.GetAllNames();

            x = new string(x.Reverse().ToArray());

            return x;
        }
    }
}
