namespace TTCS
{
    public class Node
    {
        private int _index;
        private object _data;
        private Node _next;

        public int Index
        {
            set { _index = value; }
            get { return _index; }
        }

        public object Data
        {
            set { _data = value; }
            get { return _data; }
        }

        public Node Next
        {
            set { _next = value; }
            get { return _next; }
        }
    }
}
