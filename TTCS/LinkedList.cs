using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace TTCS
{
    public class LinkedList
    {
        private Node head;

        public Node Head
        {
            get { return head; }
        }

        public void Swap(Node source, Node dest)
        {
            Node tmp = new Node();
            tmp.Data = source.Data;
            source.Data = dest.Data;
            dest.Data = tmp.Data;
        }


        /// <summary>
        /// 0 - Sort By Name
        /// 1 - Sort By Level
        /// 2 - Sort By DayOfBirth
        /// 3 - Sort By Salary
        /// </summary>
        /// <param name="tos"> TypeOfSort</param>
        public void CustomSort(int tos, bool asc)
        {
            Node current = head;
            while (current != null)
            {
                Node tmp = current.Next;
                while (tmp != null)
                {
                    var staffCurrent = current.Data as Staff;
                    var staffTmp = tmp.Data as Staff;
                    if (staffCurrent != null && staffCurrent.CompareTo(staffTmp, tos) == asc)
                    {
                        Swap(current, tmp);
                    }
                    tmp = tmp.Next;
                }
                current = current.Next;
            }
        }

        public void Insert(Node toAdd, int tos, bool asc)
        {
            Node current = head;
            if (head == null)
            {
                AddFirst(toAdd.Data);
                return;
            }

            if ((toAdd.Data as Staff).CompareTo(current.Data as Staff, tos) != asc)
            {
                AddFirst(toAdd.Data);
                UpdateIndex();
                return;
            }
            
            while (current.Next != null)
            {
                var staffCurrent = current.Data as Staff;
                var staffTmp = current.Next.Data as Staff;
                if (staffCurrent != null && staffCurrent.CompareTo(toAdd.Data as Staff, tos) != asc && (toAdd.Data as Staff).CompareTo(staffTmp, tos) != asc)
                {
                    toAdd.Next = current.Next;
                    current.Next = toAdd;
                    UpdateIndex();
                    return;
                }
                current = current.Next;
            }
            toAdd.Next = current.Next;
            current.Next = toAdd;
        }

        public void AddFirst(Object data, int index = -1)
        {
            Node toAdd = new Node
            {
                Index = index == -1 ? 1 : index,
                Data = data,
                Next = head
            };
            head = toAdd;
        }

        public void AddLast(Object data, int index = -1)
        {
            if (head == null)
            {
                head = new Node
                {
                    Index = index == -1 ? 1 : index,
                    Data = data,
                    Next = null
                };

            }
            else
            {
                Node toAdd = new Node {Data = data};

                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                toAdd.Index = index == -1 ? current.Index + 1 : index;

                current.Next = toAdd;
            }
        }

        public void UpdateIndex()
        {
            int index = -1;
            if (head != null)
            {
                head.Index = 1;
                index = head.Index;
            }
            Node current = head.Next;

            while (current != null)
            {
                current.Index = ++index;
                current = current.Next;
            }
        }

        public void Delete(Node data)
        {
            Node current = Head;
            if (head != null && head.Data == data.Data)
            {
                head = head.Next;
                data = null;
                return;
            }
            while (current.Next != null && current.Next.Data != data.Data)
            {
                current = current.Next;
            }
            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                data = null;
            }
            else
            {
                current = null;
                data = null;
            }
        }

        public void DeleteFirst()
        {
            if (head != null)
            {
                head = head.Next;
            }
        }

        public  bool ExportTextFile(string filepath)
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Create);//Tạo file          
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
                sWriter.WriteLine("{0, -20} {1,-20} {2,-20} {3,-20}", "Họ và tên", "Chức vụ", "Ngày sinh", "Hệ số lương");
                Node current = head;
                if (head != null)
                {
                    while (current != null)
                    {
                        Staff tmp = current.Data as Staff;
                        sWriter.WriteLine("{0, -20} {1,-20} {2,-20} {3,-20}", tmp.Name, tmp.Level.Key, tmp.DayOfBirth.ToString(), tmp.Salary.ToString(CultureInfo.InvariantCulture));
                        current = current.Next;
                    }
                }
                // Ghi và đóng file
                sWriter.Flush();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public void Clear()
        {
            head = null;
        }
    }
}
