namespace I2P_Project.Classes
{
    /// <summary> PriorityQueue class </summary>
    class PriorityQueue<Generic>
    {
        private int _length = 0;  // Length of a queue
        private Node _first = null;  // First element in queue
        private Node _rare = null;  // Last element in queue

        public class Node  // Element constructor
        {
            public Generic Element;  // UserID
            public int PriorityLevel;
            public Node Link;  // Link cell to next element of queue
        }

        /// <summary> Length of the queue </summary>
        public int Length => _length;
        /// <summary> Returns first element in queue </summary>
        public Node FirstElement => _first;
        /// <summary> Checking empty in queue </summary>
        private bool IsEmpty => (_length == 0);

        /// <summary> Enqueue method </summary>
        public void Push(Generic el, int priority)
        {
            Node temp = _first;
            Node prev_node = _first;  // Previous node
            Node toAdd = new Node
            {
                Element = el,
                PriorityLevel = priority
            };

            if (temp == null)
            {
                _first = toAdd;
                _length++;
                return;
            }

            while (true)  // move elements in the queue according to priority
            {
                if (priority > temp.PriorityLevel)
                {
                    if (temp.Link != null)
                    {
                        prev_node = temp;
                        temp = temp.Link;
                    }
                    else
                    {
                        temp.Link = toAdd;
                        _rare = temp.Link;
                        break;
                    }
                }
                else if (priority < temp.PriorityLevel)
                {
                    if (temp == _first)
                    {
                        toAdd.Link = _first;
                        _first = toAdd;
                    }
                    else
                    {
                        prev_node.Link = toAdd;
                        toAdd.Link = temp;
                    }
                    break;
                }
                else if (priority == temp.PriorityLevel)
                {
                    if (temp.Link != null)
                    {
                        while (priority == temp.PriorityLevel)
                        {
                            if (temp.Link != null)
                            {
                                prev_node = temp;
                                temp = temp.Link;
                            }
                            else
                            {
                                temp.Link = toAdd;
                                _length++;
                                return;
                            }
                        }
                        toAdd.Link = prev_node.Link;
                        prev_node.Link = toAdd;
                        break;
                    }
                    else
                    {
                        temp.Link = toAdd;
                        _rare = temp.Link;
                        break;
                    }
                }
            }
            _length++;  // Increase queue length by 1
        }

        /// <summary> Dequeue method </summary>
        public Generic Pop()
        {
            if (_length > 0)
            {
                Generic el = _first.Element;
                _first = _first.Link;
                _length--;
                return el;  // Returns element data
            }
            else
            {
                return default(Generic);
            }
        }
    }
}
